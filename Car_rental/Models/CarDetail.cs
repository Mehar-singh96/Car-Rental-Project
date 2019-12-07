namespace Car_rental.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CarDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CarDetail()
        {
            CarLocations = new HashSet<CarLocation>();
        }

        [Key]
        [StringLength(10)]
        public string CarModel { get; set; }

        public int Quantity { get; set; }

        [Required]
        [StringLength(10)]
        public string VehicleType { get; set; }

        public int PassengerCapacity { get; set; }

        public int Price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarLocation> CarLocations { get; set; }
    }
}
