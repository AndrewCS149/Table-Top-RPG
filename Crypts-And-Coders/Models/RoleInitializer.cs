using Crypts_And_Coders.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crypts_And_Coders.Models
{
    public class RoleInitializer
    {
        // Create a list of identity roles
        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole{Name = ApplicationRoles.GameMaster, NormalizedName = ApplicationRoles.GameMaster.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString()},
            new IdentityRole{Name = ApplicationRoles.Player, NormalizedName = ApplicationRoles.Player.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString()},
        };

        public static void SeedData(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, IConfiguration _config)
        {
            using (var dbContext = new CryptsDbContext(serviceProvider.GetRequiredService<DbContextOptions<CryptsDbContext>>()))
            {
                dbContext.Database.EnsureCreated();
                AddRoles(dbContext);
                SeedUsers(userManager, _config);
            }
        }

        public static void AddRoles(CryptsDbContext dbContext)
        {
            if (dbContext.Roles.Any()) return;

            foreach (var role in Roles)
            {
                dbContext.Roles.Add(role);
                dbContext.SaveChanges();
            }
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager, IConfiguration _config)
        {
            if (userManager.FindByNameAsync(_config["AdminUserName"]).Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = _config["AdminUserName"];
                IdentityResult result = userManager.CreateAsync(user, _config["AdminPassword"]).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, ApplicationRoles.GameMaster);
                }
            }
        }
    }
}