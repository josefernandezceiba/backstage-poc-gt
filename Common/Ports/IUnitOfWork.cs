namespace MoMo.Common.Ports;

public interface IUnitOfWork
{
    Task SaveAsync(CancellationToken? cancellationToken);
}
