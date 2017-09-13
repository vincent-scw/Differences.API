using System;
using System.Collections.Generic;
using System.Text;
using Differences.Interaction.Models;
using Differences.Interaction.Repositories;
using Microsoft.Extensions.Options;

namespace Differences.DataAccess.Repositories
{
    public class ReplyRepository : RepositoryBase<Reply>, IReplyRepository
    {
        public ReplyRepository(DifferencesDbContext dbContext) : base(dbContext)
        {
        }
    }
}
