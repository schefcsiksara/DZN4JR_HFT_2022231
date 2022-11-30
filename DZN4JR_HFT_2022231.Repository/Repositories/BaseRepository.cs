using DZN4JR_HFT_2022231.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Repository.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected DbContext Db;

        protected BaseRepository(DbContext Db)
        {
            this.Db = Db;
        }

        public TEntity Create(TEntity entity)
        {
            var result = Db.Add(entity);
            Db.SaveChanges();

            return result.Entity;
        }

        public void Delete(TEntity entity)
        {
            Db.Remove(entity);
            Db.SaveChanges();
        }

        public abstract TEntity Read(TKey id);

        public IQueryable<TEntity> ReadAll()
        {
            return Db.Set<TEntity>();
        }

        public TEntity Update(TEntity entity)
        {
            var result = Db.Set<TEntity>().Update(entity);
            Db.SaveChanges();

            return result.Entity;
        }
    }
}
