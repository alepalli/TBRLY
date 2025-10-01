using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TBRly.API.Data;
using TBRly.API.Repositories;
using TBRly.API.Services;

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
app.UseRouting();
app.MapControllers();

// Avvia l'applicazione
app.Run();
