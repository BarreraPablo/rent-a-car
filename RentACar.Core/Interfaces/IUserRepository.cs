using RentACar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetById(long id);

        public Task<User> GetByEmail(string email);
        public Task<User> GetByUsername(string username);

        public Task Add(User user);

        public Task Update(User user);

    }
}
