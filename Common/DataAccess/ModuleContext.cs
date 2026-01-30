using Microsoft.EntityFrameworkCore;
using MoMo.Common.Core;


namespace MoMo.Common.DataAccess;

public partial class ModuleContext(DbContextOptions<ModuleContext> opts) : DbContext(opts)
{
    
    public async Task CommitAsync(CancellationToken? cancellationToken) =>
        await SaveChangesAsync(cancellationToken == new CancellationTokenSource().Token);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ModuleContext).Assembly);

        modelBuilder.Model.GetEntityTypes().Where(e => typeof(BaseEntity).IsAssignableFrom(e.ClrType))
           .ToList()
           .ForEach(e =>
           {
               modelBuilder.Entity(e.ClrType).Property<DateTime>("CreatedOn");
               modelBuilder.Entity(e.ClrType).Property<DateTime>("LastModifiedOn");
           });

        base.OnModelCreating(modelBuilder);
    }
}
