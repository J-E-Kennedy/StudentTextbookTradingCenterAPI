using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SttcBookTrade.Entities;
using SttcBookTrade.Models;
using SttcBookTrade.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SttcBookTrade.Controllers
{
    /// <summary>
    /// Controller for interacting with books
    /// </summary>
    [Route("api")]
    public class BooksController : Controller
    {
        private IBookTradeRepository _bookTradeRepository;

#pragma warning disable CS1591
        public BooksController(IBookTradeRepository bookTradeRepository)
        {
            _bookTradeRepository = bookTradeRepository;
        }
#pragma warning restore CS1591

        /// <summary>
        /// Gets all books store in the database, currently no limit on the amount of books returned
        /// </summary>
        /// <param name="withSeller">If each book should include information about it's seller</param>
        /// <returns>A json collection of books</returns>
        [HttpGet("books")]
        public IActionResult GetBooks(bool withSeller = false)
        {
            var bookEntities = _bookTradeRepository.GetBooks();

            if(withSeller)
            {

                var booksWithSellerResults = Mapper.Map<IEnumerable<BookWithSellerDto>>(bookEntities);
                return Ok(booksWithSellerResults);
            }
            var booksResult = Mapper.Map<IEnumerable<BookDto>>(bookEntities);
            return Ok(booksResult);
        }

        /// <summary>
        /// Searches the database with a query
        /// </summary>
        /// <param name="q">The query to search the database with, if left empty will return all books 
        /// in the database, multiple terms should be seperated with "+", searches books on their
        /// name, author, edition, and ISBN numbers.</param>
        /// <param name="order">How the search should be ordered, valid arguments are 
        /// relevance, which returns books in order of most matching keywords,
        /// price, returns books ordered by their listing price, 
        /// and name, sorted by the title of the book. Defaults to relevance</param>
        /// <param name="dir">If the search results should be in ascending (asc) or descending (desc) order
        /// defaults to asc</param>
        /// <returns>A json collection of books, always includes the book's seller</returns>
        [HttpGet("search")]
        public IActionResult SearchBooks(string q = "", string order = "relevance", string dir = "asc")
        {
            var books = _bookTradeRepository.SearchBooks(q, order, dir);

            var booksWithSellerResult = Mapper.Map<IEnumerable<BookWithSellerDto>>(books);
            return Ok(booksWithSellerResult);
        }

        /// <summary>
        /// Gets a book
        /// </summary>
        /// <param name="userId">The id of the user of whose book to get</param>
        /// <param name="bookId">The id of the book to get</param>
        /// <returns>The book as a json object on success or not found if there was not matching userId or bookId</returns>
        [HttpGet("users/{userId}/books/{bookId}", Name = "GetBook")]
        public IActionResult GetBook(int userId, int bookId)
        {
            if (!_bookTradeRepository.UserExists(userId))
            {
                return NotFound();
            }

            var book = _bookTradeRepository.GetBookOfUser(userId, bookId);

            if (book == null)
            {
                return NotFound();
            }

            var bookResult = Mapper.Map<BookDto>(book);
            return Ok(bookResult);
        }


        /// <summary>
        /// Adds a book to a given user, book should be contained in the body of the post request
        /// </summary>
        /// <param name="userId">The id of the user who the book should be added to</param>
        /// <param name="book">(In body) All info of the book</param>
        /// <returns>The new book added on success, bad request on invalid book, or not found
        /// for a non-existent user given</returns>
        [HttpPost("users/{userId}/books")]
        public IActionResult CreateBook(int userId,
           [FromBody] BookForCreationDto book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_bookTradeRepository.UserExists(userId))
            {
                return NotFound();
            }

            //var finalBook = Mapper.Map<Entities.Book>(book);
            Book finalBook = new Book()
            {
                Name = book.Name,
                Edition = book.Edition,
                Author = book.Author,
                ISBN10 = book.ISBN10,
                ISBN13 = book.ISBN13,
                Price = book.Price,
                Condition = book.Condition,
                Notes = book.Notes,
                IsWishlisted = book.IsWishlisted
            };


            _bookTradeRepository.AddBookToUser(userId, finalBook);

            if (!_bookTradeRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdBookToReturn = Mapper.Map<Models.BookDto>(finalBook);
            var retBookUserId = userId;

            return CreatedAtRoute("GetBook", new
            { userId = retBookUserId, bookId = createdBookToReturn.BookId }, createdBookToReturn);
        }

        /// <summary>
        /// Updates a user's book
        /// </summary>
        /// <param name="userId">The id of the user of whose book to update</param>
        /// <param name="id">The id of the book to update</param>
        /// <param name="book">(From body) info for the book in json format</param>
        /// <returns>Returns nothing on success, returns bad request if the book info
        /// is invalid, returns not found if the user or book id does not exist</returns>
        [HttpPut("users/{userId}/books/{id}")]
        public IActionResult UpdateBook(int userId, int id,
           [FromBody] BookForUpdateDto book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_bookTradeRepository.UserExists(userId))
            {
                return NotFound();
            }

            var bookEntity = _bookTradeRepository.GetBookOfUser(userId, id);
            if (bookEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(book, bookEntity);

            if (!_bookTradeRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

/// <summary>
    /// Deletes a book from the database
    /// </summary>
    /// <param name="userId">The id of the user whose book is to be deleted</param>
    /// <param name="bookId">The id of the book to be deleted</param>
    /// <returns>Returns no content on success, returns not found if the userId or bookId don't exist</returns>
        [HttpDelete("{userId}/books/{bookId}")]
        public IActionResult DeleteBook(int userId, int bookId)
        {
            if(!_bookTradeRepository.UserExists(userId))
            {
                return NotFound();
            }

            var bookEntity = _bookTradeRepository.GetBookOfUser(userId, bookId);

            if(bookEntity == null)
            {
                return NotFound();
            }

            _bookTradeRepository.DeleteBook(bookEntity);

            if(!_bookTradeRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }


        /// <summary>
        /// Returns a sample of books with simplified information
        /// </summary>
        /// <param name="amount">The size of the sample, if given a value larger than the
        /// number of books available, returns the largest amount it can.</param>
        /// <returns>A json collection of book objects</returns>
        [HttpGet("books/sample")]
        public IActionResult GetSampleBooks(int amount = 4)
        {
            var sample = _bookTradeRepository.Sample(amount);

            var sampleResult = Mapper.Map<IEnumerable<BookSimplifiedDto>>(sample);

            return Ok(sampleResult);
        }


    }
}
