namespace Egharpay.Business.Models
{
    public class DocumentCategory
    {
        public int DocumentCategoryId { get; set; }
        public string Name { get; set; }
        //public bool Expires { get; set; }
        //public bool RequiresReview { get; set; }
        //public string RequiresAction { get; set; }
        //public bool GenericDocument { get; set; }
        //public string BasePath { get; set; }
        public string FileNameRegex { get; set; }
    }
}
