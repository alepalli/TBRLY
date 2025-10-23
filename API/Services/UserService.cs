using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using TBRly.API.DTOs;
using TBRly.API.Models;
using TBRly.API.Repositories;

namespace TBRly.API.Services;

public class UserService
{
    private readonly IUserRepository _repo;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly IJwtService _jwtService;

    public UserService(IUserRepository repo, IJwtService jwtService)
    {
        _repo = repo; // Inietta il repository
        _passwordHasher = new PasswordHasher<User>();
        _jwtService = jwtService;
    }

    public List<UserDto> GetAllUsers() => _repo.GetAllUsers().ConvertAll(MapToUserDto);

    public UserDto AddUser(UserDto userDto)
    {
        var errors = new List<string>();

        // VERIFICA 1: EMAIL DUPLICATA
        var existingEmailUser = _repo.GetUserByEmail(userDto.Email);
        if (existingEmailUser != null)
        {
            errors.Add("L'indirizzo email è già registrato.");
        }

        // VERIFICA 2: USERNAME DUPLICATO
        var existingUsernameUser = _repo.GetUserByUsername(userDto.Username);
        if (existingUsernameUser != null)
        {
            errors.Add("Lo username è già in uso. Scegline un altro.");
        }

        // 🛑 PUNTO CRUCIALE: Se ci sono errori, lancia una singola stringa multi-linea.
        if (errors.Count > 0)
        {
            // Usa Environment.NewLine, che si traduce in \r\n su Windows,
            // ma viene comunque gestito dal browser come una separazione logica.
            // Se \n non funziona, prova un separatore custom (vedi sotto).
            throw new ArgumentException(string.Join(Environment.NewLine, errors));
        }

        // ... (restante logica: mappatura, hashing, salvataggio) ...
        var newUser = MapToUser(userDto);
        newUser.Password = _passwordHasher.HashPassword(newUser, newUser.Password);
        return MapToUserDto(_repo.AddUser(newUser));
    }

    public User? GetUserById(long id) => _repo.GetUserById(id);

    public void DeleteUser(long id) => _repo.DeleteUser(id);

    public User? UpdateUser(long id, UserUpdateDto userUpdate)
    {
        var existingUser = _repo.GetUserById(id);
        if (existingUser == null)
        {
            return null;
        }

        // Aggiorna solo i campi che non sono null
        if (userUpdate.Username != null)
        {
            existingUser.Username = userUpdate.Username;
        }
        if (userUpdate.Email != null)
        {
            existingUser.Email = userUpdate.Email;
        }
        Console.WriteLine(userUpdate.Password);
        if (userUpdate.Password != null)
        {
            existingUser.Password = _passwordHasher.HashPassword(existingUser, userUpdate.Password);
        }
        if (userUpdate.BirthDate.HasValue)
        {
            existingUser.BirthDate = userUpdate.BirthDate.Value;
        }
        if (userUpdate.Role.HasValue)
        {
            existingUser.Role = userUpdate.Role.Value;
        }
        if (userUpdate.ProfilePictureUrl != null)
        {
            existingUser.ProfilePictureUrl = userUpdate.ProfilePictureUrl;
        }
        if (userUpdate.Bio != null)
        {
            existingUser.Bio = userUpdate.Bio;
        }

        existingUser.UpdatedAt = DateTime.UtcNow; // Aggiorna il timestamp

        return _repo.UpdateUser(existingUser);
    }

    public User? Autheticate(string username, string password)
    {
        var user = _repo.GetAllUsers().Find(u => u.Username == username); // Cerca l'utente per username
        if (user == null)
        {
            return null; // Utente non trovato
        }
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password); // Verifica la password
        return result == PasswordVerificationResult.Success ? user : null; // Ritorna l'utente se la password è corretta, altrimenti null
    }

    public string Login(LoginDto loginDto)
    {
        var user = Autheticate(loginDto.Username, loginDto.Password);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Username o password errati");
        }
        return _jwtService.GenerateJwtToken(user); // Genera e ritorna nella stessa riga il token JWT
    }

    // Mappa UserDto a User
    public User MapToUser(UserDto user) =>
        new User
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            BirthDate = user.BirthDate,
            Role = user.Role,
            ProfilePictureUrl = user.ProfilePictureUrl,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            Bio = user.Bio,
        };

    // Mappa User a UserDto
    public UserDto MapToUserDto(User user) =>
        new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            BirthDate = user.BirthDate,
            Role = user.Role,
            ProfilePictureUrl = user.ProfilePictureUrl,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            Bio = user.Bio,
        };
}
