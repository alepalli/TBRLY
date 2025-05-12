using System.Collections.Generic;
using TBRly.API.Models;

namespace TBRly.API.Repositories;

public interface IBookRepository
{
    List<Book> GetAllBooks();
    Book? GetBookById(int id);
    Book AddBook(Book book);
    void DeleteBook(int id);
    Book? UpdateBook(int id, Book book);
    List<Book> GetBooksByAuthor(string author);
    List<Book> GetBooksByGenre(string genre);
    List<Book> GetBooksByLanguage(string language);
    List<Book> GetBooksByStatus(string status);
    List<Book> GetBooksByFormat(string format);
    List<Book> GetBooksByISBN(string isbn);
    List<Book> GetBooksByTitle(string title);
}
