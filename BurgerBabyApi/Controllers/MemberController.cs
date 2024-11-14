using BurgerBabyApi.Models.EFModel;
using BurgerBabyApi.Models.Repo.Interface;
using BurgerBabyApi.Models.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BurgerBabyApi.Controllers
{

    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IMemberRepository _memberRepository;
        public MemberController(IMemberService memberService,IMemberRepository memberRepository)
        {
                _memberService = memberService;
            _memberRepository = memberRepository;
        }
        [HttpGet("member/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var member= await _memberRepository.GetMemberByIdAsync(id);
            if (member == null) { 
            return NotFound();
            }
            var data = new {Name=member.Name ,Phone=member.Phone,Address=member.Address,Email=member.Email};
           return Ok(data);
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(string newPassword) {
            try {
                var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
                {
                    return Unauthorized("沒有有效的JWT");
                }
                var jwt = authorizationHeader.Substring("Bearer ".Length).Trim();
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);
                var userIdFromClaim = token.Claims.FirstOrDefault(x => x.Type ==ClaimTypes.NameIdentifier)?.Value;
                var isInt = int.TryParse(userIdFromClaim, out int userId);
                if (isInt == false)
                    return BadRequest("JWT未包含用戶訊息");
                var member = await _memberRepository.GetMemberByIdAsync(userId);
                if (member == null) { return NotFound(); }
                member.Password= newPassword;
                await _memberRepository.EditMemberAsync(member.ToDto());
                return Ok("修改成功");
            }
            catch (Exception ex){ 
            return BadRequest("修改失敗,"+ex.Message);
            }
        }
    }
}
