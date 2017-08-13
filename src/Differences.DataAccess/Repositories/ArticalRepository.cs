using Differences.Interaction.Models;
using Differences.Interaction.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace Differences.DataAccess.Repositories
{
    public class ArticalRepository : RepositoryBase<Article>, IArticalRepository
    {
        public ArticalRepository(IOptions<DbConnectionSetting> settings) : base(settings)
        {
        }
    }
}
