using BarterProject.Domain.BaseEntities;

namespace BarterProject.Domain.Entites;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string PasswordHash { get; set; }
    public string Address { get; set; }
}
