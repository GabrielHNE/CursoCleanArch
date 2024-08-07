using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthenticateService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var res = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

            return res.Succeeded;
        }

        public async Task<bool> ResgisterUser(string email, string password)
        {
            var applicationUser = new ApplicationUser{
                UserName = email,
                Email = email    
            };

            var res = await _userManager.CreateAsync(applicationUser, password);

            if(res.Succeeded){
                await _signInManager.SignInAsync(applicationUser, isPersistent: false);
            }

            return res.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}