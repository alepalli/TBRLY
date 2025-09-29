using System.Collections.Generic;
using TBRly.API.Models;

namespace TBRly.API.Repositories;

public interface IBookRepository
{
    List<Book> GetAllBooks();
    Book? GetBookByISBN(long ISBN);
    Book AddBook(Book book);
    void DeleteBook(long ISBN);
    Book? UpdateBook(long ISBN, Book book);
    List<Book> GetBooksByAuthor(string author);
    List<Book> GetBooksByGenre(string genre);
    List<Book> GetBooksByLanguage(string language);
    List<Book> GetBooksByStatus(string status);
    List<Book> GetBooksByFormat(string format);
    List<Book> GetBooksByTitle(string title);
}
