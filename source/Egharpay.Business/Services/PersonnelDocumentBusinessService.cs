using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Egharpay.Business.Extensions;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using DocumentCategory = Egharpay.Business.Enum.DocumentCategory;

namespace Egharpay.Business.Services
{
    public class PersonnelDocumentBusinessService : IPersonnelDocumentBusinessService
    {
        private readonly IDocumentsBusinessService _documentsBusinessService;
        private readonly IMapper _mapper;
        private readonly IPersonnelDataService _dataService;

        public PersonnelDocumentBusinessService(IDocumentsBusinessService documentsBusinessService, IMapper mapper, IPersonnelDataService dataService)
        {
            _documentsBusinessService = documentsBusinessService;
            _mapper = mapper;
            _dataService = dataService;
        }

        public async Task<ValidationResult<Document[]>> RetrievePersonnelDocuments(int personnelId, DocumentCategory category)
        {
            var validationResult = new ValidationResult<Document[]>();
            try
            {

                var documents = await _documentsBusinessService.RetrieveDocuments(personnelId, category);
                validationResult.Succeeded = true;
                validationResult.Entity = documents.Entity;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }
        public async Task<ValidationResult<Document>> RetrievePersonnelProfileImage(int personnelId)
        {
            var validationResult = new ValidationResult<Document>();
            try
            {

                var documents = await _documentsBusinessService.RetrieveDocuments(personnelId, DocumentCategory.ProfilePhoto);
                var photo = documents.Entity.FirstOrDefault();
                if (photo == null)
                {
                    validationResult.Errors = new List<string> { "No Profile Image" };
                    validationResult.Succeeded = false;
                    return validationResult;
                }
                var result = _mapper.Map<Document>(photo);
                validationResult.Succeeded = true;
                validationResult.Entity = result;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }
        public async Task<PagedResult<Document>> RetrievePersonnelDocuments(int personnelId, Paging paging = null, List<OrderBy> orderBy = null)
        {
            var documents = await _dataService.RetrievePagedResultAsync<DocumentDetail>(d => d.PersonnelId == personnelId.ToString(), orderBy, paging);
            var searchResults = _mapper.Map<IEnumerable<Document>>(documents.Items);
            return PagedResult<Document>.Create(searchResults, documents.CurrentPage, documents.ResultsPerPage, documents.TotalPages, documents.TotalResults);
        }
        public async Task<PagedResult<PersonnelDocumentDetail>> RetrievePersonnelSelfies(DateTime startDateTime, DateTime endDateTime)
        {
            return await _dataService.RetrievePagedResultAsync<PersonnelDocumentDetail>(d => d.CreatedDateUTC >= startDateTime && d.CreatedDateUTC <= endDateTime);
        }
    }
}
