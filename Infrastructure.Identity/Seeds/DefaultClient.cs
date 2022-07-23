using Core.Application.Enums;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultClient
    {
        public static async Task Seeds(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser applicationUser = new()
            {
                Name = "Ramon",
                UserName = "Ramonson",
                Email = "",
                PhoneNumber = "8095432231",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

            };
            if(userManager.Users.All(user=>user.Id != applicationUser.Id))
            {
                var user = await userManager.FindByEmailAsync(applicationUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(applicationUser,"123Pasword!");
                    await userManager.AddToRoleAsync(applicationUser, Roles.Client.ToString());
                }

            }
        }
    }
}
