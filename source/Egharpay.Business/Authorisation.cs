using Egharpay.Business.Interfaces;
using Egharpay.Entity.Dto;

namespace Egharpay.Business
{
    public class Authorisation : IAuthorisation
    {
        public int? OrganisationId { get; set; }

        public Role Role => (Role)RoleId;

        public int RoleId { get; set; }
    }
}
