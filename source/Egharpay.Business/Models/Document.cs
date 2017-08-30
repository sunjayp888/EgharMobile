using System;

namespace Egharpay.Business.Models
{
    public class Document
    {
        public long? DocumentId { get; set; }
        public string DocumentCategoryName { get; set; }
        public int? PersonnelId { get; set; }
        public int? DocumentCategoryId { get; set; }
        public string UserId { get; set; }
        public Guid? DocumentGuid { get; set; }
        public DateTime? UploadedDate { get; set; }
        public string UploadedBy { get; set; }
        public DateTime? ValidFromDate { get; set; }
        public DateTime? ValidToDate { get; set; }
        public string DocumentType { get; set; }
        public string Filename { get; set; }
        public string Description { get; set; }
        public byte[] Bytes { get; set; }
    }
}
