using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesUsersApi.DbModels
{
    public class UserCities
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CityId { get; set; }
    }
}
