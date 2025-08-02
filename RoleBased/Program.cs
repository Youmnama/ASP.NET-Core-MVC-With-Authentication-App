using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoleBased.Data;
using RoleBased.Models;
using RoleBased.Services;





var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(op =>
            op.UseSqlServer(builder.Configuration.GetConnectionString("Server=.\\SQLEXPRESS;Database=RoleBased;integrated security=sspi;trustservercertificate=true;encrypt=true")));
builder.Services.AddIdentity<User,IdentityRole>(options => { //Extra data about User
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options .User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail= false;
    options.SignIn.RequireConfirmedPhoneNumber = false;










}
).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();









var app = builder.Build();
             await SeedService.SeedData(app.Services);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
       
