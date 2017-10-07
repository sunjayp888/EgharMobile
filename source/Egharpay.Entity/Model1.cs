namespace Egharpay.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<SellerGrid> SellerGrids { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
                .Property(e => e.SearchField)
                .IsUnicode(false);
        }
    }
}
