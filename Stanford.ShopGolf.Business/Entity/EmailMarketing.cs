namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmailMarketing")]
    public partial class EmailMarketing
    {
        public int Id { get; set; }

        [StringLength(30)]
        public string FullName { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}
