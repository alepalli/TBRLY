using System;
using System.Collections.Generic;
using TBRly.API.DTOs;
using TBRly.API.Models;
using TBRly.API.Repositories;

namespace TBRly.API.Services;

public class UserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo; // Inietta il repository
    }

    public List<UserDto> GetAllUsers() => _repo.GetAllUsers().ConvertAll(MapToUserDto);

    public UserDto AddUser(UserDto user)
    {
        var newUser = MapToUser(user);
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

    public User MapToUser(UserDto user) =>
        new User
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
