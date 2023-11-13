﻿using Blog.Entity.DTOs.Users;
using Blog.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-Posta adresiniz veya şifreniz yanlıştır.");
                        return View();
                    }
                }
                else
                {

                    ModelState.AddModelError("", "E-Posta adresiniz veya şifreniz yanlıştır.");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        [HttpGet]
        public  async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}