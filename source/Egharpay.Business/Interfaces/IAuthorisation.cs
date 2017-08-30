using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface IAuthorisation
    {
        int? OrganisationId { get; set; }        
        int RoleId { get; set; }
        Role Role { get; }
    }
}
