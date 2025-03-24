using BarterProject.Domain.Entites;

namespace BarterProject.Repository.Repositories;

public interface IBarterRequestRepository
{
    Task AddAyync(BarterRequest barterRequest);
    Task<bool> Delete(int id, int deletedBy);
}
