using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Differences.Interaction.Models;

namespace Differences.DataAccess.Repositories
{
    public abstract class TraceableRepositoryBase<TEntity> : RepositoryBase<TEntity>
        where TEntity : AggregateRoot
    {
        public TraceableRepositoryBase(DifferencesDbContext dbContext) : base(dbContext)
        {
        }

        public override TEntity Add(TEntity entity)
        {
            var added = base.Add(entity);
            InsertModifyHistory(entity, DataStatus.New);
            return added;
        }

        public override int Remove(int id)
        {
            var result = base.Remove(id);
            if (result != default(int))
                InsertRemoveHistory(id);
            return result;
        }

        public override TEntity Update(int id, TEntity entity)
        {
            var result = base.Update(id, entity);
            if (result != default(TEntity))
                InsertModifyHistory(entity, DataStatus.Normal);

            return result;
        }

        protected abstract void InsertModifyHistory(TEntity entity, DataStatus status);

        protected abstract void InsertRemoveHistory(int id);
    }
}
