using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Egharpay.Business.Interfaces;
using Egharpay.Data.Interfaces;

namespace Egharpay.Business.Services
{
    public class TemplateBusinessService : ITemplateBusinessService
    {
        private readonly IPdfBusinessService _pdfBusinessService;
        private readonly IRazorBusinessService _razorBusinessService;
        private readonly ITemplateDataService _templateDataService;

        public TemplateBusinessService(IRazorBusinessService razorBusinessService, IPdfBusinessService pdfBusinessService, ITemplateDataService templateDataService)
        {
            _pdfBusinessService = pdfBusinessService;
            _templateDataService = templateDataService;
            _razorBusinessService = razorBusinessService;
        }

        public byte[] CreatePDF(int organisationId, string jsonString, string templateName)
        {
            try
            {
                string htmlData = CreateText(jsonString, templateName);
                return _pdfBusinessService.CreatePDFfromHtml(htmlData);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public byte[] CreatePDFfromPDFTemplate(int organisationId, Dictionary<string, string> formValues, string templateName)
        {
            return _pdfBusinessService.CreatePDFfromPDFTemplate(formValues, string.Empty);
        }

        public string CreateText(string jsonString, string templateName)
        {
            if (!_razorBusinessService.IsTemplateCached(templateName))
            {
                var template = GetTemplateHtml(templateName);
                _razorBusinessService.CacheTemplate(templateName, template);
            }
            return _razorBusinessService.CreateText(jsonString, templateName);
        }

        public string GetTemplateHtml(string templateName)
        {
            var templateDetails = _templateDataService.Retrieve<Entity.Template>(e => e.Name.ToLower() == templateName.ToLower());
            var template = templateDetails.FirstOrDefault();
            template.FilePath = Path.Combine(ConfigHelper.TemplateRootFilePath, template.FileName);
            return File.ReadAllText(template.FilePath);
        }
    }
}
