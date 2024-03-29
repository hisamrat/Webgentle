﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.Models;
using Webgentle.Repository;

namespace Webgentle.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [Route("signup")]
        
        public IActionResult Signup()
        {
            return View();
        }
        [Route("signup")]
        [HttpPost]
        public async Task< IActionResult> Signup(SignUpUserModel usermodel)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(usermodel);
                if(!result.Succeeded)
                {
                    foreach(var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    return View(usermodel);
                }
                ModelState.Clear();
            }
            return View();
        }
        [Route("login")]

        public IActionResult Login()
        {
            return View();
        }
        [Route("login")]
        [HttpPost]
        public async Task< IActionResult> Login(SignInModel signInModel,string returnUrl)
        {
            if(ModelState.IsValid)
            {
                
                var result = await _accountRepository.PasswordSignInAsync(signInModel);
                if(result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid");
            }
            return View(signInModel);
        }
        [Route("logout")]

        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Route("Change password")]

        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Route("Change password")]

        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountRepository.ChangePasswordAsync(model);
                if(result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                        ModelState.Clear();
                    return View();
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }
}
