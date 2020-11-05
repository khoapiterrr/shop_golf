namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Loft")]
    public partial class Loft
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Loft()
        {
            SanPham_Loft = new HashSet<SanPham_Loft>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string LoftDegress { get; set; }

        public int? OrderNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham_Loft> SanPham_Loft { get; set; }
    }
}
