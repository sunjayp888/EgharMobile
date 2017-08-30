using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Egharpay.Models
{
    public class PersonnelDocumentViewModel
    {
        public int? WorkerDocumentId { get; set; }
        public int WorkerId { get; set; }
        public int PersonnelId { get; set; }
        public int? ClientPersonnelId { get; set; }
        [Required]
        [Display(Name ="Category")]
        public int? DocumentCategoryId { get; set; }
        public Guid DocumentGuid { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public HttpPostedFileBase File { get; set; }

        public SelectList DocumentCategories { get; set; }
    }
}