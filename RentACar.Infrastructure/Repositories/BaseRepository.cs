using RentACar.Core.Interfaces;
using RentACar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using RentACar.Core.Exceptions;

namespace RentACar.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> where T : BaseEntity
    {
        private readonly RentACarContext db;
        protected DbSet<T> entities;
        public BaseRepository(RentACarContext db)
        {
            this.db = db;
            this.entities = db.Set<T>();
        }
        public virtual IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public async Task<T> GetById(long id)
        {
            return await entities.FindAsync(id);
        }
        public async Task Add(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNotDefinedException();
            }

            entity.CreatedAt = DateTime.Now;
            entities.Add(entity);
        }
        public async Task CheckAndUpdate(T entity, T entityWithChanges)
        {
            if (entity == null || entityWithChanges == null)
            {
                throw new ArgumentNotDefinedException();
            }

            entity.ModifiedAt = DateTime.Now;
            db.Entry(entity).CurrentValues.SetValues(entityWithChanges);
            db.Entry(entity).Property(c => c.CreatedAt).IsModified = false;
        }

        public async Task GetAndUpdate(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNotDefinedException();
            }

            var current = await GetById(entity.Id);

            if (current == null)
            {
                throw new NullEntityException();
            }

            entity.ModifiedAt = DateTime.Now;
            db.Entry(current).CurrentValues.SetValues(entity);
            db.Entry(current).Property(c => c.CreatedAt).IsModified = false;
        }

        public async Task Delete(long id)
        {
            T entity = await GetById(id);

            if(entity == null)
            {
                throw new NullEntityException();
            }

            entities.Remove(entity);
        }



    }
}
