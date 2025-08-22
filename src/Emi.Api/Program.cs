using System.Text;
using Emi.Infrastructure.Options;
using Emi.Application.Abstractions.Persistence;
using Emi.Application.Abstractions.Security;
using Emi.Infrastructure.Data;
using Emi.Infrastructure.Data.Seeds;
using Emi.Infrastructure.Identity.Models;
using Emi.Infrastructure.Identity.Seeds;
using Emi.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// DbContext
var cs = builder.Configuration.GetConnectionString("Default") ?? "Data Source=emi.db";
builder.Services.AddDbContext<EmiDbContext>(o => o.UseSqlite(cs));
builder.Services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<EmiDbContext>());

// Identity
builder.Services.AddIdentityCore<AppUser>(opts =>
{
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequiredLength = 6;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<EmiDbContext>();

// JWT options
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));
var jwt = builder.Configuration.GetSection("JWT").Get<JwtOptions>() ?? new JwtOptions();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
        };
    });
builder.Services.AddAuthorization();

// JWT service
builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => Results.Ok("API up"));

// Seeds
await SeedData.EnsureSeededAsync(app.Services);
await IdentitySeed.EnsureSeededAsync(app.Services);

app.Run();