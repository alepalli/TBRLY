using System;
using System.Collections.Generic;
using System.Linq;
using TBRly.API.Data;
using TBRly.API.DTOs;
using TBRly.API.Models;

namespace TBRly.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context; // Riferimento al contesto del database

    public UserRepository(AppDbContext context)
    {
        _context = context; // Iniettiamo il contesto del database tramite il costruttore
    }

    // torna tutti gli utenti
    public List<User> GetAllUsers() => _context.Users.ToList();

    // aggiunge un nuovo utente
    public User AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

}
