namespace Stanford.ShopGolf.Business.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Data.Entity.Infrastructure;
    using Stanford.ShopGolf.Business.SQLQuery;

    public partial class ShopGolfEntities : DbContext
    {
        public ShopGolfEntities()
            : base("name=ShopGolfEntities")
        {
        }

        public virtual DbSet<AnhSanPham> AnhSanPhams { get; set; }
        public virtual DbSet<Bounce> Bounces { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<CauHinh> CauHinhs { get; set; }
        public virtual DbSet<ChucNang> ChucNangs { get; set; }
        public virtual DbSet<ChuDe> ChuDes { get; set; }
        public virtual DbSet<ChuDeTin> ChuDeTins { get; set; }
        public virtual DbSet<ChuongTrinhUuDai> ChuongTrinhUuDais { get; set; }
        public virtual DbSet<ChuongTrinhUuDai_SanPham> ChuongTrinhUuDai_SanPham { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<DataType> DataTypes { get; set; }
        public virtual DbSet<EmailMarketing> EmailMarketings { get; set; }
        public virtual DbSet<Flex> Flexes { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<HanhDong> HanhDongs { get; set; }
        public virtual DbSet<HoaDonBan> HoaDonBans { get; set; }
        public virtual DbSet<HoaDonChiTiet> HoaDonChiTiets { get; set; }
        public virtual DbSet<Inseam> Inseams { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LengthSize> LengthSizes { get; set; }
        public virtual DbSet<LoaiKhachHang> LoaiKhachHangs { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public virtual DbSet<Loft> Lofts { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<NhatKyHeThong> NhatKyHeThongs { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }
        public virtual DbSet<QuocGia> QuocGias { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<SanPham_Bounce> SanPham_Bounce { get; set; }
        public virtual DbSet<SanPham_Color> SanPham_Color { get; set; }
        public virtual DbSet<SanPham_Flex> SanPham_Flex { get; set; }
        public virtual DbSet<SanPham_Inseam> SanPham_Inseam { get; set; }
        public virtual DbSet<SanPham_LengthSize> SanPham_LengthSize { get; set; }
        public virtual DbSet<SanPham_Loft> SanPham_Loft { get; set; }
        public virtual DbSet<SanPham_Shaft> SanPham_Shaft { get; set; }
        public virtual DbSet<SanPham_Size> SanPham_Size { get; set; }
        public virtual DbSet<SanPham_Waist> SanPham_Waist { get; set; }
        public virtual DbSet<SanPham_Width> SanPham_Width { get; set; }
        public virtual DbSet<Shaft> Shafts { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<ThamSo> ThamSoes { get; set; }
        public virtual DbSet<ThuocTinhGiaTri> ThuocTinhGiaTris { get; set; }
        public virtual DbSet<ThuocTinhSanPham> ThuocTinhSanPhams { get; set; }
        public virtual DbSet<TinhThanh> TinhThanhs { get; set; }
        public virtual DbSet<TinTuc> TinTucs { get; set; }
        public virtual DbSet<TrangThai> TrangThais { get; set; }
        public virtual DbSet<VaiTro> VaiTroes { get; set; }
        public virtual DbSet<Waist> Waists { get; set; }
        public virtual DbSet<Width> Widths { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnhSanPham>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<Brand>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<CauHinh>()
                .Property(e => e.Logo)
                .IsUnicode(false);

            modelBuilder.Entity<CauHinh>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<CauHinh>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<CauHinh>()
                .Property(e => e.Website)
                .IsUnicode(false);

            modelBuilder.Entity<ChucNang>()
                .Property(e => e.TenForm)
                .IsUnicode(false);

            modelBuilder.Entity<ChucNang>()
                .Property(e => e.Module)
                .IsUnicode(false);

            modelBuilder.Entity<ChuDe>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<ChuDeTin>()
                .HasMany(e => e.TinTucs)
                .WithOptional(e => e.ChuDeTin)
                .HasForeignKey(e => e.ChuDeId);

            modelBuilder.Entity<ChuongTrinhUuDai>()
                .HasMany(e => e.ChuongTrinhUuDai_SanPham)
                .WithOptional(e => e.ChuongTrinhUuDai)
                .HasForeignKey(e => e.ChuongTrinhId);

            modelBuilder.Entity<Color>()
                .Property(e => e.ColorCode)
                .IsUnicode(false);

            modelBuilder.Entity<DataType>()
                .HasMany(e => e.ThuocTinhSanPhams)
                .WithOptional(e => e.DataType)
                .HasForeignKey(e => e.TypeId);

            modelBuilder.Entity<EmailMarketing>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<EmailMarketing>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonBan>()
                .HasMany(e => e.HoaDonChiTiets)
                .WithOptional(e => e.HoaDonBan)
                .HasForeignKey(e => e.HoaDonId);

			modelBuilder.Entity<HoaDonChiTiet>()
				.Ignore(e => e.guid);

			modelBuilder.Entity<Inseam>()
                .Property(e => e.InseamName)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MaSoThue)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MaSoCongDan)
                .IsUnicode(false);

            modelBuilder.Entity<LengthSize>()
                .HasMany(e => e.SanPham_LengthSize)
                .WithOptional(e => e.LengthSize)
                .HasForeignKey(e => e.LengthId);

            modelBuilder.Entity<LoaiKhachHang>()
                .HasMany(e => e.KhachHangs)
                .WithOptional(e => e.LoaiKhachHang)
                .HasForeignKey(e => e.LoaiId);

            modelBuilder.Entity<LoaiSanPham>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.LoaiSanPham)
                .HasForeignKey(e => e.LoaiId);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.NguoiDung)
                .HasForeignKey(e => e.NguoiTaoId);

            modelBuilder.Entity<NhatKyHeThong>()
                .Property(e => e.FormName)
                .IsUnicode(false);

            modelBuilder.Entity<NhatKyHeThong>()
                .Property(e => e.DiaChiMayTinh)
                .IsUnicode(false);

            modelBuilder.Entity<QuocGia>()
                .Property(e => e.MaQuocGia)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.MaSanPham)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.AnhDaiDien)
                .IsUnicode(false);

            modelBuilder.Entity<Slider>()
                .Property(e => e.AnhDaiDien)
                .IsUnicode(false);

            modelBuilder.Entity<Slider>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<ThamSo>()
                .Property(e => e.TenThamSo)
                .IsUnicode(false);

            modelBuilder.Entity<ThuocTinhSanPham>()
                .Property(e => e.TenThuocTinh)
                .IsUnicode(false);

            modelBuilder.Entity<ThuocTinhSanPham>()
                .HasMany(e => e.ThuocTinhGiaTris)
                .WithOptional(e => e.ThuocTinhSanPham)
                .HasForeignKey(e => e.ThuocTinhId);

            modelBuilder.Entity<TinhThanh>()
                .Property(e => e.MaTinh)
                .IsUnicode(false);

            modelBuilder.Entity<Waist>()
                .Property(e => e.WaistName)
                .IsUnicode(false);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}