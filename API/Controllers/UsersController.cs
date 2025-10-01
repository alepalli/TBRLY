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

    [HttpGet]
    public IActionResult GetAllUsers() => Ok(_userService.GetAllUsers());

    [HttpPost]
    public IActionResult AddUser(UserDto user)
    {
        Console.WriteLine("sono qua");
        var newUser = _userService.AddUser(user);
        return CreatedAtAction(nameof(GetAllUsers), new { id = 1 }, null); // Esempio di risposta
    }
}
