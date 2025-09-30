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
            _context.Books.Update(book);
            _context.SaveChanges();
        }
        return book;
    }

    // cerca libri per autore (case insensitive)
    public List<Book> GetBooksByAuthor(string author)
    {
        // return _context.Books.Where(b => b.Author.ToLower().Contains(author.ToLower())).ToList();
        return _context.Books.Where(b => EF.Functions.Like(b.Author, $"%{author}%")).ToList(); // altro modo per fare una ricerca case insensitive
    }

    // cerca libri per genere tramite l'enum BookGenre
    public List<Book> GetBooksByGenre(int genre)
    {
        return _context.Books.Where(b => (int)b.Genre == genre).ToList();
    }

    // cerca libri per lingua tramite l'enum BookLanguage
    public List<Book> GetBooksByLanguage(int language)
    {
        return _context.Books.Where(b => (int)b.Language == language).ToList();
    }

    // cerca libri per stato tramite l'enum BookStatus

    public List<Book> GetBooksByStatus(int status)
    {
        return _context.Books.Where(b => (int)b.Status == status).ToList();
    }

    // cerca libri per formato tramite enum BookFormat
    public List<Book> GetBooksByFormat(int format)
    {
        return _context.Books.Where(b => (int)b.Format == format).ToList();
    }

    // cerca libri per titolo (case insensitive)
    public List<Book> GetBooksByTitle(string title)
    {
        return _context.Books.Where(b => EF.Functions.Like(b.Title, $"%{title}%")).ToList();
    }
}
