using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Egharpay.Business.Interfaces;
using Egharpay.Data.Interfaces;


namespace Egharpay.Business
{
    public class TemplateService : ITemplateService
    {
        private readonly IPdfService _pdfService;
        private readonly IRazorService _razorService;
        private readonly ITemplateDataService _templateDataService;

        public TemplateService(IRazorService razorService, IPdfService pdfService, ITemplateDataService templateDataService)
        {
            _pdfService = pdfService;
            _templateDataService = templateDataService;
            _razorService = razorService;
        }

        public byte[] CreatePDF(int organisationId, string jsonString, string templateName)
        {
            try
            {
                string htmlData = CreateText(jsonString, templateName);
                return _pdfService.CreatePDFfromHtml(htmlData);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public byte[] CreatePDFfromPDFTemplate(int organisationId, Dictionary<string, string> formValues, string templateName)
        {
            return _pdfService.CreatePDFfromPDFTemplate(formValues, string.Empty);
        }

        public string CreateText(string jsonString, string templateName)
        {
            if (!_razorService.IsTemplateCached(templateName))
            {
                var template = GetTemplateHtml(templateName);
                _razorService.CacheTemplate(templateName, template);
            }
            return _razorService.CreateText(jsonString, templateName);
        }

        public string GetTemplateHtml(string templateName)
        {
            var templateDetails = _templateDataService.Retrieve<Entity.Template>(e => e.Name.ToLower() == templateName.ToLower());
            var template = templateDetails.FirstOrDefault();
            return File.ReadAllText(template?.FilePath);
        }
    }
}
