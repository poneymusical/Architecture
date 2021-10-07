using System.Reflection;
using Application;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public new DbSet<TAggregateRoot> Set<TAggregateRoot>() where TAggregateRoot : class, IAggregateRoot =>
            base.Set<TAggregateRoot>();
    }
}