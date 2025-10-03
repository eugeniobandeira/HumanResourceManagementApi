namespace HR.Management.Domain.Interfaces;
public interface IUnitOfWork
{
    Task Commit(CancellationToken cancellationToken = default);
}
