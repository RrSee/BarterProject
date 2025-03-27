using BarterProject.Repository.Repositories;

namespace BarterProject.Repository.Common;

public interface IUnitOfWork
{
    public IBarterRequestRepository BarterRequestRepository { get; }
    public INotificationRepository NotificationRepository { get; }
    Task<int> CommitAsync();
}
