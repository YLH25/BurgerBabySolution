using BurgerBabyApi.Models.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BurgerBabyApi.Models.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using BurgerBabyApi.Models.EFModel;
using BurgerBabyApi.Models.Infa; 
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;


namespace BurgerBabyApi.Controllers
{
 
    public class HomeController : Controller
    {
        private IConfiguration _configuration;
        private IMemberService _memberService;
        private IRoleService _roleService;
        public HomeController(IConfiguration configuration, IMemberService memberService, IRoleService roleService)
        {
            _configuration = configuration;
            _memberService = memberService;
            _roleService = roleService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginVM loginVM)
        {
            var valid = await _memberService.ValidateMemberAsync(loginVM.Email, loginVM.Password);
            if (valid != null)
            {
                var member = valid;
                var role = await _roleService.GetRoleByIdAsync(member.RoleId);
                var claims = new List<Claim>();
                if (role == null || role.RoleName == null)
                    return Unauthorized();
                claims = new List<Claim>
                {
                        new Claim(ClaimTypes.NameIdentifier,member.Id.ToString()),
                        new Claim(ClaimTypes.Email, member.Email),
                        new Claim (ClaimTypes.Role, role.RoleName)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "RefreshTokenScheme");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var authProperties = new AuthenticationProperties
                {

                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
                };

                await HttpContext.SignInAsync("RefreshTokenScheme", claimsPrincipal, authProperties);
                return Ok();
            }
            return Unauthorized();
        }

        [Authorize(AuthenticationSchemes = "RefreshTokenScheme")]
        [HttpPost("accessToken")]
        public IActionResult GetAccessToken()
        {
            var claims = User.Claims.ToList();
            var accessToken = CreateJWT(claims);
            return Ok(accessToken);
        }

        private string CreateJWT(List<Claim> claims)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWTSecretKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "BurgerBabyApi",
                audience: "BurgerBaby",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("RefreshTokenScheme");

            return Ok(new { statusCode = 200, message = "登出成功" });
        }
    }


}


