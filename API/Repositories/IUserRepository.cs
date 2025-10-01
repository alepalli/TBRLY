using System;
using System.Collections.Generic;
using TBRly.API.Models;

namespace TBRly.API.Repositories;

public interface IUserRepository
{
    List<User> GetAllUsers();
    User AddUser(User user);
}
