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
        [HttpGet("{id}", Name = "GetUser")]
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
            if (credentials == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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

        /// <summary>
        /// Get a username and user ID, from the user's username and password
        /// </summary>
        /// <param name="username">their username, not case sensitive</param>
        /// <param name="password">their password, case sensitive,
        /// being passed in plain text, just for testing purposes</param>
        /// <returns>A JSON object containing the properly cased username and their 
        /// ID in the database</returns>
        [HttpGet("Login")]
        public IActionResult GetIDFromCredentials(string username = "", string password = "")
        {
            var user = _bookTradeRepository.GetUserFromCredentials(username, password);
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


        /// <summary>
        /// Add a new user to the database
        /// </summary>
        /// <param name="userInfo">All the info needed to add a user, the username must be one that has not 
        /// been used by another user in the database</param>
        /// <returns>Returns the user added to the database on success, a bad request for a missing or invalid
        /// json object, a bad request for a username that has already been taken</returns>
        [HttpPost()]
        public IActionResult AddUser([FromBody] UserForCreationDto userInfo)
        {
            if (userInfo == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_bookTradeRepository.IsUsernameTaken(userInfo.Username))
            {
                return BadRequest("That username is already taken");
            }

            //var finalUser = Mapper.Map<User>(userInfo);
            User finalUser = new User()
            {
                Username = userInfo.Username,
                Password = userInfo.Password,
                Firstname = userInfo.Firstname,
                Lastname = userInfo.Lastname,
                Email = userInfo.Email,
                SchoolId = userInfo.SchoolId,
                Profile = userInfo.Profile,
                StudentIdentification = userInfo.StudentIdentification
            };

            _bookTradeRepository.AddUser(finalUser);

            if (!_bookTradeRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdUserToReturn = Mapper.Map<UserWithoutBooksDto>(finalUser);
            var retBookUserId = _bookTradeRepository
                .GetUserFromCredentials(userInfo.Username, userInfo.Password).UserId;

            return CreatedAtRoute("GetUser", new
            { id = retBookUserId, includeBooks = false }, createdUserToReturn);
        }
        

    }
}
