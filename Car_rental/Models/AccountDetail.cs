namespace Car_rental.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountDetail")]
    public partial class AccountDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccountDetail()
        {
            Bookings = new HashSet<Booking>();
        }

        [Key]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public long PhoneNo { get; set; }

        public int TotalPoints { get; set; }

        [Required]
        [StringLength(50)]
        public string IssueCountry { get; set; }

        [Required]
        [StringLength(50)]
        public string IssueAuthority { get; set; }

        [Required]
        [StringLength(50)]
        public string DriverLicenceNo { get; set; }

        public virtual Discount Discount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
