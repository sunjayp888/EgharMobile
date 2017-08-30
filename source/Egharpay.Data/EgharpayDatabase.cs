namespace Egharpay.Data
{
    using System.Data.Entity;
    using Egharpay.Entity;

    public partial class EgharpayDatabase : DbContext
    {
        public EgharpayDatabase() : base("name=EgharpayDatabase")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsersAlertSchedule> AspNetUsersAlertSchedules { get; set; }
        public virtual DbSet<Centre> Centres { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentCategory> DocumentCategories { get; set; }
        public virtual DbSet<Personnel> Personnels { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Centre>()
                          .Property(e => e.CentreCode)
                          .IsUnicode(false);

            modelBuilder.Entity<Centre>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Centre>()
                .Property(e => e.Address1)
                .IsUnicode(false);

            modelBuilder.Entity<Centre>()
                .Property(e => e.Address2)
                .IsUnicode(false);

            modelBuilder.Entity<Centre>()
                .Property(e => e.Address3)
                .IsUnicode(false);

            modelBuilder.Entity<Centre>()
                .Property(e => e.Address4)
                .IsUnicode(false);

            modelBuilder.Entity<Centre>()
                .Property(e => e.EmailId)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.FileName)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentCategory>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentCategory>()
                .Property(e => e.BasePath)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentCategory>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.DocumentCategory)
                .HasForeignKey(e => e.DocumentTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.PANNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.BankTelephone)
                .IsUnicode(false);

            modelBuilder.Entity<Personnel>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetUser>()
                .Property(e => e.Name)
                .IsUnicode(false);
            OnModelCreating(modelBuilder);
        }
    }
}
