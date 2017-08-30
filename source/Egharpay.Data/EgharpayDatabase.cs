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
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Mobile> Mobiles { get; set; }
        public virtual DbSet<BrandGrid> BrandGrids { get; set; }
        public virtual DbSet<MobileGrid> MobileGrids { get; set; }

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

            modelBuilder.Entity<Brand>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Mobiles)
                .WithRequired(e => e.Brand)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Technology)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Network2GBands)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Network3GBands)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Network4GBands)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Speed)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Gprs)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Edge)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Announced)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Dimensions)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Weight)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Sim)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.MiscellaneousBody)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.DisplayType)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.DisplaySize)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.DisplayResolution)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Multitouch)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Protection)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Os)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Chipset)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Cpu)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Gpu)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.CardSlot)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Internal)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.PrimaryCamera)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.CameraFeatures)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Video)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.SecondaryCamera)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.AlertTypes)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Loudspeaker)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Sound3Point5MmJack)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.MiscellaneousSound)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Wlan)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Bluetooth)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Gps)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Nfc)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Radio)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Usb)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Sensors)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Messaging)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Browser)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Java)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.MiscellaneousFeatures)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.MiscellaneousBattery)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.StandBy)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.TalkTime)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.MusicPlay)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Colours)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Sar)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.SarEu)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Price)
                .IsUnicode(false);

            modelBuilder.Entity<BrandGrid>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<BrandGrid>()
                .Property(e => e.SearchField)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Technology)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Network2GBands)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Network3GBands)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Network4GBands)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Speed)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Gprs)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Edge)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Announced)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Dimensions)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Weight)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Sim)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.MiscellaneousBody)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.DisplayType)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.DisplaySize)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.DisplayResolution)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Multitouch)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Protection)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Os)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Chipset)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Cpu)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Gpu)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.CardSlot)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Internal)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.PrimaryCamera)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.CameraFeatures)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Video)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.SecondaryCamera)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.AlertTypes)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Loudspeaker)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Sound3Point5MmJack)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.MiscellaneousSound)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Wlan)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Bluetooth)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Gps)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Nfc)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Radio)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Usb)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Sensors)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Messaging)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Browser)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Java)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.MiscellaneousFeatures)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.MiscellaneousBattery)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.StandBy)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.TalkTime)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.MusicPlay)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Colours)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Sar)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.SarEu)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Price)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.SearchField)
                .IsUnicode(false);

            OnModelCreating(modelBuilder);
        }
    }
}
