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
            AddModifyHistory(entity, DataStatus.New);
            return added;
        }

        public override Task AddAsync(TEntity entity)
        {
            return base.AddAsync(entity).ContinueWith((t) => AddModifyHistoryAsync(entity, DataStatus.New));
        }

        public override bool Remove(string id)
        {
            var result = base.Remove(id);
            if (result)
                AddRemoveHistory(id);
            return result;
        }

        public override Task<bool> RemoveAsync(string id)
        {
            return base.RemoveAsync(id).ContinueWith((t) =>
            {
                if (t.Result)
                    AddRemoveHistoryAsync(id);

                return t.Result;
            });
        }

        public override bool Update(string id, TEntity entity)
        {
            var result = base.Update(id, entity);
            if (result)
                AddModifyHistory(entity, DataStatus.Normal);

            return result;
        }

        public override Task<bool> UpdateAsync(string id, TEntity entity)
        {
            return base.UpdateAsync(id, entity).ContinueWith((t) =>
            {
                if (t.Result)
                    AddModifyHistoryAsync(entity, DataStatus.Normal);

                return t.Result;
            });
        }

        protected abstract void AddModifyHistory(TEntity entity, DataStatus status);

        protected abstract Task AddModifyHistoryAsync(TEntity entity, DataStatus status);

        protected abstract void AddRemoveHistory(string id);

        protected abstract Task AddRemoveHistoryAsync(string id);
    }
}
