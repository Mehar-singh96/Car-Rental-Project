namespace Ellite_Car_Rental.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CarModel : DbContext
    {
        public CarModel()
            : base("name=CarModel")
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Car_Type> Car_Type { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car_Type>()
                .HasMany(e => e.Cars)
                .WithOptional(e => e.Car_Type)
                .HasForeignKey(e => e.TypeId);

            modelBuilder.Entity<Car>()
                .Property(e => e.Rent)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Car>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Car)
                .HasForeignKey(e => e.Car_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Car>()
                .HasMany(e => e.Carts)
                .WithOptional(e => e.Car)
                .HasForeignKey(e => e.Car_Id);

            modelBuilder.Entity<Order>()
                .Property(e => e.Total_Price)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Status)
                .HasForeignKey(e => e.Pay_Id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Carts)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.User_Id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Usr_Id);
        }
    }
}
