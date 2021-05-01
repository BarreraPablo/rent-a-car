﻿using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Data;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RentACarContext context;
        private IBrandRepository _brandRepository;
        private IDocumentTypeRepository _documentTypeRepository;
        private IUserRepository _userRepository;
        private IBodyTypeRepository _bodyTypeRepository;
        private ICarRepository _carRepository;

        public UnitOfWork(RentACarContext context)
        {
            this.context = context;
        }

        public IDocumentTypeRepository DocumentTypeRepository
        {
            get
            {
                if(_documentTypeRepository == null)
                {
                    _documentTypeRepository = new DocumentTypeRepository(context);
                }
                return _documentTypeRepository;
            }
        }

        public IBrandRepository BrandRepository
        {
            get
            {
                if(_brandRepository == null)
                {
                    _brandRepository = new BrandRepository(context);
                }
                return _brandRepository;
            }
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

        public ICarRepository CarRepository
        {
            get
            {
                if(_carRepository == null)
                {
                    _carRepository = new CarRepository(context);
                }
                return _carRepository;
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
