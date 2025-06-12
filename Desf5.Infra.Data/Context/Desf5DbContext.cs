using Microsoft.EntityFrameworkCore;
using Desf5.Domain.Entities;

namespace Desf5.Infra.Data.Context;

public class Desf5DbContext : DbContext
{
    public Desf5DbContext(DbContextOptions<Desf5DbContext> options) : base(options) { }

    public DbSet<Produto> Produto { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Desf5DbContext).Assembly);
    }
}
