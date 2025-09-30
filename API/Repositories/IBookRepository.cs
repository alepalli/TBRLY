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
    List<Book> GetBooksByGenre(int genre);
    List<Book> GetBooksByLanguage(int language);
    List<Book> GetBooksByStatus(int status);
    List<Book> GetBooksByFormat(int format);
    List<Book> GetBooksByTitle(string title);
}
