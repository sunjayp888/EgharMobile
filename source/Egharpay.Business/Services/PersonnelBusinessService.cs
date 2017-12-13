using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Egharpay.Business.EmailServiceReference;
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
        protected IPersonnelDataService _dataService;
        private readonly IMapper _mapper;
        protected IDocumentsBusinessService DocumentsBusinessService;
        protected IEmailBusinessService _emailBusinessService;

        public PersonnelBusinessService(IPersonnelDataService dataService, IMapper mapper, IDocumentsBusinessService documentsBusinessService,IEmailBusinessService emailBusinessService)
        {
            _dataService = dataService;
            _mapper = mapper;
            DocumentsBusinessService = documentsBusinessService;
            _emailBusinessService = emailBusinessService;
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
                //Send Confirmation Email to Personnel and Seller
                //if (personnel. != null && personnel.IsSeller.Value)
                    SendSellerEmail(personnel);

                validationResult.Entity = personnel;
                validationResult.Succeeded = true;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }

        private void SendSellerEmail(Personnel personnel)
        {
            var emailData = new EmailData()
            {
                BCCAddressList = new List<string> { "sunjayp88@gmail.com" },
                Body = String.Format("Dear {0} {1} {2}, Thanks For Registering on Mumbile.Com",personnel.Title,personnel.Forenames,personnel.Surname),
                Subject = "Welcome To Mumbile.Com",
                IsHtml = true,
                ToAddressList = new List<string> { personnel.Email.ToLower() }
            };
            _emailBusinessService.SendEmail(emailData);
        }

        public async Task<ValidationResult<Document>> UploadDocument(Document document, int personnelId)
        {
            var validationResult = new ValidationResult<Document>();
            try
            {
                var result = await DocumentsBusinessService.CreateDocument(document);
                if (result.Succeeded)
                {
                    var personnelDocument = new PersonnelDocument
                    {
                        PersonnelId = personnelId,
                        DocumentDetailId = result.Entity.DocumentDetailId // Just Confirm what should pass to documentdetailid
                    };
                    await _dataService.CreateAsync(personnelDocument);
                }
                validationResult.Entity = document;
                validationResult.Succeeded = true;
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
            var personnels = await _dataService.RetrieveAsync<Personnel>(a => a.PersonnelId == personnelId);
            return personnels.FirstOrDefault();
        }

        public async Task<ValidationResult<Personnel>> RetrievePersonnel(int personnelId)
        {
            var personnel = await _dataService.RetrieveByIdAsync<Personnel>(personnelId);
            if (personnel != null)
            {
                var validationResult = new ValidationResult<Personnel>
                {
                    Entity = personnel,
                    Succeeded = true
                };
                return validationResult;
            }

            return new ValidationResult<Personnel>
            {
                Succeeded = false,
                Errors = new[] { string.Format("No Worker found with Id: {0}", personnelId) }
            };
        }

        public async Task<PagedResult<Personnel>> RetrievePersonnels(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var personnel = await _dataService.RetrievePagedResultAsync<Personnel>(e => true, orderBy, paging);
            return personnel;
        }

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


        #endregion

        #region Update

        public async Task<ValidationResult<Personnel>> UpdatePersonnel(Personnel personnel)
        {
            var validationResult = new ValidationResult<Personnel>();
            try
            {
                await _dataService.UpdateAsync(personnel);
                validationResult.Entity = personnel;
                validationResult.Succeeded = true;
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
