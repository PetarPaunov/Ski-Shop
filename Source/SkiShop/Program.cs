using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SkiShop.Core.Contracts;
using SkiShop.Core.Contracts.Admin;
using SkiShop.Core.Contracts.Common;
using SkiShop.Core.Contracts.ProductContracts;
using SkiShop.Core.Services;
using SkiShop.Core.Services.Admin;
using SkiShop.Core.Services.Common;
using SkiShop.Core.Services.ProductServices;
using SkiShop.Data;
using SkiShop.Data.Common;
using SkiShop.Data.Models.Account;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 5;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

builder.Services.AddControllersWithViews();

builder.Services.AddApplicationServices();

var cloudName = builder.Configuration.GetValue<string>("AccountSettings:ColudName");
var apiKey = builder.Configuration.GetValue<string>("AccountSettings:ApiKey");
var apiSecret = builder.Configuration.GetValue<string>("AccountSettings:ApiSecret");

//if (new[] { cloudName, apiKey, apiSecret }.Any(string.IsNullOrEmpty))
//{
//    throw new ArgumentException("Please specify Cloudinary accont details");
//}

builder.Services.AddSingleton(new Cloudinary(new Account(cloudName, apiKey, apiSecret)));

builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = "793582705075323";
        options.AppSecret = "0bf5265e61ddc6e2cf5007a5390b0f9b";
    })
    .AddTwitter(options =>
    {
        options.ConsumerKey = "Test";
        options.ConsumerSecret = "Test";
    })
    .AddGoogle(options =>
    {
        options.ClientId = "647563805482-o8j443o76u2ktshsok3v6ainrq792234.apps.googleusercontent.com";
        options.ClientSecret = "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";
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "Admin",
      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapRazorPages();

app.Run();