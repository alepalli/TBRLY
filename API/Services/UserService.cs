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

    public UserDto AddUser(UserDto userDto) // Il tuo metodo "AddUser"
    {
        // 🛑 STEP 1: VERIFICA DI DUPLICAZIONE DELL'EMAIL (tramite il Repository)
        var existingUser = _repo.GetUserByEmail(userDto.Email);

        if (existingUser != null)
        {
            // Intercetta l'errore QUI, prima che il DB lo lanci (DbUpdateException)
            throw new ArgumentException("L'indirizzo email è già registrato.");
        }

        // Mappatura, Hashing e Salvataggio (la tua logica attuale)
        var newUser = MapToUser(userDto);

        // Esempio: _passwordHasher.HashPassword(newUser, newUser.Password);
        newUser.Password = _passwordHasher.HashPassword(newUser, newUser.Password);

        // Esempio: Chiama il metodo AddUser del Repository (IUserRepository)
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
