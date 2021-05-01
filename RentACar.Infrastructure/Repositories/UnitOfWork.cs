using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Data;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RentACarContext context;
        private IUserRepository _userRepository;
        private IBodyTypeRepository _bodyTypeRepository;

        public UnitOfWork(RentACarContext context)
        {
            this.context = context;
        }


        public IUserRepository UserRepository
        {
            get
            {
                if(_userRepository == null)
                {
                    _userRepository = new UserRepository(context);
                }
                return _userRepository;
            }
        }

        public IBodyTypeRepository BodyTypeRepository {
            get
            {
                if(_bodyTypeRepository == null)
                {
                    _bodyTypeRepository = new BodyTypeRepository(context);
                }
                return _bodyTypeRepository;
            }
        }

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
