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

        [HttpPost]
        [Route("adduser")]
        public async Task<IActionResult> AddNewUser([FromBody]UserRequestDto newUser)
        {
            await _userCitiesProvider.AddUser(newUser);
            return Ok();
        }

        [HttpGet]
        [Route("getusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userCitiesProvider.GetAllUsers();

            return Ok(users);
        }

        [HttpGet]
        [Route("getcitiesbyuserid")]
        public async Task<IActionResult> GetCitiesByUserId([FromQuery]int userId)
        {
            var cities = await _userCitiesProvider.GetCitiesByUserId(userId);

            return Ok(cities);
        }

    }
}