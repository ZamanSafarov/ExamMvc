using Core.Models;
using ExamMvc.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Pronia.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterVm memberRegisterVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByNameAsync(memberRegisterVM.UserName);

            if (user is not null)
            {
                ModelState.AddModelError("UserName", "Username is Already Taken!");
                return View();
            }

            user = await _userManager.FindByEmailAsync(memberRegisterVM.Email);

            if (user is not null)
            {
                ModelState.AddModelError("Email", "Email is Already Taken!");
                return View();
            }

            user = new AppUser()
            {
                Fullname = memberRegisterVM.FullName,
                UserName = memberRegisterVM.UserName,
                Email = memberRegisterVM.Email,
            };

            var result = await _userManager.CreateAsync(user, memberRegisterVM.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View();
                }
            }
            await _userManager.AddToRoleAsync(user, "Member");
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(MemberLoginVm memberLoginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByEmailAsync(memberLoginVM.Email);

            if (user is null)
            {
                ModelState.AddModelError("", "Email or Password is not Correct");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, memberLoginVM.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or Password is not Correct");
                return View();
            }
            //var callbackUrl = Request.Query["ReturnUrl"];
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "home");
        }
    }
}