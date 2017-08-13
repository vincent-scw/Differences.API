using Differences.Interaction.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Differences.Interaction.Repositories
{
    public interface IRepository<TEntity> where TEntity : AggregateRoot
    {
        Task<TEntity> Single(ISpecification spec);
        Task<TEntity> SingleOrDefault(ISpecification spec);
        Task<IEnumerable<TEntity>> Find(ISpecification spec);
        Task<IEnumerable<TEntity>> GetAll();

        Task Add(TEntity entity);
        Task Remove(string id);
        Task Remove(TEntity entity);
        Task Update(TEntity entity);
        //void CommitChanges();
    }
}
