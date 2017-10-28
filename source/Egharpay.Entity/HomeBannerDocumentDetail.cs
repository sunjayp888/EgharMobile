namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HomeBannerDocumentDetail")]
    public partial class HomeBannerDocumentDetail
    {
        public int HomeBannerDocumentDetailId { get; set; }

        public int HomeBannerId { get; set; }

        public int DocumentDetailId { get; set; }
    }
}
