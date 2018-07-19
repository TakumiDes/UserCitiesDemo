using CitiesUsersApi.DbModels;
using CitiesUsersApi.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesUsersApi.DataProvider
{
    public interface IUserCitiesProvider
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<IEnumerable<City>> GetCitiesByUserId(int userId);

        Task<int> AddUser(UserRequestDto user);

    }
}
