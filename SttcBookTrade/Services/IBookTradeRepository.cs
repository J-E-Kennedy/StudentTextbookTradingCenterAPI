using SttcBookTrade.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SttcBookTrade.Services
{
#pragma warning disable CS1591
    public interface IBookTradeRepository
    {
        bool UserExists(int userId);

        IEnumerable<User> GetUsers();

        User GetUser(int userId, bool includeBooks);

        Book GetBookOfUser(int userId, int bookId);

        IEnumerable<Book> GetBooksOfUser(int userId, int bookId);

        void AddBookToUser(int userId, Book book);
        void AddUser(User user);

        void DeleteBook(Book book);

        IEnumerable<Book> GetBooks();

        IEnumerable<Book> SearchBooks(string query, string order, string direction);

        IEnumerable<Trade> GetTrades();
        User GetUserFromCredentials(string username, string password);
        bool IsUsernameTaken(string username);

        bool Save();

        IEnumerable<Book> Sample(int amount);

    }
#pragma warning restore CS1591
}
