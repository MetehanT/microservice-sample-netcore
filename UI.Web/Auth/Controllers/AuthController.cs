using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI.Web.Auth.Models;
using UI.Web.Models.Authentication;
using Microsoft.Extensions.Logging;

namespace UI.Web.Auth.Controllers
{
    public class AuthController : Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        ILogger<UserController> _logger;
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            //var userClaims = await _userManager.GetClaimsAsync(user);
            if (user != null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Kullanıcı başarıyla giriş yaptı {date} {email}", DateTime.UtcNow, model.Email);
                    /*Claim claim = new Claim("pozisyon", "admin");
                    if (!userClaims.Any(x => x.Type == "pozisyon"))
                        await _userManager.AddClaimAsync(user, claim);*/
                    return RedirectToAction("Index", "Product");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Denied()
        {
            return View();
        }
    }
}