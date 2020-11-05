namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Waist")]
    public partial class Waist
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string WaistName { get; set; }

        public int? OrderNumber { get; set; }
    }
}
