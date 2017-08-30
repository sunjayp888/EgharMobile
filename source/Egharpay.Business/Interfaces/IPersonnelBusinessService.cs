using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Document = Egharpay.Business.Models.Document;

namespace Egharpay.Business.Interfaces
{
    public interface IPersonnelBusinessService
    {
        //Create
        Task<ValidationResult<Personnel>> CreatePersonnel(Personnel personnel);

        //Retrieve
        Task<bool> CanDeletePersonnel(int personnelId);
        Task<Personnel> RetrievePersonnel(int centreId, int personnelId);
        Task<Personnel> RetrievePersonnel(int personnelId);
        //Task<PagedResult<PersonnelGrid>> RetrievePersonnels(int centreId, List<OrderBy> orderBy = null, Paging paging = null);
        //Task<PagedResult<PersonnelGrid>> Search(int centreId,string term, List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<Models.Document>> RetrievePersonnelDocuments(int personnelId, Paging paging = null, List<OrderBy> orderBy = null);
        //Update
        Task<ValidationResult<Personnel>> UpdatePersonnel(Personnel department);

        //Delete
        Task<bool> DeletePersonnel(int id);
    }
}
