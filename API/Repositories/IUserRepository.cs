using System;
using System.Collections.Generic;
using TBRly.API.Models;

namespace TBRly.API.Repositories;

public interface IUserRepository
{
    List<User> GetAllUsers();
    User AddUser(User user);
    User? GetUserById(long id);
    void DeleteUser(long id);
    User? UpdateUser(User user);
    User? GetUserByEmail(string email);
    User? GetUserByUsername(string username);
}
