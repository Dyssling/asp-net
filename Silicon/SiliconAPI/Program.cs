using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using SiliconAPI.Contexts;
using SiliconAPI.Repositories;
using SiliconAPI.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //Här sätts default Authenticate schemat i applikationen, till ett Jwt scheme. När autentisering av någon form sker i applikationen kommer man alltså nu utgå från ett Jwt scheme
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Vi ser även till att samma scheme används när vi eventuellt utför authentication challenges

}).AddJwtBearer(x => //Här lägger vi till och konfigurerar vår JwtBearer
{
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = "SiliconAPI",

        ValidateAudience = true,
        ValidAudience = "SiliconAPI",

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Secret")!)), //Det är standard practice att skapa en "Symmetric security key" av vår secret, då det bland annat gör det snäppet säkrare

        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromSeconds(10)
    };
});
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<CourseService>();

var app = builder.Build();

app.UseCors(x =>
{
    x.AllowAnyHeader();
    x.AllowAnyOrigin();
    x.AllowAnyMethod();
});
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
