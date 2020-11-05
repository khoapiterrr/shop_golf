namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThuocTinhSanPham")]
    public partial class ThuocTinhSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThuocTinhSanPham()
        {
            ThuocTinhGiaTris = new HashSet<ThuocTinhGiaTri>();
        }

        public int Id { get; set; }

        public int? SanPhamId { get; set; }

        [StringLength(50)]
        public string TenThuocTinh { get; set; }

        [StringLength(500)]
        public string GiaTri { get; set; }

        public int? TypeId { get; set; }

        public virtual DataType DataType { get; set; }

        public virtual SanPham SanPham { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThuocTinhGiaTri> ThuocTinhGiaTris { get; set; }
    }
}
