namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LengthSize")]
    public partial class LengthSize
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LengthSize()
        {
            SanPham_LengthSize = new HashSet<SanPham_LengthSize>();
        }

        public int Id { get; set; }

        [Column("LengthSize")]
        [StringLength(30)]
        public string LengthSize1 { get; set; }

        public int? OrderNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham_LengthSize> SanPham_LengthSize { get; set; }
    }
}
