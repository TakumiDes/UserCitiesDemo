using CitiesUsersApi.Controllers;
using CitiesUsersApi.DataProvider;
using CitiesUsersApi.DbModels;
using CitiesUsersApi.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CitiesUsersApi.Test
{
    public class UserCitiesControllerTest
    {
        UserCitiesController _controller;
        IUserCitiesProvider _provider;

        public UserCitiesControllerTest()
        {
            _provider = new UserCitiesProviderFake();
            _controller = new UserCitiesController(_provider);
        }

        [Fact]
        public void GetAllUsers_WhenCalled_ReturnOkResult()
        {
            //act
            var okResult = _controller.GetAllUsers();

            //assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetAllUsers_WhenCalled_ReturnAllUsers()
        {
            //act
            var okResult = _controller.GetAllUsers().Result as OkObjectResult;

            //assert
            var items = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetCitiesByUserId_UnknownIdPassed_ReturnNotFoundResult()
        {
            //act
            var notFoundResult = _controller.GetCitiesByUserId(99);

            //assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetCitiesByUserId_ExistingIdPassed_ReturnOkFoundResult()
        {
            //act 
            var okResult = _controller.GetCitiesByUserId(2);

            //assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetCitiesByUserId_ExistingIdPassed_ReturnItems()
        {
            //act 
            var okResult = _controller.GetCitiesByUserId(1).Result as OkObjectResult;

            //assert
            var items = Assert.IsType<List<City>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public void GetCitiesByUserId_ExistingIdPassed_ReturnRightItem()
        {
            //act 
            var okResult = _controller.GetCitiesByUserId(2).Result as OkObjectResult;

            // Assert
            Assert.IsType<List<City>>(okResult.Value);
            Assert.Equal(2, (okResult.Value as List<City>)[0].Id);
        }

        //[Fact]
        //public void AddNewUser_InvalidObjectPassed_ReturnsBadRequest()
        //{
        //    // Arrange
        //    var userMissingName = new UserRequestDto()
        //    {
        //        Phone = "123123",
        //        Email = "test@test.ts"
        //    };
        //    _controller.ModelState.AddModelError("Name", "Required");

        //    // Act
        //    var badResponse = _controller.AddNewUser(userMissingName);

        //    // Assert
        //    Assert.IsType<BadRequestObjectResult>(badResponse);
        //}

    }
}
