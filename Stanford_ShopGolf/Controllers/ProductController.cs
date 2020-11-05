using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Areas.Admin.CommonAdmin;
using Stanford_ShopGolf.Extensitions;
using Stanford_ShopGolf.Services;
using Stanford_ShopGolf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Controllers
{
	public class ProductController : Controller
	{
		private readonly DemoService demoService;
		private readonly ProductService productService;

		public ProductController()
		{
			demoService = new DemoService();
			productService = new ProductService();
		}

		// GET: Product
		public ActionResult Index(int id)
		{
			var model = productService.GetProduct(id);

			return View(model);
		}

		public ActionResult RecentViewedProducts(int? loaiSP)
		{
			if (loaiSP.HasValue && loaiSP > 0)
			{
				var model = productService.Get8ProductsByLoaiId(loaiSP).Take(8).ToList();
				return PartialView(model);
			}
			else
			{
				var model = new List<ProductViewModel>();
				return PartialView(model);
			}
		}

		public ActionResult GetMatchingProduct(string term)
		{
			Response.CacheControl = "no-cache";

			var lstProduct = DataProvider.Entities.SanPhams.Where(a => a.DaDuyet == true && a.DaXoa != true && (a.TenSanPham.StartsWith(term) ||
				   a.MaSanPham.StartsWith(term))).Take(5).ToList();
			var lst = lstProduct.Select(m => new
			{
				id = m.Id,
				value = m.TenSanPham,
				label = m.TenSanPham,
				description = m.MaSanPham,
				price = Convert.ToDecimal(m.GiaBan).ToString("###,###.##"),
				image = Url.Content((string.IsNullOrEmpty(m.AnhDaiDien) ?
				 "nopicture.png" : $"/{ImageURL.IMG_SANPHAM}{m.AnhDaiDien}"))
			}).ToList().Take(5);
			return Json(lst.ToArray(), JsonRequestBehavior.AllowGet);
		}

		public ActionResult QuickView(int id)
		{
			var model = productService.GetProduct(id);
			return PartialView(model);
		}
		public ActionResult WriteReview(float? rateValue, string txtTitleComment, string txtContentComment, int idSP)
		{
			Review review = new Review
			{
				NgayTao = DateTime.Now,
				NoiDung = txtContentComment,
				SanPhamId = idSP,
				TieuDe = txtTitleComment,
				NguoiDungId = 1
			};
			if (rateValue.HasValue)
			{
				review.ReviewCount = Math.Ceiling(rateValue.Value);
			}
			else
			{
				review.ReviewCount = 0;
			}
			
			DataProvider.Entities.Reviews.Add(review);

			var sp= DataProvider.Entities.SanPhams.Find(idSP);
			
			var lstReview = DataProvider.Entities.Reviews.Where(p => p.SanPhamId == idSP).ToList();
			lstReview.Add(review);
			DataProvider.Entities.SaveChanges();
			if (lstReview.Count>0)
			{
				sp.Review = lstReview.Select(p=>p.ReviewCount).Average();
				 
			}
			DataProvider.Entities.SaveChanges();
			return RedirectToAction("Index", new { id = idSP });
		}
		public ActionResult InsertToGioHang(HoaDonChiTiet hoaDonChiTiet)
		{
		
			try
			{
				List<HoaDonChiTiet> lstGioHang = (List<HoaDonChiTiet>)SessionHelper.GetSessionGioHangForAdd();
				if (lstGioHang == null || lstGioHang.Count==0)
				{
					lstGioHang = new List<HoaDonChiTiet>();
					hoaDonChiTiet.guid = Guid.NewGuid();
					lstGioHang.Add(hoaDonChiTiet);
					SessionHelper.SetSessionGioHangForAdd(lstGioHang);
				}
				else
				{
					var temp = lstGioHang.Where(p => CompareTwoOrderDetail(p, hoaDonChiTiet)).FirstOrDefault();
					if (temp!= null)
					{
						temp.SoLuong = temp.SoLuong + hoaDonChiTiet.SoLuong;
					}
					else
					{
						hoaDonChiTiet.guid = Guid.NewGuid();
						lstGioHang.Add(hoaDonChiTiet);
					}
					SessionHelper.SetSessionGioHangForAdd(lstGioHang);
				}
				return Json("Thành công");
			}
			catch (Exception ex)
			{
				return Json("Thất bại");
			}
		}
		private bool CompareTwoOrderDetail(HoaDonChiTiet first, HoaDonChiTiet second)
		{
			if (
				first.GenderId != second.GenderId	||
				first.LengthSizeId != second.LengthSizeId ||
				first.ShaftId !=second.ShaftId	||
				first.WidthId != second.WidthId ||
				first.LoftId != second.LoftId ||
				first.BounceId !=second.BounceId ||
				first.FlexId != second.FlexId ||
				first.SizeId != second.SizeId ||
				first.InseamId != second.InseamId ||
				first.WaistId != second.WaistId ||
				first.ColorId != second.ColorId
				)
			{
				return false;
			}
			return true;
		}
	}
}