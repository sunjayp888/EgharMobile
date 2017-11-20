namespace Egharpay.Business.Interfaces
{
    public interface IRazorBusinessService
    {
        string CreateText(string jsonString, string templateName);
        bool IsTemplateCached(string templateName);
        void CacheTemplate(string templateName, string template);
    }
}
