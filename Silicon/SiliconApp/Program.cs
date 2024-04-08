using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiliconApp.Contexts;
using SiliconApp.Entities;
using SiliconApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.Password.RequiredLength = 8;

}).AddEntityFrameworkStores<DataContext>();

builder.Services.AddScoped<UserService>();

var app = builder.Build();

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseStatusCodePagesWithRedirects("/Error/{0}"); //När det sker en error med en statuskod så dirigeras man till denna URL:en. Man kommer då hamna i ErrorController

app.Run();
