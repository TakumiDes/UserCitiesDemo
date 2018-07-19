using CitiesUsersApi.DataProvider;
using CitiesUsersApi.DbModels;
using CitiesUsersApi.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesUsersApi.Test
{
    public class UserCitiesProviderFake : IUserCitiesProvider
    {
        private readonly IEnumerable<User> _usersList;
        private readonly IEnumerable<City> _citiesList;
        private readonly IEnumerable<UserCities> _userCitiesList;

        public UserCitiesProviderFake()
        {
            _usersList = new List<User>()
            {
                new User() { Id = 1, Name = "John ", Email = "test@test.te", Phone = "123-23-12" },
                new User() { Id = 2, Name = "Smith", Email = "test@test.te", Phone = "123-23-12" },
                new User() { Id = 3, Name = "Josh ", Email = "test@test.te", Phone = "123-23-12" }
            };

            _citiesList = new List<City>()
            {
                new City() {Id = 1, Name = "Moscow", Area = 24.0 },
                new City() {Id = 2, Name = "Kazan", Area = 25.0 }
            };

            _userCitiesList = new List<UserCities>()
            {
                new UserCities() { Id = 1, CityId = 1, UserId = 1 },
                new UserCities() { Id = 1, CityId = 2, UserId = 1},
                new UserCities() { Id = 1, CityId = 2, UserId = 2}
            };
        }

        public Task<int> AddUser(UserRequestDto user)
        {
            var maxUserId = _usersList.Max(x => x.Id);
            (_usersList as List<User>).Add(new User()
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Id =  maxUserId + 1
                });

            if (user.CitiesIds != null) {
                foreach(var item in user.CitiesIds)
                {
                    var maxUserCitiesId = _userCitiesList.Max(x => x.Id);
                    (_userCitiesList as List<UserCities>).Add(new UserCities()
                    {
                        CityId = item,
                        UserId = maxUserId + 1,
                        Id = maxUserCitiesId + 1
                    });
                }
            }

            return Task.Run(() => maxUserId + 1);

        }

        public Task<IEnumerable<User>> GetAllUsers()
        {
            return Task.Run(() => _usersList);
        }

        public Task<IEnumerable<City>> GetCitiesByUserId(int userId)
        {
            var citiesId = _userCitiesList.Where(q => q.UserId == userId).Select(s => s.CityId).ToList();
            return Task.Run(() => _citiesList.Where(q => citiesId.Contains(q.Id)).ToList() as IEnumerable<City>);
        }
    }
}
