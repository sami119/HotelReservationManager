using HotelReservationManager.Areas.Identity.Data;
using HotelReservationManager.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationManager.Areas
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var context = provider.GetRequiredService<HotelReservationManagerDBContext>();
                var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = provider.GetRequiredService<UserManager<HotelManagerUser>>();

                // automigration 
                context.Database.Migrate();
                AddRoles(roleManager, "Admin");
                AddRoles(roleManager, "Client");

                AssigneeRole(userManager, "Admin");
            }
        }

        private static void AddRoles(RoleManager<IdentityRole> roleManager, string name)
        {
            var roleExist = roleManager.RoleExistsAsync(name).Result;
            if (!roleExist)
            {
                //create the roles and seed them to the database
                roleManager.CreateAsync(new IdentityRole(name)).GetAwaiter().GetResult();
            }
        }

        private static void AssigneeRole(UserManager<HotelManagerUser> userManager, string roleName)
        {
            if (userManager.FindByNameAsync("user1").Result == null)
            {
                HotelManagerUser user = new HotelManagerUser()
                {
                    UserName = "Admin",
                    Email = "admin@localhost.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "Admin1234!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, roleName).Wait();
                }
            }
        }
    }
}
