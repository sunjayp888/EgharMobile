namespace Egharpay.Business.Interfaces
{
    public interface ITemplateBusinessService
    {
        byte[] CreatePDF(int organisationId, string jsonString, string templateName);
        byte[] CreatePDFfromPDFTemplate(int organisationId, System.Collections.Generic.Dictionary<string, string> formValues, string templateName);
        string CreateText(string jsonString, string templateName);
    }
}
