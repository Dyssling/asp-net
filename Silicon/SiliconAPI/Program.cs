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
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //H�r s�tts default Authenticate schemat i applikationen, till ett Jwt scheme. N�r autentisering av n�gon form sker i applikationen kommer man allts� nu utg� fr�n ett Jwt scheme
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Vi ser �ven till att samma scheme anv�nds n�r vi eventuellt utf�r authentication challenges

}).AddJwtBearer(x => //H�r l�gger vi till och konfigurerar v�r JwtBearer
{
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = "SiliconAPI",

        ValidateAudience = true,
        ValidAudience = "SiliconAPI",

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Secret")!)), //Det �r standard practice att skapa en "Symmetric security key" av v�r secret, d� det bland annat g�r det sn�ppet s�krare

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
