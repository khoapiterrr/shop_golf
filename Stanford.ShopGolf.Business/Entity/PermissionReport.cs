namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PermissionReport")]
    public partial class PermissionReport
    {
        public int Id { get; set; }

        public int? CompanyId { get; set; }

        public int? UserId { get; set; }
    }
}
