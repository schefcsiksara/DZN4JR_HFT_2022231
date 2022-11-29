using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Repository.Interfaces
{
    public interface IRepository<TEntity, TKey>
    {
        IQueryable<TEntity> ReadAll();

        TEntity Read(TKey id);

        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
