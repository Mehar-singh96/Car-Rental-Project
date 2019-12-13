namespace Ellite_Car_Rental.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Bookings = new HashSet<Booking>();
        }

        [Key]
        public int Trans_Id { get; set; }

        public int? Usr_Id { get; set; }

        public int? Pay_Id { get; set; }

        public decimal? Total_Price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual Status Status { get; set; }

        public virtual User User { get; set; }
    }
}
