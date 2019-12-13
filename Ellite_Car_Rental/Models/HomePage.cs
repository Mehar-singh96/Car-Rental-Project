namespace Ellite_Car_Rental.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HomePage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HomePage()
        {
            Bookings = new HashSet<Booking>();
            Carts = new HashSet<Cart>();
        }

        public int ID { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        public string Url_Img { get; set; }

        [StringLength(255)]
        public string Desc { get; set; }

        public int? TypeId { get; set; }

        public bool? Available { get; set; }

        public int? Qty { get; set; }

        public decimal? Rent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual Car_Type Car_Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
