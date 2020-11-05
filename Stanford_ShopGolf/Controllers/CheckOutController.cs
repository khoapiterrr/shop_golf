using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Areas.Admin.CommonAdmin;
using Stanford_ShopGolf.Extensitions;
using Stanford_ShopGolf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Controllers
{
	public class CheckOutController : Controller
	{
		// GET: CheckOut
		public ActionResult Index()
		{
			var lstGioHang = (List<HoaDonChiTiet>)SessionHelper.GetSessionGioHangForAdd();
			var model = new CheckOutItemTotal();
			if (lstGioHang != null && lstGioHang.Count > 0)
			{
				foreach (var chitiet in lstGioHang)
				{
					var sp = DataProvider.Entities.SanPhams.Find(chitiet.SanPhamId);
					decimal price = Convert.ToDecimal(sp.GiaBan.Value);
					if (sp.GiaGiam.HasValue)
					{
						price = Convert.ToDecimal(sp.GiaGiam.Value);
					}
					

					var Attributes = new List<SpecialAttributeViewModel>();
					AddAtt(Attributes, chitiet);
					model.Items.Add(new CheckOutItem()
					{
						Id = chitiet.SanPhamId ?? 0,
						Avability = sp.SoLuong > 0 ? "Còn hàng" : "Hết hàng",
						BrandName = sp.Brand != null ? sp.Brand.BrandName : "",
						Name = $" {sp.TenSanPham}",
						DeliveryOptions = "Tại nhà",
						ProductId = chitiet.SanPhamId ?? 0,
						ProductCode = $"{sp.MaSanPham}",
						Price = price,
						ProductThumbPath = (!string.IsNullOrEmpty(sp.AnhDaiDien) ? $"/{ImageURL.IMG_SANPHAM}{sp.AnhDaiDien}" : "nopicture.png"),
						Quanlity = chitiet.SoLuong.Value,
						Attributes = Attributes,
						guid = chitiet.guid
					});
				}
			}

			return View(model);
		}
		public ActionResult DeleteFormCheckout(Guid guid)
		{
			var lstGioHang = (List<HoaDonChiTiet>)SessionHelper.GetSessionGioHangForAdd();
			if (lstGioHang!=null && lstGioHang.Count>0)
			{
				var obj = lstGioHang.Where(p => p.guid == guid).FirstOrDefault();
				if (obj!=null)
				{
					lstGioHang.Remove(obj);
					SessionHelper.SetSessionGioHangForAdd(lstGioHang);
					return Json("Thành công");
				}
				
			}
			return Json("Thất bại");
		}
		public ActionResult ChangeQuantityProduct(Guid guid, int quantity)
		{
			if (quantity>0)
			{
				var lstGioHang = (List<HoaDonChiTiet>)SessionHelper.GetSessionGioHangForAdd();
				if (lstGioHang != null && lstGioHang.Count > 0)
				{
					var obj = lstGioHang.Where(p => p.guid == guid).FirstOrDefault();
					if (obj != null)
					{
						obj.SoLuong = quantity;
						SessionHelper.SetSessionGioHangForAdd(lstGioHang);
						return Json("Thành công");
					}

				}
			}
			
			return Json("Thất bại");
		}
		public ActionResult ChangeDelivery()
		{
			return PartialView();
		}

		public ActionResult Payment()
		{
			return PartialView();
		}

		public ActionResult Complete()
		{
			return PartialView();
		}
		public ActionResult CountCheckout()
		{
			var lstGioHang = (List<HoaDonChiTiet>)SessionHelper.GetSessionGioHangForAdd();
			if (lstGioHang != null && lstGioHang.Count > 0)
			{
				return Json(lstGioHang.Count);
			}
			return Json("0");
		}
		private void AddAtt(List<SpecialAttributeViewModel> Attributes, HoaDonChiTiet hoaDonChiTiet)
		{
			if (hoaDonChiTiet.ColorId > 0)
			{
				var colorName = DataProvider.Entities.Colors.Find(hoaDonChiTiet.ColorId).ColorName;
				Attributes.Add(new SpecialAttributeViewModel()
				{
					Description = "Màu sắc",
					Name = "Màu",
					Value = colorName,
					ValueType = Extensitions.ProductSpecialAttributeType.Color
				});
			}
			if (hoaDonChiTiet.GenderId > 0)
			{
				var genderName = DataProvider.Entities.Genders.Find(hoaDonChiTiet.GenderId).GenderName;
				Attributes.Add(new SpecialAttributeViewModel()
				{
					Description = "Giới tính",
					Name = "Giới tính",
					Value = genderName,
					ValueType = Extensitions.ProductSpecialAttributeType.Gender
				});
			}
			if (hoaDonChiTiet.LengthSizeId > 0)
			{
				var lengthsize = DataProvider.Entities.LengthSizes.Find(hoaDonChiTiet.LengthSizeId).LengthSize1;
				Attributes.Add(new SpecialAttributeViewModel()
				{
					Description = "Độ dài",
					Name = "Dài",
					Value = lengthsize,
					ValueType = Extensitions.ProductSpecialAttributeType.LegthSize
				});
			}
			if (hoaDonChiTiet.ShaftId > 0)
			{
				var shaft = DataProvider.Entities.Shafts.Find(hoaDonChiTiet.ShaftId).ShaftName;
				Attributes.Add(new SpecialAttributeViewModel()
				{
					Description = "Shaft",
					Name = "Shaft",
					Value = shaft,
					ValueType = Extensitions.ProductSpecialAttributeType.Shaft
				});
			}
			if (hoaDonChiTiet.WidthId > 0)
			{
				var Widths = DataProvider.Entities.Widths.Find(hoaDonChiTiet.WidthId).WidthName;
				Attributes.Add(new SpecialAttributeViewModel()
				{
					Description = "Chiều ngang",
					Name = "Ngang",
					Value = Widths,
					ValueType = Extensitions.ProductSpecialAttributeType.Width
				});
			}
			if (hoaDonChiTiet.LoftId > 0)
			{
				var LoftDegress = DataProvider.Entities.Lofts.Find(hoaDonChiTiet.LoftId).LoftDegress;
				Attributes.Add(new SpecialAttributeViewModel()
				{
					Description = "Loft degress",
					Name = "Loft degress",
					Value = LoftDegress,
					ValueType = Extensitions.ProductSpecialAttributeType.Loft
				});
			}
			if (hoaDonChiTiet.BounceId > 0)
			{
				var BounceName = DataProvider.Entities.Bounces.Find(hoaDonChiTiet.BounceId).BounceName;
				Attributes.Add(new SpecialAttributeViewModel()
				{
					Description = "Bounce",
					Name = "Bounce",
					Value = BounceName,
					ValueType = Extensitions.ProductSpecialAttributeType.Bounce
				});
			}
			if (hoaDonChiTiet.FlexId > 0)
			{
				var FlexName = DataProvider.Entities.Flexes.Find(hoaDonChiTiet.FlexId).FlexName;
				Attributes.Add(new SpecialAttributeViewModel()
				{
					Description = "Flex",
					Name = "Flex",
					Value = FlexName,
					ValueType = Extensitions.ProductSpecialAttributeType.Flex
				});
			}
			if (hoaDonChiTiet.SizeId > 0)
			{
				var SizeName = DataProvider.Entities.Sizes.Find(hoaDonChiTiet.SizeId).SizeName;
				Attributes.Add(new SpecialAttributeViewModel()
				{
					Description = "Size",
					Name = "Size",
					Value = SizeName,
					ValueType = Extensitions.ProductSpecialAttributeType.Size
				});
			}
			if (hoaDonChiTiet.InseamId > 0)
			{
				var InseamName = DataProvider.Entities.Inseams.Find(hoaDonChiTiet.InseamId).InseamName;
				Attributes.Add(new SpecialAttributeViewModel()
				{
					Description = "Đường may",
					Name = "Đường may",
					Value = InseamName,
					ValueType = Extensitions.ProductSpecialAttributeType.Inseam
				});
			}
			if (hoaDonChiTiet.WaistId > 0)
			{
				var WaistName = DataProvider.Entities.Waists.Find(hoaDonChiTiet.WaistId).WaistName;
				Attributes.Add(new SpecialAttributeViewModel()
				{
					Description = "Waisr",
					Name = "Waisr",
					Value = WaistName,
					ValueType = Extensitions.ProductSpecialAttributeType.Waist
				});
			}

		}

	}
}