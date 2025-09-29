using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TBRly.API.Data;
using TBRly.API.Models;

namespace TBRly.API.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    // torna tutti i libri
    public List<Book> GetAllBooks() => _context.Books.ToList();

    // cerca un libro per ISBN
    public Book? GetBookByISBN(long ISBN) => _context.Books.FirstOrDefault(b => b.ISBN == ISBN); //FirstOrDefault restituisce null se non trova nulla → per questo il compilatore vuole che tu dichiari Book? (cioè "Book o null").

    // aggiunge un libro
    public Book AddBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
        return book;
    }

    // elimina un libro per ISBN
    public void DeleteBook(long ISBN)
    {
        var book = GetBookByISBN(ISBN);
        // Console.WriteLine(book == null ? "non trovato" : $"trovato {book.Title} {book.ISBN}");
        if (book != null)
        {
            _context.Books.Remove(book);
            _context.SaveChanges(); // salva le modifiche al database
        }
    }

    // aggiorna un libro tramite ISBN
    public Book? UpdateBook(long ISBN, Book updatedBook)
    {
        var book = GetBookByISBN(ISBN);
        if (book != null)
        {
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Format = updatedBook.Format;
            book.Status = updatedBook.Status;
            book.PageCount = updatedBook.PageCount;
            book.Review = updatedBook.Review;
            book.ISBN = updatedBook.ISBN;
            book.Language = updatedBook.Language;
            book.Genre = updatedBook.Genre;
            book.Description = updatedBook.Description;
            book.CoverImageUrl = updatedBook.CoverImageUrl;
            book.Format = updatedBook.Format;
            Console.WriteLine($"Aggiornato libro {book.Title} con ISBN {book.ISBN}");
            _context.Books.Update(book);
            _context.SaveChanges();
        }
        return book;
    }

    // cerca libri per autore (case insensitive)
    public List<Book> GetBooksByAuthor(string author)
    {
        Console.WriteLine($"Cercando libri di autore: {author}");
        // return _context.Books.Where(b => b.Author.ToLower().Contains(author.ToLower())).ToList();
        return _context.Books.Where(b => EF.Functions.Like(b.Author, $"%{author}%")).ToList();
    }

    public List<Book> GetBooksByGenre(string genre)
    {
        throw new System.NotImplementedException();
    }

    public List<Book> GetBooksByLanguage(string language)
    {
        throw new System.NotImplementedException();
    }

    public List<Book> GetBooksByStatus(string status)
    {
        throw new System.NotImplementedException();
    }

    public List<Book> GetBooksByFormat(string format)
    {
        throw new System.NotImplementedException();
    }

    public List<Book> GetBooksByTitle(string title)
    {
        throw new System.NotImplementedException();
    }
}