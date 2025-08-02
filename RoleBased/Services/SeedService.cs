using Microsoft.AspNetCore.Identity;
using RoleBased.Data;
using RoleBased.Models;

namespace RoleBased.Services
{
    public class SeedService
    {
        public static async  Task SeedData( IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();
            try {
                //Ensure Database is created and Ready for seeding
                logger.LogInformation("Seeding database...");               
                await context.Database.EnsureCreatedAsync();
                //Add Role
                logger.LogInformation("Adding roles...");
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("User"));
                //Add Admin User
                logger.LogInformation("Adding admin user...");
                var adminEmail = "youmnaelkot31@gmail.com";
                if (await userManager.FindByEmailAsync(adminEmail)==null)
                {
                    var adminUser = new User
                    {
                        FullName = "Youmna",
                        UserName= adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true,
                        NormalizedEmail = adminEmail.ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString(),



                    };
                    var result = await userManager.CreateAsync(adminUser, "Admin@123");
                    if (result.Succeeded)
                    {
                        logger.LogInformation("Admin user created successfully.");
                        //Assign Admin Role
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                        logger.LogInformation("Admin role assigned to the admin user.");
                    }
                    else
                    {
                        logger.LogError("Failed to create admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    }

                }
               
                

            
            
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
            }

        }
        private static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager ,string roleName)
        {
            if (! await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role {roleName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    
                }

            }

        }
    }
}
