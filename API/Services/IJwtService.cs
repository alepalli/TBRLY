using TBRly.API.Models;

namespace TBRly.API.DTOs;

public interface IJwtService
{
    string GenerateJwtToken(User user);
}
