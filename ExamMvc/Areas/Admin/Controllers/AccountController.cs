using Core.Models;
using ExamMvc.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> CreateAdmin()
        {
            AppUser admin = new AppUser
            {
                UserName = "ZamanAdmin",
                Fullname = "Zaman Safarov",
            };

            var result = await _userManager.CreateAsync(admin, "Admin123@");
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var roleResult = await _userManager.AddToRoleAsync(admin, "SuperAdmin");
            if (!roleResult.Succeeded)
            {
                return BadRequest(roleResult.Errors);
            }

            return Ok("Admin created successfully");

        }

        public async Task<IActionResult> CreateRoles()
        {
            IdentityRole role1 = new IdentityRole("Admin");
            IdentityRole role2 = new IdentityRole("Member");
            await _roleManager.CreateAsync(role1);
            await _roleManager.CreateAsync(role2);


            return Ok("rolllar yarandi");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginVM admin)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByNameAsync(admin.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "User Name or Password is not Valid!");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user, admin.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "User Name or Password is not Valid!");
                return View();

            }
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("SuperAdmin"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "User does not have the required role!");
                return View();
            }
        }
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
