using Microsoft.EntityFrameworkCore;                 // <- UseSqlite
using Emi.Application.Abstractions.Persistence;      // <- IAppDbContext
using Emi.Infrastructure.Data;                       // <- EmiDbContext
using Emi.Infrastructure.Data.Seeds;                 // <- SeedData

var builder = WebApplication.CreateBuilder(args);
var dbPath = Path.Combine(AppContext.BaseDirectory, "emi.db");
var cs = builder.Configuration.GetConnectionString("Default") ?? $"Data Source={dbPath}";
builder.Services.AddDbContext<EmiDbContext>(o => o.UseSqlite(cs));

// Exponer la interfaz de persistencia
builder.Services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<EmiDbContext>());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => Results.Ok("API up"));

// ---- SEED (creación/migración + datos básicos) ----
await SeedData.EnsureSeededAsync(app.Services);

app.Run();
