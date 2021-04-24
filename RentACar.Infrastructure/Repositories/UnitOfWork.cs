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
        private readonly RentACarContext db;
        //IBrandRepository _brandRepository { get; }

        public UnitOfWork(RentACarContext db)
        {
            this.db = db;
        }
        //public IBrandRepository BrandRepository => _brandRepository ?? new BrandRepository(db);

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            if(db != null)
            {
                db.Dispose();
            }

        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
