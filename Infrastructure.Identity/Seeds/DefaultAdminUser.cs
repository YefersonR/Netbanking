﻿using Core.Application.Enums;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task Seeds(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser applicationAdmin = new()
            {
                Name = "Admin",
                LastName = "Administrador",
                UserName = "Admin",
                Email = "Admin@gmai.com",
                PhoneNumber = "8096534321",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
        
            if(userManager.Users.All(user=>user.Id != applicationAdmin.Id))
            {
                var user = await userManager.FindByEmailAsync(applicationAdmin.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(applicationAdmin,"123Pa$$word!");
                    await userManager.AddToRoleAsync(applicationAdmin,Roles.Admin.ToString());
                }
            }
        }
    }
}
