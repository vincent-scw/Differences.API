using System;
using System.Collections.Generic;
using System.Text;
using Differences.Interaction.Models;
using Differences.Interaction.Repositories;

namespace Differences.DataAccess.Repositories
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(DifferencesDbContext dbContext) : base(dbContext)
        {
        }
    }
}
