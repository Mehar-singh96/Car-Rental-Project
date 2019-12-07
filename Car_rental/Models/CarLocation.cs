namespace Car_rental.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CarLocation")]
    public partial class CarLocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CarLocation()
        {
            Bookings = new HashSet<Booking>();
        }

        [Key]
        [StringLength(50)]
        public string CarNo { get; set; }

        [Required]
        [StringLength(10)]
        public string Location { get; set; }

        [Required]
        [StringLength(10)]
        public string CarModel { get; set; }

        [Required]
        [StringLength(10)]
        public string Color { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual CarDetail CarDetail { get; set; }
    }
}
