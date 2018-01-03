using System;
using System.Collections.Generic;
using System.Text;
using Differences.Interaction.EntityModels;
using Differences.Interaction.Repositories;

namespace Differences.DataAccess.Repositories
{
    public class LikeRecordRepository : RepositoryBase<LikeRecord>, ILikeRecordRepository
    {
        public LikeRecordRepository(DifferencesDbContext dbContext) : base(dbContext)
        {
        }
    }
}
