﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.Models;
using Webgentle.Services;

namespace Webgentle.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;

        public AccountRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }
        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel usermodel)
        {
            var user = new ApplicationUser()
            { 
              FirstName=usermodel.FirstName,
              LastName=usermodel.LastName,
              Email=usermodel.Email,
              UserName=usermodel.Email
            };
          var result=await  _userManager.CreateAsync(user, usermodel.Password);

            return result;
        }
        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false);
       return result;
                
                }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var userid = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userid);
           return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }
    }
}
