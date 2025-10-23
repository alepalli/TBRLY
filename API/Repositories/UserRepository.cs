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

    // cerca un utente per id
    public User? GetUserById(long id) => _context.Users.FirstOrDefault(u => u.Id == id); // cerca un utente per id

    // elimina un utente per id
    public void DeleteUser(long id)
    {
        var user = GetUserById(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }

    public User? UpdateUser(User user)
    {
        // Aggiorna i campi dell'utente esistente
        _context.Users.Update(user);

        _context.SaveChanges(); // Salva le modifiche al database
        return user; // Ritorna l'utente aggiornato
    }

    public User? GetUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }

    // ... (AddUser rimane invariato, ma verrà chiamato solo dopo la validazione)
    public User AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges(); // <-- La DbUpdateException avviene qui
        return user;
    }
}