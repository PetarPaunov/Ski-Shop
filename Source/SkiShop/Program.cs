using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkiShop.Data;
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

var cloudName = builder.Configuration.GetValue<string>("CloudAccountSettings:ColudName");
var apiKey = builder.Configuration.GetValue<string>("CloudAccountSettings:ApiKey");
var apiSecret = builder.Configuration.GetValue<string>("CloudAccountSettings:ApiSecret");

//if (new[] { cloudName, apiKey, apiSecret }.Any(string.IsNullOrEmpty))
//{
//    throw new ArgumentException("Please specify Cloudinary accont details");
//}

builder.Services.AddSingleton(new Cloudinary(new Account(cloudName, apiKey, apiSecret)));

builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = builder.Configuration.GetValue<string>("ExternalLoginCridentials:FacebookAppId");
        options.AppSecret = builder.Configuration.GetValue<string>("ExternalLoginCridentials:FacebookAppSecret");
    })
    .AddTwitter(options =>
    {
        options.ConsumerKey = "Test";
        options.ConsumerSecret = "Test";
    })
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration.GetValue<string>("ExternalLoginCridentials:GoogleClientId");
        options.ClientSecret = builder.Configuration.GetValue<string>("ExternalLoginCridentials:GoogleClientSecret");
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