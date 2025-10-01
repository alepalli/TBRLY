using System;
using TBRly.API.Enums;

namespace TBRly.API.DTOs;

public class UserUpdateDto
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public UserRole? Role { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? Bio { get; set; }
    public DateTime? BirthDate { get; set; }
}
