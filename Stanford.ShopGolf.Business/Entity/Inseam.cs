namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Inseam")]
    public partial class Inseam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inseam()
        {
            SanPham_Inseam = new HashSet<SanPham_Inseam>();
        }

        public int Id { get; set; }

        [StringLength(20)]
        public string InseamName { get; set; }

        public int? OrderNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham_Inseam> SanPham_Inseam { get; set; }
    }
}
