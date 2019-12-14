namespace Ellite_Car_Rental.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
        }

        public int ID { get; set; }

        [StringLength(255)]
        [DisplayName("First Name")]
        public string F_Name { get; set; }

        [StringLength(255)]
        [DisplayName("Last Name")]
        public string L_Name { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        [DisplayName("User Name")]
        public string User_name { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(255)]
        public string Role { get; set; }

        [StringLength(255)]
        [DisplayName("Street Address")]
        public string Street_Address { get; set; }

        [StringLength(255)]
        [RegularExpression(@"^([a-zA-Z \.\&\'\-]+)$", ErrorMessage = "Special characters and digits are not allowed ")]
        public string City { get; set; }

        [StringLength(255)]
        public string Province { get; set; }

        [StringLength(255)]
        public string Country { get; set; }

        [StringLength(255)]
        [DisplayName("Postal Code")]
        public string Postal_Code { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
