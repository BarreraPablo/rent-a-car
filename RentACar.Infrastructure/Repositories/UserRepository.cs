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
    public class UserRepository : IUserRepository
    {
        private readonly RentACarContext context;

        public UserRepository(RentACarContext context)
        {
            this.context = context;
        }
        public async Task Add(User user)
        {
            user.CreatedAt = DateTime.Now;
            await context.AddAsync(user);
        }

        public async Task<User> GetByUsername(string username)
        {
            if(string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNotDefinedException();
            }

            return await context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNotDefinedException();
            }

            return await context.Users.Where(u => u.EmailAddress == email).FirstOrDefaultAsync();
        }

        public async Task<User> GetById(long id)
        {
            return await context.Users.Where(u => u.UserId == id).FirstOrDefaultAsync();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
