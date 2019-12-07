namespace Car_rental.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CarRentalModel : DbContext
    {
        public CarRentalModel()
            : base("name=CarRentalModel")
        {
        }

        public virtual DbSet<AccountDetail> AccountDetails { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<CarDetail> CarDetails { get; set; }
        public virtual DbSet<CarLocation> CarLocations { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<ExtraFeature> ExtraFeatures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountDetail>()
                .Property(e => e.DriverLicenceNo)
                .IsUnicode(false);

            modelBuilder.Entity<AccountDetail>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.AccountDetail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CarDetail>()
                .Property(e => e.CarModel)
                .IsFixedLength();

            modelBuilder.Entity<CarDetail>()
                .Property(e => e.VehicleType)
                .IsFixedLength();

            modelBuilder.Entity<CarDetail>()
                .HasMany(e => e.CarLocations)
                .WithRequired(e => e.CarDetail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CarLocation>()
                .Property(e => e.Location)
                .IsFixedLength();

            modelBuilder.Entity<CarLocation>()
                .Property(e => e.CarModel)
                .IsFixedLength();

            modelBuilder.Entity<CarLocation>()
                .Property(e => e.Color)
                .IsFixedLength();

            modelBuilder.Entity<CarLocation>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.CarLocation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Discount>()
                .HasMany(e => e.AccountDetails)
                .WithRequired(e => e.Discount)
                .HasForeignKey(e => e.TotalPoints)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ExtraFeature>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.ExtraFeature)
                .HasForeignKey(e => e.ExtraFeatureId)
                .WillCascadeOnDelete(false);
        }
    }
}
