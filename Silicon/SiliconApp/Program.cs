var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllersWithViews();

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
