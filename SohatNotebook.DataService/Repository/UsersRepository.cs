using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SohatNotebook.DataService.IRepository;
using SohatNotebook.DataService.Repository;
using SohatNotebook.Entities.DbSet;

namespace SohatNotebook.DataService.Data
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(
            AppDbContext context,
            ILogger logger
        ) : base (context, logger)
        {
            
        }

        public override async Task<IEnumerable<User>> All() 
        {
            try
            {
                 return await dbSet.Where(x => x.Status == 1)
                                .AsNoTracking()
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} all method has genereted an error", typeof(UsersRepository));
                return new List<User>();
            }
        }

       
    }
}