using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TBRly.API.Models;

namespace TBRly.API.DTOs;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration; // Per accedere alle impostazioni di configurazione

    // IConfiguration serve per leggere le impostazioni dal file appsettings.json
    // (per esempio la chiave segreta del JWT, l'issuer e l'audience)

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration; // Inietta la configurazione
    }

    // Metodo che crea un JWT a partire da un utente
    public string GenerateJwtToken(User user)
    {
        // 👤 1) CREAZIONE DELLE CLAIMS
        // Le "claims" sono informazioni sull'utente che inseriamo dentro al token,
        // come username, email, ruolo, ecc. Servono a identificare l'utente senza
        // dover andare ogni volta sul DB.
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username), // "sub" è lo standard per identificare l'utente
            new Claim(JwtRegisteredClaimNames.Email, user.Email), // inserisco anche l'email
            new Claim(ClaimTypes.Role, user.Role.ToString()), // aggiungo il ruolo (es. Admin, User)
        };
        // 🔑 2) CREAZIONE DELLA CHIAVE SEGRETA
        // Il token deve essere firmato digitalmente, per garantire che nessuno lo modifichi.
        // La chiave segreta è salvata nel file appsettings.json in "Jwt:Key".
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        // 📜 3) CREDENZIALI DI FIRMA
        // Indico con quale algoritmo firmare il token (qui HmacSha256)
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 🎫 4) CREAZIONE DEL TOKEN JWT
        // Definisco i dati principali del token:

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"], // - Issuer: chi emette il token (la tua API)
            audience: _configuration["Jwt:Audience"], // - Audience: chi può usare il token (per esempio un client specifico)
            claims: claims, // - Claims: le informazioni sull’utente
            expires: DateTime.Now.AddHours(2), // - Expiration: scadenza del token (qui 2 ore)
            signingCredentials: creds // - Credentials: firma digitale con la chiave segreta
        );
        // 🔄 5) CONVERTO L'OGGETTO IN STRINGA
        // JwtSecurityTokenHandler serve a trasformare l’oggetto token
        // in una stringa compatta (tipo "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...")
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
