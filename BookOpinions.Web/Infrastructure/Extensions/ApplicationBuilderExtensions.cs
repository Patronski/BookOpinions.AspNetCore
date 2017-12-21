using BookOpinions.Data;
using BookOpinions.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BookOpinions.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<BookOpinionsDbContext>();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                    {
                        string[] roles =
                        {
                            WebConstants.AdminRole,
                            WebConstants.BooksModeratorRole,
                            WebConstants.CommentsModeratorRole,
                            WebConstants.ShopModeratorRole
                        };

                        foreach (var role in roles)
                        {
                            var roleExists = await roleManager.RoleExistsAsync(role);

                            if (!roleExists)
                            {
                                await roleManager.CreateAsync(new IdentityRole
                                {
                                    Name = role
                                });
                            }
                        }

                        var adminEmail = "donstz@yahoo.com";

                        var adminUser = await userManager.FindByEmailAsync(adminEmail);

                        if (adminUser == null)
                        {
                            adminUser = new User
                            {
                                Name = "Admin",
                                Birtdate = new DateTime(1988, 08, 29, 00, 00, 00),
                                Email = adminEmail,
                                UserName = WebConstants.AdminRole
                            };

                            await userManager.CreateAsync(adminUser, "admin");

                            await userManager.AddToRoleAsync(adminUser, WebConstants.AdminRole);
                        }
                    })
                    .Wait();
            }

            return app;
        }
    }
}
