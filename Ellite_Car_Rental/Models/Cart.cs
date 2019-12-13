namespace Ellite_Car_Rental.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cart")]
    public partial class Cart
    {
        public int ID { get; set; }

        public int? User_Id { get; set; }

        public int? Car_Id { get; set; }

        public DateTime? From_Date { get; set; }

        public DateTime? Till_Date { get; set; }

        public virtual Car Car { get; set; }

        public virtual User User { get; set; }
    }
}
