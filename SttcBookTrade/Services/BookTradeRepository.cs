using Microsoft.EntityFrameworkCore;
using SttcBookTrade.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SttcBookTrade.Services
{
#pragma warning disable CS1591
    public class BookTradeRepository : IBookTradeRepository
    {
        private BookTradeContext _context;

        public BookTradeRepository(BookTradeContext context)
        {
            _context = context;
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(c => c.UserId == userId);
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.OrderBy(c => c.Username).ToList();
        }

        public User GetUser(int userId, bool includeBooks)
        {
            if(includeBooks)
            {
                return _context.Users.Include(c => c.Books)
                    .Where(c => c.UserId == userId).FirstOrDefault();
            }

            return _context.Users.Where(c => c.UserId == userId).FirstOrDefault();
        }

        public Book GetBookOfUser(int userId, int bookId)
        {
            return _context.Books
                .Where(p => p.UserId == userId && p.BookId == bookId).FirstOrDefault();
        }
        public IEnumerable<Book> GetBooksOfUser(int userId, int bookId)
        {
            return _context.Books
                .Where(p => p.UserId == userId).ToList();
        }
        public IEnumerable<Book> SearchBooks(string query, string order, string direction)
        {
            string[] parts = query.ToLower().Split('+', StringSplitOptions.RemoveEmptyEntries);

            Dictionary<Book, int> Relevance;
            
            if(parts.Length > 0)
            {
                Relevance = new Dictionary<Book, int>();
                foreach (var part in parts)
                {
                    var bookWithKeyword = _context.Books
                        .Where(p => $"{p.Name} {p.Edition} {p.Author} {p.ISBN10} {p.ISBN13}"
                        .Contains(part, StringComparison.InvariantCultureIgnoreCase));

                    foreach (var book in bookWithKeyword)
                    {
                        if (Relevance.ContainsKey(book))
                        {
                            Relevance[book]++;
                        }
                        else
                        {
                            Relevance.Add(book, 1);
                        }
                    }
                }
            }
            else
            {
                Relevance = _context.Books.ToDictionary(x => x, x => 1);
            }

            foreach(var book in Relevance)
            {
                if(book.Key.User == null)
                {
                    book.Key.User = GetUser(book.Key.UserId, false);
                }
            }

            switch(order)
            {
                case "relevance":
                    if(direction == "asc")
                    {
                        return Relevance.OrderByDescending(x => x.Value).Select(x => x.Key);
                    }
                    else if(direction == "desc")
                    {
                        return Relevance.OrderBy(x => x.Value).Select(x => x.Key);
                    }
                    break;
                case "price":
                    if (direction == "asc")
                    {
                        return Relevance.OrderBy(x => x.Key.Price).Select(x => x.Key);
                    }
                    else if (direction == "desc")
                    {
                        return Relevance.OrderByDescending(x => x.Key.Price).Select(x => x.Key);
                    }
                    break;
                case "name":
                    if (direction == "asc")
                    {
                        return Relevance.OrderBy(x => x.Key.Name).Select(x => x.Key);
                    }
                    else if (direction == "desc")
                    {
                        return Relevance.OrderByDescending(x => x.Key.Name).Select(x => x.Key);
                    }
                    break;
            }
            return new List<Book>();
        }
        
        public void AddBookToUser(int userId, Book book)
        {
            var user = GetUser(userId, false);
            user.Books.Add(book);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public void DeleteBook(Book book)
        {
            _context.Books.Remove(book);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<Book> GetBooks()
        {
            foreach(var book in _context.Books)
            {
                if(book.User == null)
                {
                    book.User = GetUser(book.UserId, false);
                }
            }
            return _context.Books.ToList();
        }

        public IEnumerable<Trade> GetTrades()
        {
            foreach (var trade in _context.Trade)
            {
                if (trade.OffererUser == null)
                {
                    trade.OffererUser = GetUser(trade.OffererUserId, false);
                }
                if (trade.ReceiverUser == null)
                {
                    trade.ReceiverUser = GetUser(trade.ReceiverUserId, false);
                }
               
            }
            return _context.Trade.ToList();
        }

        public User GetUserFromCredentials(string username, string password)
        {
            return _context.Users.Where(x => x.Username.ToLower() == username.ToLower()
                && x.Password == password).FirstOrDefault();
        }

        public bool IsUsernameTaken(string username)
        {
            return _context.Users.Any(x => x.Username.ToLower() == username.ToLower());
        }

    }
#pragma warning restore CS1591
}
