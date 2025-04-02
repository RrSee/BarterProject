using BarterProject.Domain.Entites;

namespace BarterProject.Repository.Repositories;

public interface IBarterRequestRepository
{
    Task AddAsync(BarterRequest barterRequest);
    Task<bool> Delete(int id, int deletedBy);
}
