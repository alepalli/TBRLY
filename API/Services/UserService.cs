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
