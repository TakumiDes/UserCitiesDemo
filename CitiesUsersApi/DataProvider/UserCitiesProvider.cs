using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitiesUsersApi.Context;
using CitiesUsersApi.DbModels;
using CitiesUsersApi.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace CitiesUsersApi.DataProvider
{
    public class UserCitiesProvider : IUserCitiesProvider
    {
        private readonly CitiesUsersContext _citiesUsersContext = new CitiesUsersContext();

        public async Task AddUser(UserRequestDto user)
        {
            using (var context = new CitiesUsersContext())
            {
                User newUser = new User()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone
                };

                context.Users.Add(newUser);

                await context.SaveChangesAsync();

                foreach (var item in user.CitiesIds) {

                    UserCities userCities = new UserCities()
                    {
                        CityId = item,
                        UserId = newUser.Id
                    };

                    context.UserCities.Add(userCities);
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            using (var context = new CitiesUsersContext())
            {
                return await context.Users.ToListAsync();
            }
        }

        public async Task<IEnumerable<City>> GetCitiesByUserId(int userId)
        {
            using (var context = new CitiesUsersContext())
            {

                var cityIds = await context.UserCities.Where(x => x.UserId == userId).Select(s => s.CityId).ToListAsync();

                return await context.Cities.Where(x => cityIds.Contains(x.Id)).ToListAsync();
            }
        }

        public async Task<City> GetCityById(int cityId)
        {
            using (var context = new CitiesUsersContext())
            {
                return await context.Cities.Where(q => q.Id == cityId).FirstOrDefaultAsync();
            }
        }
    }
}
