using System;
using TBRly.API.Enums;

namespace TBRly.API.DTOs;

public class UserDto
{
    public long Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string? ProfilePictureUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Bio { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}
