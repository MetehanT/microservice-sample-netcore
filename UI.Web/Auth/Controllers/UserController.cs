using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI.Web.Auth.Models;
using UI.Web.Models.Authentication;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace UI.Web.Auth
{
    public class UserController : Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        RoleManager<AppRole> _roleManager;
        ILogger<UserController> _logger;
        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateVM model)
        {
            AppUser user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("{email} E-mail'ine sahip kullanıcı başarıyla oluşturuldu {date}", model.Email, DateTime.UtcNow);
                await _userManager.AddToRoleAsync(user, "User");
                return RedirectToAction("Login", "Auth");
            }
            return View(model);
        }
    }
}