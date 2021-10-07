using System.Threading;
using System.Threading.Tasks;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public interface IApplicationDbContext
    {
        DbSet<TAggregateRoot> Set<TAggregateRoot>() where TAggregateRoot : class, IAggregateRoot;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}