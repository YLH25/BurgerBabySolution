using BurgerBabyApi.Models.EFModel;
using BurgerBabyApi.Models.Repo.Interface;
using BurgerBabyApi.Models.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BurgerBabyApi.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace BurgerBabyApi.Controllers
{

    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IMemberRepository _memberRepository;
        public MemberController(IMemberService memberService, IMemberRepository memberRepository)
        {
            _memberService = memberService;
            _memberRepository = memberRepository;
        }
        [HttpGet("member")]
        public async Task<IActionResult> GetMemberInfo()
        {
            try
            {
                var claims = User.Claims.ToList();
                var id = Convert.ToInt32(claims[0].Value);
                var member = await _memberRepository.GetMemberByIdAsync(id);
                if (member == null)
                {
                    return NotFound();
                }

                var data = new { Name = member.Name, Phone = member.Phone, Address = member.Address, Email = member.Email };
                return Ok(data);

            }
            catch {
                return BadRequest("獲取會員資料失敗");
            }
      
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] MemberCreateVM memberCreateVM)
        {
            try
            {
                if(memberCreateVM.Email==null)
                    return BadRequest("請填入Email");
                var existMember = await _memberRepository.GetMemberByEmailAsync(memberCreateVM.Email);
                if (existMember!=null)
                {
                    return BadRequest("這個Email已經註冊過了");
                }
                memberCreateVM.RoleId = 2;
                await _memberService.CreateMemberAsync(memberCreateVM.ToEntity().ToDto());
                return Ok("註冊成功");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("change-memberinfo")]
        public async Task<IActionResult> Edit([FromBody] MemberVM memberVM)
        {
            try
            {
                var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
                {
                    return Unauthorized("沒有有效的JWT");
                }
                var jwt = authorizationHeader.Substring("Bearer ".Length).Trim();
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);
                var userIdFromClaim = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var isInt = int.TryParse(userIdFromClaim, out int userId);
                if (isInt == false)
                    return BadRequest("JWT未包含用戶訊息");
                var member = await _memberRepository.GetMemberByIdAsync(userId);
                if (member == null) { return NotFound(); }
                if (!string.IsNullOrEmpty(memberVM.Password))
                    member.Password = memberVM.Password;
                if (!string.IsNullOrEmpty(memberVM.Phone))
                    member.Phone = memberVM.Phone;
                if (!string.IsNullOrEmpty(memberVM.Address))
                    member.Address = memberVM.Address;
                if (!string.IsNullOrEmpty(memberVM.Name))
                    member.Name = memberVM.Name;
                await _memberRepository.EditMemberAsync(member.ToDto());
                return Ok("修改成功");
            }
            catch (Exception ex)
            {
                return BadRequest("修改失敗," + ex.Message);
            }

        }
    }
}













