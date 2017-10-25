namespace Egharpay.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AspNetUser
    {
        public AspNetUser()
        {
            AspNetPermissions = new HashSet<AspNetPermission>();
            AspNetRoles = new HashSet<AspNetRole>();
        }

        [Key]
        [Column(Order = 0)]
        public string Id { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool PhoneNumberConfirmed { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool LockoutEnabled { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccessFailedCount { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(256)]
        public string UserName { get; set; }

        public int? PersonnelId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetPermission> AspNetPermissions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}
