namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuocGia")]
    public partial class QuocGia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuocGia()
        {
            SanPhams = new HashSet<SanPham>();
            TinhThanhs = new HashSet<TinhThanh>();
        }

        public int Id { get; set; }

        [StringLength(10)]
        public string MaQuocGia { get; set; }

        [StringLength(50)]
        public string TenQuocGia { get; set; }

        public int? SapXep { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TinhThanh> TinhThanhs { get; set; }
    }
}
