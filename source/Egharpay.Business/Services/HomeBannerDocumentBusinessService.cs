using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Egharpay.Business.Extensions;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using DocumentCategory = Egharpay.Business.Enum.DocumentCategory;

namespace Egharpay.Business.Services
{
   public class HomeBannerDocumentBusinessService:IHomeBannerDocumentBusinessService
    {
        private readonly IDocumentsBusinessService _documentsBusinessService;
        private readonly IMapper _mapper;

        public HomeBannerDocumentBusinessService(IDocumentsBusinessService documentsBusinessService, IMapper mapper)
        {
            _documentsBusinessService = documentsBusinessService;
            _mapper = mapper;
        }

        public async Task<ValidationResult<Document[]>> RetrievePersonnelDocuments(int homeBannerId, DocumentCategory category)
        {
            var validationResult = new ValidationResult<Document[]>();
            try
            {

                var documents = await _documentsBusinessService.RetrieveDocuments(homeBannerId, category);
                var result = _mapper.MapToList<Document>(documents).ToArray();
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

        public async Task<ValidationResult<Document>> RetrievePersonnelProfileImage(int homeBannerId)
        {
            var validationResult = new ValidationResult<Document>();
            try
            {

                var documents = await _documentsBusinessService.RetrieveDocuments(homeBannerId, DocumentCategory.HomeBannerImage);
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
    }
}
