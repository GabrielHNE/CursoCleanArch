using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, 
                                    RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void SeedRoles()
        {
            if(!_roleManager.RoleExistsAsync("User").Result){
                IdentityRole role = new();
                role.Name = "User";
                role.NormalizedName = "USER";

                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }

            if(!_roleManager.RoleExistsAsync("Admin").Result){
                IdentityRole role = new();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";

                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUsers()
        {
            var email = "usuario@localhost";
            if(_userManager.FindByEmailAsync(email).Result == null)
            {
                ApplicationUser user = new ();

                user.UserName = email;
                user.Email = email;
                user.NormalizedUserName = email.ToUpper();
                user.NormalizedEmail = email.ToUpper();
                user.EmailConfirmed = true;
                user.LockoutEnabled = true;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2024").Result;

                if(result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            email = "admin@localhost";
            if(_userManager.FindByEmailAsync(email).Result == null)
            {
                ApplicationUser user = new ();

                user.UserName = email;
                user.Email = email;
                user.NormalizedUserName = email.ToUpper();
                user.NormalizedEmail = email.ToUpper();
                user.EmailConfirmed = true;
                user.LockoutEnabled = true;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2024").Result;

                if(result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}