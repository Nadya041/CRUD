using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Web
{
    public static class MyExtension
    {
        public static void CreateUserRoles<T>(this IApplicationBuilder app) where T : DbContext
        {
            IServiceScopeFactory scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            using (IServiceScope scope = scopeFactory.CreateScope())
            {
                scope.ServiceProvider.GetService<T>().Database.Migrate();

                RoleManager<IdentityRole<int>> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
                UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                Task.Run(async () =>
                {
                    IdentityResult roleRes;
                    var roleCheck = await roleManager.RoleExistsAsync("Admin");
                    if (!roleCheck)
                    {
                        roleRes = await roleManager.CreateAsync(new IdentityRole<int> { Name = "Admin" });
                    }

                    roleCheck = await roleManager.RoleExistsAsync("User");
                    if (!roleCheck)
                    {
                        roleRes = await roleManager.CreateAsync(new IdentityRole<int> { Name = "User" });
                    }

                    User user = await userManager.FindByEmailAsync("admin@gmail.com");
                    if (user == null)
                    {
                        var u = new User()
                        {
                            Email = "admin@gmail.com",                            
                            Name = "Admin Name",
                            Phone = "Admin Phone",
                            UserName = "admin@gmail.com",
                            SecurityStamp = Guid.NewGuid().ToString()
                        };

                        await userManager.CreateAsync(u, "adminpass");

                        await userManager.AddToRoleAsync(u, "Admin");
                    }
                }).Wait();
            }

        }
    }
}
