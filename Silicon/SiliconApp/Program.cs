using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiliconApp.Contexts;
using SiliconApp.Entities;
using SiliconApp.Repositories;
using SiliconApp.Services;
using SiliconApp.Helpers.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.Password.RequiredLength = 8;

}).AddEntityFrameworkStores<DataContext>();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.Cookie.HttpOnly = true;
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    x.LoginPath = "/account/signin";
    x.LogoutPath = "/account/signout";
    x.SlidingExpiration = true;


});

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<UserService>();

string appId = "";
string appSecret = "";
string clientId = "";
string clientSecret = "";

builder.Services.AddAuthentication()

    .AddFacebook(x =>
    {
        x.AppId = appId;
        x.AppSecret = appSecret;
    })
    .AddGoogle(x =>
    {
        x.ClientId = clientId;
        x.ClientSecret = clientSecret;
    });

var app = builder.Build();

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseUserSessionValidation();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStatusCodePagesWithRedirects("/Error/{0}"); //När det sker en error med en statuskod så dirigeras man till denna URL:en. Man kommer då hamna i ErrorController

app.Run();
