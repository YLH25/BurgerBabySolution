using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BurgerBaby.Models.EFModel;
using BurgerBaby.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BurgerBaby.Models.Services.Interface;


namespace BurgerBaby.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IRoleService _roleService;
        public HomeController(IMemberService memberService,IRoleService roleService)
        {
            _memberService = memberService;
            _roleService= roleService;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
           return (User.Identity != null && User.Identity.IsAuthenticated)? RedirectToAction("Index"): View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Login(MemberVM model)
        {
            var valid = await _memberService.ValidateMemberAsync(model.Email, model.Password);


            if (valid != null)
            {
                var member = valid;
                var role=await _roleService.GetRoleByIdAsync(member.RoleId);
                if (role != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Email),
                    new Claim(ClaimTypes.Role,role.RoleName)
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);


                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "帳密不對");

            return View(model);
        }
        public IActionResult Logout() {
           HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }
        public IActionResult Error(string errorMessege) { 
        ViewBag.ErrorMessege = errorMessege;    
        return View();  
        }
        public IActionResult AccessDenied() { 
        return View();
        }
    }
}
