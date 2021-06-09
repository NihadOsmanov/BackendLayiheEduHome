﻿using Layihe.Models;
using Layihe.Utils;
using Layihe.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginView)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var existUser = await _userManager.FindByNameAsync(loginView.UserName);
            if(existUser == null)
            {
                ModelState.AddModelError("", "Email or password invalid");
                return View();
            }

            var loginResult = await _signInManager.PasswordSignInAsync(existUser, loginView.Password, loginView.RememberMe, true);
            if (loginResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Your account is locked out. Kindly wait for 10 minutes and try again");
                return View();
            }

            if (!loginResult.Succeeded)
            {
                ModelState.AddModelError("", "Email or password invalid");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerView)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var dbUser = await _userManager.FindByNameAsync(registerView.UserName);
            if(dbUser != null)
            {
                ModelState.AddModelError("Username", "Bu adda user var");
                return View();
            }

            var newUser = new User
            {
                UserName = registerView.UserName,
                Name = registerView.Name,
                Surname = registerView.SurName,
                Email = registerView.Email,
            };

            var identityresult = await _userManager.CreateAsync(newUser, registerView.Password);
            if (!identityresult.Succeeded)
            {
                foreach (var error in identityresult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await _signInManager.SignInAsync(newUser, true);

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ForgotPassword(string id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            if (string.IsNullOrEmpty(forgotPassword.Email))
            {
                ModelState.AddModelError("Email", " Email bos ola bilmez");
                return View();
            }

            if (forgotPassword == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "Bele bir Account yoxdur");
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            string href = Url.Action("ResetPassword", new { userEmail = forgotPassword.Email, token });

            string url = "https://localhost:44364" + href;
            string subject = "ResetPassword";
            string msgBody = $"<a href={url}>Sifreni yenilem ucun toxunun</a> ";
            string mail = forgotPassword.Email;

            await Helper.SendMessage(subject, msgBody, mail);
            TempData["Email"] = forgotPassword.Email;
            TempData["Token"] = token;

            return RedirectToAction("Login");
        }
        public IActionResult ResetPassword(string userEmail, string token)
        {
            if ((string)TempData["Email"] != userEmail || (string)TempData["Token"] != token)
            {
                return BadRequest();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string userEmail, string token, ResetPasswordViewModel resetPassword)
        {
            if (string.IsNullOrEmpty(userEmail))
                return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var dbUser = await _userManager.FindByEmailAsync(userEmail);
            if (dbUser == null)
            {
                return BadRequest();
            }

            var result = await _userManager.ResetPasswordAsync(dbUser, token, resetPassword.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            return RedirectToAction("Login");
        }   
    }
}