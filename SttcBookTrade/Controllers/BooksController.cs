using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SttcBookTrade.Models;
using SttcBookTrade.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SttcBookTrade.Controllers
{
    [Route("api/books")]
    public class BooksController : Controller
    {
        private IBookTradeRepository _bookTradeRepository;

        public BooksController(IBookTradeRepository bookTradeRepository)
        {
            _bookTradeRepository = bookTradeRepository;
        }


        [HttpGet()]
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

        [HttpGet("search")]
        public IActionResult SearchBooks(string q = "", string order = "relevance", string dir = "asc")
        {
            var books = _bookTradeRepository.SearchBooks(q, order, dir);

            var booksWithSellerResult = Mapper.Map<IEnumerable<BookWithSellerDto>>(books);
            return Ok(booksWithSellerResult);
        }

    }
}
