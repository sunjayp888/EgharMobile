using System.Collections.Generic;

namespace Egharpay.Business.Interfaces
{

    public interface IPdfBusinessService
    {
        byte[] CreatePDFfromPDFTemplate(Dictionary<string, string> formValues, string templatePath);
        
        //html is already data bound, this service is not going to call the template
        byte[] CreatePDFfromHtml(string Html);  
    }
}
