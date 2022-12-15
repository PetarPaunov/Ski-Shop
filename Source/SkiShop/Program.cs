using SkiShop.Data;
using CloudinaryDotNet;
using SkiShop.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiShop.Core.Models.EmailViewModels;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 5;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var emailConfig = builder.Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();

builder.Services.AddSingleton(emailConfig);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

builder.Services.AddControllersWithViews(option =>
{
    option.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services.AddApplicationServices();

var cloudName = builder.Configuration.GetValue<string>("CloudAccountSettings:CloudName");
var apiKey = builder.Configuration.GetValue<string>("CloudAccountSettings:ApiKey");
var apiSecret = builder.Configuration.GetValue<string>("CloudAccountSettings:ApiSecret");

builder.Services.AddSingleton(new Cloudinary(new Account(cloudName, apiKey, apiSecret)));

builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = builder.Configuration.GetValue<string>("ExternalLoginCredentials:FacebookAppId");
        options.AppSecret = builder.Configuration.GetValue<string>("ExternalLoginCredentials:FacebookAppSecret");
    })
    .AddMicrosoftAccount(options =>
    {
        options.ClientId = builder.Configuration.GetValue<string>("ExternalLoginCredentials:MicrosoftClientId");
        options.ClientSecret = builder.Configuration.GetValue<string>("ExternalLoginCredentials:MicrosoftClientSecret");
    })
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration.GetValue<string>("ExternalLoginCredentials:GoogleClientId");
        options.ClientSecret = builder.Configuration.GetValue<string>("ExternalLoginCredentials:GoogleClientSecret");
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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

app.UseSession();

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