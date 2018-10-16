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

    /// <summary>
    /// Controller for interacting with users
    /// </summary>
    [Route("api/users")]
    public class UsersController : Controller
    {

        private IBookTradeRepository _bookTradeRepository;

#pragma warning disable CS1591
        public UsersController(IBookTradeRepository bookTradeRepository)
        {
            _bookTradeRepository = bookTradeRepository;
        }
#pragma warning restore CS1591

        /// <summary>
        /// Gets all users in the database
        /// </summary>
        /// <returns>A json collection of users</returns>
        [HttpGet()]
        public IActionResult GetUsers()
        {
            var userEntities = _bookTradeRepository.GetUsers();
            var results = Mapper.Map<IEnumerable<UserWithoutBooksDto>>(userEntities);

            return Ok(results);
        }

        /// <summary>
        /// Gets a user from the database from their id
        /// </summary>
        /// <param name="id">The id of the user to get</param>
        /// <param name="includeBooks">If all of their books should be included with ther user</param>
        /// <returns>Returns a json object of the user on success or not found if the user does
        /// not exist</returns>
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

        /// <summary>
        /// Get a username and user ID, from the user's username and password
        /// </summary>
        /// <param name="credentials">JSON object with username and password
        /// username is not case sensitive, password is</param>
        /// <returns>A JSON object containing the properly cased username and their 
        /// ID in the database</returns>
        [HttpPost("Login")]
        public IActionResult GetIDFromCredentials([FromBody] CredentialsDto credentials)
        {
            var user = _bookTradeRepository.GetUserFromCredentials(credentials.username, credentials.password);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var userWithIDResult = Mapper.Map<UserWithIdDto>(user);
                return Ok(userWithIDResult);
            }

        }
        

    }
}
