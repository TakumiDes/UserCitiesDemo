using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitiesUsersApi.DataProvider;
using CitiesUsersApi.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitiesUsersApi.Controllers
{
    [Produces("application/json")]
    [Route("api/UserCities")]
    public class UserCitiesController : Controller
    {

        private IUserCitiesProvider _userCitiesProvider;

        public UserCitiesController(IUserCitiesProvider userCitiesProvider)
        {
            _userCitiesProvider = userCitiesProvider;
        }

        /// <summary>
        /// Add new User.
        /// </summary>
        /// <param name="newUser"> New user </param>
        /// <returns>Added user id</returns>
        [HttpPost]
        [Route("adduser")]
        public async Task<IActionResult> AddNewUser([FromBody]UserRequestDto newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userId = await _userCitiesProvider.AddUser(newUser);
            return Ok(new { addedUserId = userId });
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>Users</returns>
        [HttpGet]
        [Route("getusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userCitiesProvider.GetAllUsers();
            return Ok(users);
        }

        /// <summary>
        /// Get cities by iser id.
        /// </summary>
        /// <param name="userId"> User id </param>
        /// <returns>Cities</returns>
        [HttpGet]
        [Route("getcitiesbyuserid")]
        public async Task<IActionResult> GetCitiesByUserId([FromQuery]int userId)
        {
            var cities = await _userCitiesProvider.GetCitiesByUserId(userId);
            if(cities.Count() == 0)
            {
                return NotFound();
            }

            return Ok(cities);
        }

    }
}