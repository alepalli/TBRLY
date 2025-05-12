using System.Collections.Generic;
using System.Linq;
using TBRly.API.Models;

namespace TBRly.API.Repositories;

public class BookRepository : IBookRepository
{
    private readonly List<Book> _books = new();
    private int _nextId = 1;

    public List<Book> GetAllBooks() => _books;

    public Book? GetBookById(int id) => _books.FirstOrDefault(b => b.Id == id);

    public Book AddBook(Book book)
    {
        book.Id = _nextId++;
        _books.Add(book);
        return book;
    }

    public void DeleteBook(int id)
    {
        var book = GetBookById(id);
        if (book != null)
        {
            _books.Remove(book);
        }
    }

    public Book? UpdateBook(int id, Book updatedBook)
    {
        var book = GetBookById(id);
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
            return book;
        }
        return null;
    }

    public List<Book> GetBooksByAuthor(string author)
    {
        throw new System.NotImplementedException();
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

    public List<Book> GetBooksByISBN(string isbn)
    {
        throw new System.NotImplementedException();
    }

    public List<Book> GetBooksByTitle(string title)
    {
        throw new System.NotImplementedException();
    }
}
