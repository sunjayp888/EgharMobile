namespace Egharpay.Data
{
    using System.Data.Entity;
    using Egharpay.Entity;

    public partial class EgharpayDatabase : DbContext
    {
        public EgharpayDatabase()
            : base("name=EgharpayDatabase")
        {
        }

        public virtual DbSet<Advertise> Advertises { get; set; }
        public virtual DbSet<AspNetPermission> AspNetPermissions { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsersAlertSchedule> AspNetUsersAlertSchedules { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<DocumentCategory> DocumentCategories { get; set; }
        public virtual DbSet<DocumentDetail> DocumentDetails { get; set; }
        public virtual DbSet<HomeBanner> HomeBanners { get; set; }
        public virtual DbSet<Mobile> Mobiles { get; set; }
        public virtual DbSet<MobileComment> MobileComments { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Personnel> Personnels { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<RequestType> RequestTypes { get; set; }
        public virtual DbSet<Seller> Sellers { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<Trend> Trends { get; set; }
        public virtual DbSet<TrendComment> TrendComments { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<BrandGrid> BrandGrids { get; set; }
        public virtual DbSet<HomeBannerGrid> HomeBannerGrids { get; set; }
        public virtual DbSet<MobileCommentGrid> MobileCommentGrids { get; set; }
        public virtual DbSet<MobileGrid> MobileGrids { get; set; }
        public virtual DbSet<SellerGrid> SellerGrids { get; set; }
        public virtual DbSet<TrendCommentGrid> TrendCommentGrids { get; set; }
        public virtual DbSet<HomeBannerDocument> HomeBannerDocumentDetails { get; set; }
        public virtual DbSet<HomeBannerImage> HomeBannerImages { get; set; }
        public virtual DbSet<PersonnelDocument> PersonnelDocuments { get; set; }
        public virtual DbSet<PersonnelDocumentDetail> PersonnelDocumentDetails { get; set; }
        public virtual DbSet<SellerMobile> SellerMobiles { get; set; }
        public virtual DbSet<MetaSearchKeyword> MetaSearchKeywords { get; set; }
        public virtual DbSet<OrderState> OrderStates { get; set; }
        public virtual DbSet<MobileRepairPayment> MobileRepairPayments { get; set; }

        public virtual DbSet<SellerMobileGrid> SellerMobileGrids { get; set; }

        public virtual DbSet<SellerOrderGrid> SellerOrderGrids { get; set; }
        public virtual DbSet<SellerOrder> SellerOrders { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AspNetUserMobileOtp> AspNetUserMobileOtps { get; set; }
        public virtual DbSet<CouponCode> CouponCodes { get; set; }
        public virtual DbSet<MobileCoupon> MobileCoupons { get; set; }
        public virtual DbSet<MobileRepair> MobileRepairs { get; set; }
        public virtual DbSet<MobileRepairGrid> MobileRepairGrids { get; set; }
        public virtual DbSet<AvailableMobileRepairAdmin> AvailableMobileRepairAdmins { get; set; }
        public virtual DbSet<MobileRepairAdminPersonnel> MobileRepairAdminPersonnels { get; set; }
        public virtual DbSet<Search> Searches { get; set; }
        public virtual DbSet<MobileFault> MobileFaults { get; set; }
        public virtual DbSet<MobileRepairMobileFault> MobileRepairMobileFaults { get; set; }
        public virtual DbSet<MobileRepairFee> MobileRepairFees { get; set; }
        public virtual DbSet<MobileRepairFeeGrid> MobileRepairFeeGrids { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertise>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Advertise>()
                .Property(e => e.Image1)
                .IsUnicode(false);

            modelBuilder.Entity<Advertise>()
                .Property(e => e.Image2)
                .IsUnicode(false);

            modelBuilder.Entity<Advertise>()
                .Property(e => e.Image3)
                .IsUnicode(false);

            modelBuilder.Entity<Advertise>()
                .Property(e => e.Image4)
                .IsUnicode(false);

            modelBuilder.Entity<Advertise>()
                .Property(e => e.Image5)
                .IsUnicode(false);

            modelBuilder.Entity<Advertise>()
                .Property(e => e.Tag)
                .IsUnicode(false);

            modelBuilder.Entity<Advertise>()
                .Property(e => e.Detail)
                .IsUnicode(false);

            modelBuilder.Entity<Advertise>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<Brand>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Brand>()
                .Property(e => e.NumberOfDevice)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentCategory>()
                .HasMany(e => e.DocumentDetails)
                .WithRequired(e => e.DocumentCategory)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DocumentDetail>()
                .Property(e => e.FileName)
                .IsUnicode(false);

            modelBuilder.Entity<HomeBanner>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<HomeBanner>()
                .Property(e => e.SubTitle)
                .IsUnicode(false);

            modelBuilder.Entity<HomeBanner>()
                .Property(e => e.Tag)
                .IsUnicode(false);

            modelBuilder.Entity<HomeBanner>()
                .Property(e => e.Pincode)
                .IsUnicode(false);

            modelBuilder.Entity<HomeBanner>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<HomeBanner>()
                .Property(e => e.ImagePath)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.ReleasedDate)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.BodyDimension)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.OS)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Storage)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.DisplayResolution)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.CameraPixel)
                .IsUnicode(false);

            //modelBuilder.Entity<Mobile>()
            //    .Property(e => e.RAM)
            //    .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Chipset)
                .IsUnicode(false);

            //modelBuilder.Entity<Mobile>()
            //    .Property(e => e.BatterySize)
            //    .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.BatteryType)
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
                .Property(e => e.Multitouch)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Protection)
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
                .Property(e => e.InternalMemory)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.PrimaryCamera);
                //.IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.CameraFeatures)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.Video)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.SecondaryCamera);
                //.IsUnicode(false);

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
                .Property(e => e.BatteryTalkTime)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.BatteryMusicPlay)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .Property(e => e.VideoPixel)
                .IsUnicode(false);

            modelBuilder.Entity<Mobile>()
                .HasMany(e => e.MobileComments)
                .WithRequired(e => e.Mobile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MobileComment>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .Property(e => e.ShortDescription)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .Property(e => e.LongDescription)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .Property(e => e.Link)
                .IsUnicode(false);

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

            modelBuilder.Entity<Product>()
                .HasMany(e => e.DocumentDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RequestType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Seller>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Seller>()
                .Property(e => e.RegistrationNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Seller>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Seller>()
                .Property(e => e.Owner)
                .IsUnicode(false);

            modelBuilder.Entity<Seller>()
                .Property(e => e.Address1)
                .IsUnicode(false);

            modelBuilder.Entity<Seller>()
                .Property(e => e.Address2)
                .IsUnicode(false);

            modelBuilder.Entity<Seller>()
                .Property(e => e.Address3)
                .IsUnicode(false);

            modelBuilder.Entity<Seller>()
                .Property(e => e.Address4)
                .IsUnicode(false);

            modelBuilder.Entity<Seller>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<Trend>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Trend>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Trend>()
                .Property(e => e.Detail)
                .IsUnicode(false);

            modelBuilder.Entity<Trend>()
                .Property(e => e.ShortDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Trend>()
                .HasMany(e => e.TrendComments)
                .WithRequired(e => e.Trend)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TrendComment>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetUser>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<BrandGrid>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<BrandGrid>()
                .Property(e => e.SearchField)
                .IsUnicode(false);

            modelBuilder.Entity<HomeBannerGrid>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<HomeBannerGrid>()
                .Property(e => e.SubTitle)
                .IsUnicode(false);

            modelBuilder.Entity<HomeBannerGrid>()
                .Property(e => e.Tag)
                .IsUnicode(false);

            modelBuilder.Entity<HomeBannerGrid>()
                .Property(e => e.Pincode)
                .IsUnicode(false);

            modelBuilder.Entity<HomeBannerGrid>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<HomeBannerGrid>()
                .Property(e => e.MobileName)
                .IsUnicode(false);

            modelBuilder.Entity<MobileCommentGrid>()
                .Property(e => e.MobileName)
                .IsUnicode(false);

            modelBuilder.Entity<MobileCommentGrid>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<TrendCommentGrid>()
                .Property(e => e.TrendName)
                .IsUnicode(false);

            modelBuilder.Entity<TrendCommentGrid>()
                .Property(e => e.Comment)
                .IsUnicode(false);  

            modelBuilder.Entity<HomeBannerImage>()
              .Property(e => e.Name)
              .IsUnicode(false);

            modelBuilder.Entity<HomeBannerImage>()
                .Property(e => e.FileName)
                .IsUnicode(false);


            modelBuilder.Entity<SellerMobileGrid>()
                .Property(e => e.MobileName)
                .IsUnicode(false);

            modelBuilder.Entity<SellerMobileGrid>()
                .Property(e => e.SellerName)
                .IsUnicode(false);

            modelBuilder.Entity<SellerMobileGrid>()
                .Property(e => e.SearchField)
                 .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.BrandName)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.ReleasedDate)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.BodyDimension)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.OS)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Storage)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.DisplayResolution)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.CameraPixel)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Chipset)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.BatteryType)
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
                .Property(e => e.Multitouch)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Protection)
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
                .Property(e => e.InternalMemory)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.PrimaryCameraDescription)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.CameraFeatures)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.Video)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.SecondaryCameraDescription)
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
                .Property(e => e.BatteryTalkTime)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.BatteryMusicPlay)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.VideoPixel)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.ShortDescription)
                .IsUnicode(false);

            modelBuilder.Entity<MobileGrid>()
                .Property(e => e.SearchField)
                .IsUnicode(false);

            modelBuilder.Entity<SellerOrderGrid>()
                .Property(e => e.MobileName)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepair>()
                .Property(e => e.MobileNumber)
                .HasPrecision(18, 0);

            modelBuilder.Entity<MobileRepair>()
                .Property(e => e.ModelName)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepair>()
                .Property(e => e.CouponCode)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepair>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepair>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepair>()
                .Property(e => e.ZipPostalCode)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepair>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepair>()
                .Property(e => e.LandMark)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepair>()
                .Property(e => e.District)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepair>()
                .Property(e => e.AlternateNumber)
                .HasPrecision(18, 0);

            modelBuilder.Entity<MobileRepair>()
                .Property(e => e.Comment);

            modelBuilder.Entity<SellerOrderGrid>()
                .Property(e => e.ShopName)
                .IsUnicode(false);

            modelBuilder.Entity<SellerOrderGrid>()
                .Property(e => e.SellerAddress)
                .IsUnicode(false);

            modelBuilder.Entity<SellerOrderGrid>()
                .Property(e => e.SellerPincode)
                .IsUnicode(false);

            modelBuilder.Entity<SellerOrderGrid>()
                .Property(e => e.SellerEmail)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetPermission>()
                 .HasMany(e => e.AspNetRoles)
                 .WithMany(e => e.AspNetPermissions)
                 .Map(m => m.ToTable("AspNetRolePermissions").MapLeftKey("PermissionId").MapRightKey("RoleId"));

            modelBuilder.Entity<AspNetPermission>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetPermissions)
                .Map(m => m.ToTable("AspNetUserPermissions").MapLeftKey("PermissionId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetUser>()
                .Property(e => e.MobileNumber)
                .HasPrecision(18, 0);

            modelBuilder.Entity<MobileRepairGrid>()
                .Property(e => e.MobileNumber)
                .HasPrecision(18, 0);

            modelBuilder.Entity<MobileRepairGrid>()
                .Property(e => e.ModelName)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepairGrid>()
                .Property(e => e.CouponCode)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepairGrid>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepairGrid>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepairGrid>()
                .Property(e => e.ZipPostalCode)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepairGrid>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepairGrid>()
                .Property(e => e.LandMark)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepairGrid>()
                .Property(e => e.District)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepairGrid>()
                .Property(e => e.AlternateNumber)
                .HasPrecision(18, 0);

            modelBuilder.Entity<MobileRepairGrid>()
                .Property(e => e.AppointmentTime)
                .IsUnicode(false);

            modelBuilder.Entity<MobileRepairGrid>()
                .Property(e => e.SearchField)
                .IsUnicode(false);

            modelBuilder.Entity<SellerGrid>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<SellerGrid>()
                .Property(e => e.RegistrationNumber)
                .IsUnicode(false);

            modelBuilder.Entity<SellerGrid>()
                .Property(e => e.Owner)
                .IsUnicode(false);

            modelBuilder.Entity<SellerGrid>()
                .Property(e => e.Address1)
                .IsUnicode(false);

            modelBuilder.Entity<SellerGrid>()
                .Property(e => e.Address2)
                .IsUnicode(false);

            modelBuilder.Entity<SellerGrid>()
                .Property(e => e.Address3)
                .IsUnicode(false);

            modelBuilder.Entity<SellerGrid>()
                .Property(e => e.Address4)
                .IsUnicode(false);

            modelBuilder.Entity<SellerGrid>()
                .Property(e => e.Pincode)
                .IsUnicode(false);

            modelBuilder.Entity<SellerGrid>()
                .Property(e => e.SearchField)
                .IsUnicode(false);

            modelBuilder.Entity<SellerMobileGrid>()
               .Property(e => e.MobileName)
               .IsUnicode(false);

            modelBuilder.Entity<SellerMobileGrid>()
                .Property(e => e.SellerName)
                .IsUnicode(false);

            modelBuilder.Entity<SellerMobileGrid>()
                .Property(e => e.SellerAddress)
                .IsUnicode(false);

            modelBuilder.Entity<SellerMobileGrid>()
                .Property(e => e.Pincode)
                .IsUnicode(false);

            modelBuilder.Entity<SellerMobileGrid>()
                .Property(e => e.SellerDistance)
                .HasPrecision(1, 1);

            modelBuilder.Entity<SellerMobileGrid>()
                .Property(e => e.SearchField)
                .IsUnicode(false);

            modelBuilder.Entity<MobileFault>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MobileFault>()
                .HasMany(e => e.MobileRepairMobileFaults)
                .WithRequired(e => e.MobileFault)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Partner>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Partner>()
                .Property(e => e.EmailId)
                .IsUnicode(false);

            modelBuilder.Entity<Partner>()
                .Property(e => e.Comment)
                .IsUnicode(false);
        }
    }
}
