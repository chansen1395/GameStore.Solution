using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GameStore.Models;
using Microsoft.AspNetCore.Identity;
// using System;
// using System.Threading.Tasks;

namespace GameStore
{
  public class Startup
  {
    public Startup(IWebHostEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json");
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddControllersWithViews();
      services.AddRazorPages();

      services.AddAuthorization(options =>
      {
      options.AddPolicy("RequireAdministratorRole",
      policy => policy.RequireRole("Administrator"));
      });

      services.AddEntityFrameworkMySql()
        .AddDbContext<GameStoreContext>(options => options
        .UseMySql(Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(Configuration["ConnectionStrings:DefaultConnection"])));

      //new code
      services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<GameStoreContext>()
                .AddDefaultTokenProviders();

      services.Configure<IdentityOptions>(options =>
      {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 0;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredUniqueChars = 0;
      });
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseDeveloperExceptionPage();

      //new code
      app.UseAuthentication();

      app.UseRouting();

      //new code
      app.UseAuthorization();

      app.UseEndpoints(routes =>
      {
        routes.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
      });

      app.UseStaticFiles();

      app.Run(async (context) =>
      {
        await context.Response.WriteAsync("Hello World!");
      });
    }
    
    // *******************************************
    // private async Task CreateRoles(IServiceProvider serviceProvider)
    // {
        //initializing custom roles 
        // var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        // var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        // string[] roleNames = { "Adminis", "Store-Manager", "Member" };
        // IdentityResult roleResult;

    // foreach (var roleName in roleNames)
    // {
    //     var roleExist = await RoleManager.RoleExistsAsync(roleName);
    //     // ensure that the role does not exist
    //     if (!roleExist)
    //     {
    //         //create the roles and seed them to the database: 
    //         roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
    //     }
    // }

    // find the user with the admin email 
    // var _user = await UserManager.FindByEmailAsync("admin@email.com");

    // // check if the user exists
    // if(_user == null)
    // {
    //     //Here you could create the super admin who will maintain the web app
    //     var poweruser = new ApplicationUser
    //     {
    //         UserName = "Administrator",
    //         Email = "admin@email.com",
    //     };
    //     string adminPassword = "p@$$w0rd";

    //     var createPowerUser = await UserManager.CreateAsync(poweruser, adminPassword);
    //     if (createPowerUser.Succeeded)
    //     {
    //         //here we tie the new user to the role
    //         await UserManager.AddToRoleAsync(poweruser, "Administrator");

    //     }
    // }
  // }
  }
}