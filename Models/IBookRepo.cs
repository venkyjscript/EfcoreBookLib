using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Models
{
    public interface IBookRepo<TEntity>
    {
        IEnumerable<TEntity> GetAll();


        object Get(int id);

        object Add(TEntity entity);
        object Update(TEntity dbEntity, TEntity entity);
        object UpdateById(int Id,TEntity entity);

        object Delete(TEntity entity);
    }
}
