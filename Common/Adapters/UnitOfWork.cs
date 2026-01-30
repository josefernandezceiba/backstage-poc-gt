using Microsoft.EntityFrameworkCore;
using MoMo.Common.DataAccess;
using MoMo.Common.Ports;

namespace MoMo.Common.Adapters;

public class UnitOfWork(ModuleContext _context) : IUnitOfWork
{
    public async Task SaveAsync(CancellationToken? cancellationToken)
    {
        var token = cancellationToken ?? new CancellationTokenSource().Token;

        _context.ChangeTracker.DetectChanges();

        Dictionary<EntityState, string> entryStatus = new(){
            {EntityState.Added, "CreatedOn"},
            {EntityState.Modified, "LastModifiedOn"}
        };
       

        _context.ChangeTracker.Entries().Where(e => entryStatus.ContainsKey(e.State)).ToList().ForEach(e =>
        {
            e.Property(entryStatus[e.State]).CurrentValue = DateTime.UtcNow;
        });

        await _context.SaveChangesAsync(token);
       
    }
}
