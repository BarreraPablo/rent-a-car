using Microsoft.EntityFrameworkCore;
using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        public UserRepository(RentACarContext context) : base(context) { }

        public async Task<User> GetByUsername(string username)
        {
            if(string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNotDefinedException();
            }

            return await entities.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNotDefinedException();
            }

            return await entities.FirstOrDefaultAsync(u => u.EmailAddress == email);
        }

    }
}
