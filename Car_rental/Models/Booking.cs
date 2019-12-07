namespace Car_rental.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Booking")]
    public partial class Booking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookingId { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string CarNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime BookedFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime BookedTill { get; set; }

        [Column(TypeName = "date")]
        public DateTime BookingDate { get; set; }

        public int ExtraFeatureId { get; set; }

        public virtual AccountDetail AccountDetail { get; set; }

        public virtual CarLocation CarLocation { get; set; }

        public virtual ExtraFeature ExtraFeature { get; set; }
    }
}
