using System;
using Microsoft.AspNetCore.Mvc;
using TBRly.API.DTOs;
using TBRly.API.Services;

namespace TBRly.API.Controllers;

[ApiController]
[Route("api/[controller]")] // significa che il percorso base è api/users
// [controller] prende il nome della classe senza il suffisso Controller.
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    // torna tutti gli utenti
    [HttpGet]
    public IActionResult GetAllUsers() => Ok(_userService.GetAllUsers());

    // crea un nuovo utente
    [HttpPost]
    public IActionResult AddUser(UserDto user)
    {
        var newUser = _userService.AddUser(user);
        return CreatedAtAction(nameof(GetAllUsers), new { id = 1 }, null); // Esempio di risposta
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
}
