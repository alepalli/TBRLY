using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TBRly.API.Repositories;
using TBRly.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi i servizi al contenitore
builder.Services.AddControllers();

// Aggiungi la configurazione per i repository e i servizi
builder.Services.AddSingleton<IBookRepository, BookRepository>();
builder.Services.AddScoped<BookService>();

// Aggiungi il supporto per la documentazione Swagger (opzionale, utile per testare le API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Costruisci l'app
var app = builder.Build();

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
