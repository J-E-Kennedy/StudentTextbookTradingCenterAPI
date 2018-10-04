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
    [Route("api/users")]
    public class UsersController : Controller
    {

        private IBookTradeRepository _bookTradeRepository;

        public UsersController(IBookTradeRepository bookTradeRepository)
        {
            _bookTradeRepository = bookTradeRepository;
        }


        [HttpGet()]
        public IActionResult GetUsers()
        {
            var userEntities = _bookTradeRepository.GetUsers();
            var results = Mapper.Map<IEnumerable<UserWithoutBooksDto>>(userEntities);

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id, bool includeBooks = false)
        {
            var user = _bookTradeRepository.GetUser(id, includeBooks);

            if(user == null)
            {
                return NotFound();
            }

            if(includeBooks)
            {
                var userResult = Mapper.Map<UserDto>(user);
                return Ok(userResult);
            }

            var userWithoutBooksResult = Mapper.Map<UserWithoutBooksDto>(user);
            return Ok(userWithoutBooksResult);
        }

    }
}
