using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TBRly.API.Data;
using TBRly.API.DTOs;
using TBRly.API.Services;

namespace TBRly.API.Controllers;

[ApiController]
[Route("api/[controller]")] // significa che il percorso base è api/users
// [controller] prende il nome della classe senza il suffisso Controller.
public class UsersController(UserService userService) : ControllerBase // Inietta il servizio tramite il costruttore
{
    private readonly UserService _userService = userService;
    private readonly IJwtService _jwtService;

    // torna tutti gli utenti
    [HttpGet]
    public IActionResult GetAllUsers() => Ok(_userService.GetAllUsers());

    // crea un nuovo utente
    [HttpPost]
    public IActionResult AddUser(UserDto user)
    {
        try
        {
            var newUser = _userService.AddUser(user);
            var userModel = _userService.MapToUser(newUser); // Esempio
            var token = _jwtService.GenerateJwtToken(userModel);
            return StatusCode(
                201,
                new
                {
                    message = "Registrazione completata con successo",
                    token = token,
                    user = newUser,
                }
            );
        }
        catch (ArgumentException ex)
        {
            // Ritorna 400 Bad Request: Dettagliato è MEGLIO per il frontend
            // (Il frontend ha bisogno di sapere il motivo per mostrarlo all'utente)
            return BadRequest(
                new
                {
                    message = ex.Message, // Messaggio: "L'indirizzo email è già registrato."
                }
            );
        }
        // GESTIONE DI ALTRI ERRORI INTERNI NON PREVISTI (es. problemi di hashing/DB)
        catch (Exception)
        {
            // Ritorna 500 Internal Server Error: generico
            return StatusCode(
                500,
                new
                {
                    message = "Si è verificato un errore interno durante la registrazione. Riprova più tardi.",
                }
            );
        }
    }

    // cerca un utente per id
    [HttpGet("{id}")]
    public IActionResult GetUserById(long id)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    // elimina un utente per id
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(long id)
    {
        _userService.DeleteUser(id);
        return Ok("Utente eliminato con successo");
    }

    // aggiorna un utente per id parzialmente
    [HttpPatch("{id}")]
    public IActionResult UpdateUser(long id, UserUpdateDto userUpdate)
    {
        var updatedUser = _userService.UpdateUser(id, userUpdate);
        if (updatedUser == null)
        {
            return NotFound();
        }
        return Ok(updatedUser);
    }

    // login
    [HttpPost("login")]
    public IActionResult Login(LoginDto loginDto)
    {
        Console.WriteLine($"Tentativo di login per l'utente: {loginDto.Username}");
        try
        {
            var token = _userService.Login(loginDto);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Username o password errati");
        }
    }
}
