namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Shaft")]
    public partial class Shaft
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shaft()
        {
            SanPham_Shaft = new HashSet<SanPham_Shaft>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string ShaftName { get; set; }

        public int? OrderNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham_Shaft> SanPham_Shaft { get; set; }
    }
}
