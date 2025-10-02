using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using TBRly.API.Data;
using TBRly.API.Repositories;
using TBRly.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TBRly.API.DTOs;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
); // registra il contesto del database con la stringa di connessione

// Aggiungi i servizi al contenitore
builder.Services.AddControllers();

// Aggiungi la configurazione per i repository e i servizi
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<BookService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IJwtService, JwtService>(); // registra il servizio JWT

// Aggiungi il servizio JWT
builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            ),
        };
    });

// Aggiungi il supporto per la documentazione Swagger (opzionale, utile per testare le API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Costruisci l'app
var app = builder.Build();

// Applica le migrazioni del database all'avvio dell'applicazione
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Abilita Swagger per la documentazione e l'interfaccia utente
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Abilita il supporto per il routing e i controller
// app.UseRouting(); non serve quando uso mapControllers
app.MapControllers();

// Abilita l'autenticazione e l'autorizzazione
app.UseAuthentication();
app.UseAuthorization();

// Avvia l'applicazione
app.Run();
