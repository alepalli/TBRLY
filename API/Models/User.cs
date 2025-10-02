using System;
using Microsoft.EntityFrameworkCore;
using TBRly.API.Enums;

namespace TBRly.API.Models;

[Index(nameof(Email), IsUnique = true)] // rende unica la colonna Email nel database
public class User
{
    public long Id { get; set; } // non metto key perche è implicita
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public UserRole Role { get; set; } = UserRole.User; // valore di default
    public string? ProfilePictureUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? Bio { get; set; } = string.Empty;
}
