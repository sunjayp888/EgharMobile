using System;
using System.Collections.Generic;

namespace Egharpay.Business.Models
{
    public class Document
    {
        public string Category { get; set; } // eg. PAYSLIP, Document, Category
        public string FileName { get; set; }
        public string ClientFileName { get; set; }
        public string Product { get; set; }
        public DateTime? UpdatedDateUTC { get; set; }
        public byte[] Content { get; set; } // can be NULL when just retrieving descriptions
        public string ContentBase64 { get; set; }
        public Guid? DocumentGuid { get; set; }
        public string Description { get; set; }
        public string BatchDescription { get; set; }
        public string PersonnelId { get; set; } //PersonnelId
        public DateTime? CreatedDateUtc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DownloadedDateUtc { get; set; }
        public DateTime? ApprovedDateUtc { get; set; }
        public bool? RequireApproval { get; set; }
        public DateTime? EmailSentDateUtc { get; set; }
        public int? DocumentBatchId { get; set; }
        public string PersonnelName { get; set; }
        public string BatchId { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
    }
}
