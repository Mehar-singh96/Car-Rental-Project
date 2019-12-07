using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car_rental.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    

    public partial class SignUpModel
{
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SignUpModel()
        {
            Bookings = new HashSet<Booking>();
        }

        [Key]
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
       
        [Required]
        [StringLength(50)]
        [Compare(nameof(Password), ErrorMessage ="The Password didn't match")]
        public string ConfirmPassword { get; set; }



        [Required(ErrorMessage ="First Name is required")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50)]
        public string LastName { get; set; }

        public long PhoneNo { get; set; }

        

        [Required(ErrorMessage ="Issue country is required")]
        [StringLength(50)]
        public string IssueCountry { get; set; }

        [Required]
        [StringLength(50)]
        public string IssueAuthority { get; set; }

        [Required]
        [StringLength(50)]
        public string DriverLicenceNo { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
