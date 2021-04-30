using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RentACarContext context;
        //IBrandRepository _brandRepository { get; }

        IUserRepository _userRepository { get; }

        public UnitOfWork(RentACarContext context)
        {
            this.context = context;
        }
        //public IBrandRepository BrandRepository => _brandRepository ?? new BrandRepository(db);

        public IUserRepository UserRepository => _userRepository ?? new UserRepository(context);

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            if(context != null)
            {
                context.Dispose();
            }

        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
