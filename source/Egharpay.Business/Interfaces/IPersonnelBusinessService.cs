﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        Task<ValidationResult<Document>> UploadDocument(Document document, int personnelId);

        //Retrieve
        Task<bool> CanDeletePersonnel(int personnelId);
        Task<Personnel> RetrievePersonnel(int centreId, int personnelId);
        Task<ValidationResult<Personnel>> RetrievePersonnel(int personnelId);
        Task<PagedResult<Personnel>> RetrievePersonnels(List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<Personnel>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null);
        Task<Personnel> RetrievePersonnel(string userId);

        //Update
        Task<ValidationResult<Personnel>> UpdatePersonnel(Personnel department);

        //Delete
        Task<bool> DeletePersonnel(int id);

        //Helper
        Task<ValidationResult<Personnel>> PersonnelAlreadyExists(string mobileNumber, string email = null);
    }
}
