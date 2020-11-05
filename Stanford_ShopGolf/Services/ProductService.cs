using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Areas.Admin.CommonAdmin;
using Stanford_ShopGolf.Extensitions;
using Stanford_ShopGolf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopGolf.Services
{
	public class ProductService
	{
		public ProductService()
		{
		}

		public IList<ProductViewModel> Get8ProductsByLoaiId(int? LoaiId)
		{
			var lstCungLoai = DataProvider.Entities.SanPhams.Where(p => p.DaDuyet == true && p.DaXoa == false && p.LoaiId == LoaiId).Take(8).Select(p => p.Id).ToList();
			var result = new List<ProductViewModel>();
			foreach (var item in lstCungLoai)
			{
				result.Add(GetProduct(item));
			}

			return result;
		}

		public ProductViewModel GetProduct(int id)
		{
			var sanPham = DataProvider.Entities.SanPhams.Find(id);
			var result = new ProductViewModel();
			var reviews = DataProvider.Entities.Reviews.Where(p => p.SanPhamId == id).ToList();
			var countReview = reviews.Count();

			result.SanPhamEntity = sanPham;
			result.Id = id;
			result.CallForPrice = true;
			result.Gtin = "Gtin";
			result.SKU = "SKU";
			result.Name = $"{sanPham.TenSanPham}";
			result.Title = $"{sanPham.TenSanPham}";
			result.LongDescription = sanPham.MoTa;
			result.ShortDescription = (!(String.IsNullOrEmpty(sanPham.MoTa)) && sanPham.MoTa.Length > 150) ? sanPham.MoTa.Substring(0, 150) : sanPham.MoTa;
			result.ShowPrice = true;
			result.ShowRate = true;
			result.HideContentHeight = 200;
			result.HideContentIfLong = true;
			result.ShowDescription = true;
			result.ShowSaleOff = true;
			result.Images = GetProductImages(id);
			result.Price = GetProductPrice(sanPham);
			result.ProductCode = $"{sanPham.MaSanPham}";
			result.Seo = GetProductSeo(id);
			result.Reviews = new ReviewViewModel()
			{
				TotalReview = countReview,
				LastReview = $"Last review comment product {id}",
				LastReviewByUserName = $"UserName",
				LastReviewOnTime = DateTime.Now,
				RateValue = sanPham.Review.HasValue ? sanPham.Review.Value : 5,
				Percent5 = (reviews.Where(p => p.ReviewCount >= 4.5 && p.ReviewCount <= 5).Count()/ (float)countReview)*100,
				Percent4 = (reviews.Where(p => p.ReviewCount > 3 && p.ReviewCount < 4.5).Count()/ (float)countReview)*100,
				Percent3 = (reviews.Where(p => p.ReviewCount > 2 && p.ReviewCount <= 3).Count()/ (float)countReview)*100,
				Percent2 = (reviews.Where(p => p.ReviewCount > 1 && p.ReviewCount <= 2).Count()/ (float)countReview)*100,
				Percent1 = (reviews.Where(p => p.ReviewCount >= 0 && p.ReviewCount <= 1).Count()/ (float)countReview)*100
			};
			result.Category = sanPham.ChuDeId.HasValue ? GetProductCategory(sanPham.ChuDeId.Value) : null;

			result.Attributes = GetProductAttributes(id);
			result.SpecialAttributes = GetProductSpecialAttributes(id);
			result.Brand = (sanPham.Brand != null) ? new BrandViewModel()
			{
				Id = sanPham.Brand.Id,
				Name = $"{sanPham.Brand.BrandName}",
				ShowImage = true,
				Title = $"{sanPham.Brand.BrandName}",
				ImagePath = $"/Content/Admin/images/Brand/{sanPham.Brand.Avatar}"
			} : new BrandViewModel();
			result.ProductType = sanPham.LoaiId;
			result.Url = $"/san-pham/{id}";
			result.FutureImagePath = (!string.IsNullOrEmpty(sanPham.AnhDaiDien) ? $"/{ImageURL.IMG_SANPHAM}{sanPham.AnhDaiDien}" : "nopicture.png");

			//nếu không có slide ảnh thì lấy ảnh đại diện
			if (result.Images.Count==0)
			{
				result.Images.Add(new PictureViewModel()
				{
					ImagePath = (!string.IsNullOrEmpty(sanPham.AnhDaiDien) ? $"/{ImageURL.IMG_SANPHAM}{sanPham.AnhDaiDien}" : "nopicture.png"),
					ThumbnailPath = (!string.IsNullOrEmpty(sanPham.AnhDaiDien) ? $"/{ImageURL.IMG_SANPHAM}{sanPham.AnhDaiDien}" : "nopicture.png"),
					Width = 600,
					Height = 400
				});
			}

			result.Top5Review = GetTop5Reivew(id);
			return result;
		}
		private List<Review> GetTop5Reivew(int spId)
		{
			return DataProvider.Entities.Reviews.Where(p => p.SanPhamId == spId).OrderByDescending(p => p.ReviewCount).Take(5).ToList();
		}
		private BrandViewModel CreateBrand(int id)
		{
			return new BrandViewModel()
			{
				Id = id,
				Name = $"Thương hiệu {id}",
				ShowImage = true,
				Title = $"Thương hiệu {id}",
				ImagePath = "/Media/Images/brand-demo.png"
			};
		}

		private IList<PictureViewModel> GetProductImages(int id)
		{
			var lstAnh = DataProvider.Entities.AnhSanPhams.Where(p => p.SanPhamId == id).ToList();
			var result = new List<PictureViewModel>();
			foreach (var item in lstAnh)
			{
				result.Add(new PictureViewModel()
				{
					Id = item.Id,
					ImagePath = $"/Content/Images/SanPham/AnhSP/{item.TenAnh}",
					ThumbnailPath = $"/Content/Images/SanPham/AnhSP/{item.TenAnh}",
					Title = $"Product {item.MoTa}",
					Width = 600,
					Height = 400
				});
			}

			return result;
		}

		private ProductPriceViewModel GetProductPrice(int id)
		{
			var random = new Random();
			var oldPrice = random.Next(10, 100) * 1000000;
			return new ProductPriceViewModel()
			{
				CurrentcyFormat = "{0:0,0} đ",
				OldPrice = oldPrice,
				Price = oldPrice * random.Next(5, 10) / 10
			};
		}
		private ProductPriceViewModel GetProductPrice(SanPham sp)
		{
		
			var pd = new ProductPriceViewModel()
			{
				CurrentcyFormat = "{0:0,0} đ",
			};
			if (sp.GiaGiam!= null)
			{
				pd.OldPrice = (decimal)sp.GiaBan;
				pd.Price = (decimal)sp.GiaGiam;
			}
			else
			{
				pd.Price = (decimal)sp.GiaBan;
			}
			return pd;
		}
		private IList<SpecialAttributeViewModel> GetProductSpecialAttributes(int id)
		{
			var result = new List<SpecialAttributeViewModel>();
			result.Add(new SpecialAttributeViewModel()
			{
				Name = "Color",
				Value = "Red",
				ValueType = ProductSpecialAttributeType.Color,
				Description = "Red color"
			});
			result.Add(new SpecialAttributeViewModel()
			{
				Name = "Size",
				Value = "M",
				ValueType = ProductSpecialAttributeType.Size,
				Description = "Medium"
			});
			return result;
		}

		private IList<AttributeViewModel> GetProductAttributes(int id)
		{
			var result = new List<AttributeViewModel>();
			for (var i = 0; i < 5; i++)
			{
				result.Add(new AttributeViewModel()
				{
					Name = $"Attribute {i}",
					Value = $"{i}",
					Description = $"Description {i}",
					ValueType = ProductAttributeType.TextBox
				});
			}
			return result;
		}

		private CategoryViewModel GetProductCategory(int id)
		{
			ChuDe objChuDe = DataProvider.Entities.ChuDes.Find(id);
			CategoryViewModel result = new CategoryViewModel
			{
				Id = objChuDe.Id,
				Name = objChuDe.TenChuDe,
				Title = objChuDe.TenChuDe
			};
			return result;
		}

		private ReviewViewModel GetProductReviews(int id)
		{
			var random = new Random();
			return new ReviewViewModel()
			{
				TotalReview = random.Next(5, 20),
				LastReview = $"Last review comment product {id}",
				LastReviewByUserName = $"UserName",
				LastReviewOnTime = DateTime.Now,
				RateValue = random.Next(1, 10) / 10 + random.Next(1, 5)
			};
		}

		private SeoViewModel GetProductSeo(int id)
		{
			return new SeoViewModel()
			{
				Id = id,
				MetaDescription = $"Meta Description for Seo {id}",
				MetaKeywork = $"Meta Keywork for Seo {id}",
				MetaTitle = $"Meta title for Seo {id}",
				SeoName = $"Meta title for Seo {id}"
			};
		}
	}
}