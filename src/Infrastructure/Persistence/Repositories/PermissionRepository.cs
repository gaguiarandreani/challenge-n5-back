using Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using SecurityApi.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecurityApi.Infrastructure.Persistence.Repositories
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(AppDbContextOptionsBuilder builder) : base(builder)
        {
        }

        public override async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return ((await base.GetAllAsync()) as DbSet<Permission>).Include(nameof(PermissionType));
        }
    }
}