namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HomeBannerDocument")]
    public partial class HomeBannerDocument
    {
        public int HomeBannerDocumentId { get; set; }

        public int HomeBannerId { get; set; }

        public int DocumentDetailId { get; set; }
    }
}
