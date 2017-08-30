using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Egharpay.Business.Extensions;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Data.Interfaces;
using Document = Egharpay.Business.Models.Document;

namespace Egharpay.Business.Services
{
    public partial class PersonnelBusinessService : IPersonnelBusinessService
    {
        protected IPersonnelTestDataService _dataService;
        private readonly IMapper _mapper;

        public PersonnelBusinessService(IPersonnelTestDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        #region Create

        public async Task<ValidationResult<Personnel>> CreatePersonnel(Personnel personnel)
        {
            var validationResult = await PersonnelAlreadyExists(personnel.Email);
            if (!validationResult.Succeeded)
            {
                return validationResult;
            }
            try
            {
                await _dataService.CreateAsync(personnel);
                validationResult.Entity = personnel;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }

        #endregion

        #region Retrieve

        public async Task<bool> CanDeletePersonnel(int PersonnelId)
        {
            return await Task.FromResult(true);
        }

        public async Task<Personnel> RetrievePersonnel(int centreId, int personnelId)
        {
            var personnels = await _dataService.RetrieveAsync<Personnel>(a => a.PersonnelId == personnelId && (a.CentreId == centreId));
            return personnels.FirstOrDefault();
        }

        public async Task<Personnel> RetrievePersonnel(int personnelId)
        {
            var personnels = await _dataService.RetrieveAsync<Personnel>(a => a.PersonnelId == personnelId);
            return personnels.FirstOrDefault();
        }
        //public async Task<PagedResult<PersonnelGrid>> RetrievePersonnels(int centreId, List<OrderBy> orderBy = null, Paging paging = null)
        //{
        //    var personnel = await _dataService.RetrievePagedResultAsync<PersonnelGrid>(a => a.CentreId == centreId, orderBy, paging);
        //    return personnel;
        //}

        private async Task<ValidationResult<Personnel>> PersonnelAlreadyExists(string email)
        {
            var personnels = await _dataService.RetrieveAsync<Personnel>(a => a.Email.Trim().ToLower() == email.Trim().ToLower());
            var alreadyExists = personnels.Any();
            return new ValidationResult<Personnel>
            {
                Succeeded = !alreadyExists,
                Errors = alreadyExists ? new List<string> { "Personnel already exists." } : null
            };
        }

        //public async Task<PagedResult<PersonnelGrid>> Search(int centreId, string term, List<OrderBy> orderBy = null, Paging paging = null)
        //{
        //    return await _dataService.RetrievePagedResultAsync<PersonnelGrid>(a => a.CentreId == centreId && a.SearchField.ToLower().Contains(term.ToLower()), orderBy, paging);
        //}

        public async Task<PagedResult<Document>> RetrievePersonnelDocuments(int personnelId, Paging paging = null, List<OrderBy> orderBy = null)
        {
            var documents = await _dataService.RetrievePagedResultAsync<Entity.Document>(d => d.PersonnelId == personnelId.ToString(), orderBy, paging);

            var searchResults = _mapper.Map<IEnumerable<Models.Document>>(documents.Items);

            return PagedResult<Models.Document>.Create(searchResults, documents.CurrentPage, documents.ResultsPerPage, documents.TotalPages, documents.TotalResults);
        }
        #endregion

        #region Update

        public async Task<ValidationResult<Personnel>> UpdatePersonnel(Personnel personnel)
        {
            var validationResult = await PersonnelAlreadyExists(personnel.Email);
            if (!validationResult.Succeeded)
            {
                return validationResult;
            }
            try
            {
                await _dataService.UpdateAsync(personnel);
                validationResult.Entity = personnel;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }

        #endregion

        #region Delete

        public async Task<bool> DeletePersonnel(int id)
        {
            try
            {
                await _dataService.DeleteByIdAsync<Personnel>(id);
                return true;
            }
            catch
            {
                return false;
            }
        }


        #endregion
    }
}
