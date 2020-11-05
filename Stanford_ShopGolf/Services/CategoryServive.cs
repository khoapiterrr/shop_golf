using PagedList;
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
    public class CategoryServive
    {
        private readonly ShopGolfEntities _context;

        public CategoryServive()
        {
            _context = new ShopGolfEntities();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type">
        /// 0_get theo chủ đề
        /// 1_get theo brand
        /// 2_get theo color
        /// 3_get theo flex
        /// 4_get theo size
        /// 5_get theo loft
        /// 6_get theo Inseam
        /// 7_get theo Width
        /// 8_get theo Shaft
        /// 9_get theo Waist
        /// 10_get theo Brouce
        /// 11_get theo LengthSize
        /// </param>
        /// <param name="pageNumber"></param>
        /// <param name="typeSort"></param>
        /// <returns></returns>
        public CategoryViewModel GetProduct(int id, int? filter, int? typeFilter, int? pageNumber, int? typeSort)
        {
            var filterBar = filter ?? 0;
            var type = typeFilter ?? 0;
            var page = pageNumber ?? 1;
            var sort = typeSort ?? 0;
            var selectedChuDe = DataProvider.Entities.ChuDes.Find(id);
            var result = new CategoryViewModel()
            {
                Id = id,
                Icon = "fa fa-list",
                Name = $"Danh mục thêm chủ đề {id} tên : {selectedChuDe.TenChuDe}",
                Title = selectedChuDe.TenChuDe,
                UrlPath = $"category/{id}",
                SubTitle = $"Sub title category {id}",
                PageIndex = 0,
                PageSize = 10,
                ShowProducts = true,
                TotalProduct = 10,
                ShowSubcategories = true,
            };
            result.SideBars = GetAllSideBars(selectedChuDe);
            var dataProduct = GetProductByType(id, type, filterBar);
            //Sắp xêp theo ngày cập nhật
            if (sort == 0)
            {
                dataProduct = dataProduct.OrderByDescending(x => x.UpdatedDate).ToList();
            }
            //Giá tăng dần
            else if (sort == 1)
            {
                dataProduct = dataProduct.OrderBy(x => x.Price.Price).ToList();
            }
            //Giá giảm dần
            else if (sort == 2)
            {
                dataProduct = dataProduct.OrderByDescending(x => x.Price.Price).ToList();
            }
            //ưu thích nhất
            else if (sort == 3)
            {
                dataProduct = dataProduct.OrderByDescending(x => x.Reviews.RateValue).ToList();
            }

            if (dataProduct != null)
            {
                var lstData = dataProduct.ToPagedList((page), ConstantData.PAGE_SIZE);
                result.TotalPage = lstData.PageCount;
                result.TotalItem = lstData.TotalItemCount;
                result.Products = lstData.ToList();
            }
            //result.TotalPage = Math.Ceiling(result.Products.Count() * 1.0 / ConstantData.PAGE_SIZE);
            //result.Products = dataProduct.Skip((page - 1) * 1).Take(1).ToList();

            return result;
        }

        private object GetDataFillTitle(int id, int v)
        {
            throw new NotImplementedException();
        }

        private IList<ProductViewModel> GetProductByType(int id, int type, int filter)
        {
            switch (type)
            {
                case 1:
                    return GetProductByBrandCode(id, filter);

                case 2:
                    return GetProductByColorCode(id, filter);

                case 3:
                    return GetProductByFlexCode(id, filter);

                case 4:
                    return GetProductBySizeCode(id, filter);

                case 5:
                    return GetProductByLoftCode(id, filter);

                case 6:
                    return GetProductByInseamCode(id, filter);

                case 7:
                    return GetProductByWidthCode(id, filter);

                case 8:
                    return GetProductByShaftCode(id, filter);

                case 9:
                    return GetProductByWaistCode(id, filter);

                case 10:
                    return GetProductByBrouceCode(id, filter);

                case 11:
                    return GetProductByLengthSizeCode(id, filter);

                default:
                    return GetProductByCategoryCode(id);
            }
        }

        private SidebarViewModel GetAllSideBars(ChuDe item)
        {
            var data = new SidebarViewModel();
            if (item.MaCha == 0)
            {
                data.ShowChuDe = true;
                data.ChuDes = DataProvider.Entities.ChuDes.Where(x => x.MaCha == item.Id).Select(x => new SideBarSubViewModel
                {
                    Id = x.Id,
                    Name = x.TenChuDe,
                    Count = DataProvider.Entities.SanPhams.Where(p => p.ChuDeId == x.Id).Count(),
                    Url = "/category/" + x.Id
                }).ToList();
            }
            //Brand type = 1
            data.Brands = DataProvider.Entities.Brands.Select(x => new SideBarSubViewModel
            {
                Id = x.Id,
                Name = x.BrandName,
                Count = DataProvider.Entities.SanPhams.Where(p => p.BrandId == x.Id).Count(),
                Url = "/category/" + item.Id + "?filter=" + x.Id + "&type=1"
            }).ToList();
            //Color
            data.Colors = DataProvider.Entities.Colors.Select(x => new SideBarSubViewModel
            {
                Id = x.Id,
                Name = x.ColorCode,
                Count = DataProvider.Entities.SanPham_Color.Where(p => p.ColorId == x.Id).Count(),
                Url = "/category/" + item.Id + "?filter=" + x.Id + "&type=2"
            }).ToList();
            // Flex type =3
            data.Flex = DataProvider.Entities.Flexes.Select(x => new SideBarSubViewModel
            {
                Id = x.Id,
                Name = x.FlexName,
                Count = DataProvider.Entities.SanPham_Flex.Where(p => p.FlexId == x.Id).Count(),
                Url = "/category/" + item.Id + "?filter=" + x.Id + "&type=3"
            }).ToList();
            //size type = 4
            data.Sizes = DataProvider.Entities.Sizes.Select(x => new SideBarSubViewModel
            {
                Id = x.Id,
                Name = x.SizeName,
                Count = DataProvider.Entities.SanPham_Size.Where(p => p.SizeId == x.Id).Count(),
                Url = "/category/" + item.Id + "?filter=" + x.Id + "&type=4"
            }).ToList();
            // loft type = 5
            data.Loft = DataProvider.Entities.Lofts.Select(x => new SideBarSubViewModel
            {
                Id = x.Id,
                Name = x.LoftDegress,
                Count = DataProvider.Entities.SanPham_Loft.Where(p => p.LoftId == x.Id).Count(),
                Url = "/category/" + item.Id + "?filter=" + x.Id + "&type=5"
            }).ToList();
            // Inseam type = 6
            data.Inseam = DataProvider.Entities.Inseams.Select(x => new SideBarSubViewModel
            {
                Id = x.Id,
                Name = x.InseamName,
                Count = DataProvider.Entities.SanPham_Inseam.Where(p => p.InseamId == x.Id).Count(),
                Url = "/category/" + item.Id + "?filter=" + x.Id + "&type=6"
            }).ToList();
            // width type = 7
            data.Widths = DataProvider.Entities.Widths.Select(x => new SideBarSubViewModel
            {
                Id = x.Id,
                Name = x.WidthName,
                Count = DataProvider.Entities.SanPham_Width.Where(p => p.WidthId == x.Id).Count(),
                Url = "/category/" + item.Id + "?filter=" + x.Id + "&type=7"
            }).ToList();
            //Shaft type = 8
            data.Shaft = DataProvider.Entities.Shafts.Select(x => new SideBarSubViewModel
            {
                Id = x.Id,
                Name = x.ShaftName,
                Count = DataProvider.Entities.SanPham_Shaft.Where(p => p.ShaftId == x.Id).Count(),
                Url = "/category/" + item.Id + "?filter=" + x.Id + "&type=8"
            }).ToList();
            //Waist type = 9
            data.Waist = DataProvider.Entities.Waists.Select(x => new SideBarSubViewModel
            {
                Id = x.Id,
                Name = x.WaistName,
                Count = DataProvider.Entities.SanPham_Waist.Where(p => p.WaistId == x.Id).Count(),
                Url = "/category/" + item.Id + "?filter=" + x.Id + "&type=9"
            }).ToList();
            //Brouce type = 10
            data.Brouce = DataProvider.Entities.Bounces.Select(x => new SideBarSubViewModel
            {
                Id = x.Id,
                Name = x.BounceName,
                Count = DataProvider.Entities.SanPham_Bounce.Where(p => p.BounceId == x.Id).Count(),
                Url = "/category/" + item.Id + "?filter=" + x.Id + "&type=10"
            }).ToList();

            //LengthSize type = 11
            data.Brouce = DataProvider.Entities.LengthSizes.Select(x => new SideBarSubViewModel
            {
                Id = x.Id,
                Name = x.LengthSize1,
                Count = DataProvider.Entities.SanPham_LengthSize.Where(p => p.LengthId == x.Id).Count(),
                Url = "/category/" + item.Id + "?filter=" + x.Id + "&type=11"
            }).ToList();

            if (item.LoaiId == (int)ProductTypeEnum.AO)
            {
                data.ShowBrand = data.Brands.Count() != 0;
                data.ShowSize = data.Sizes.Count() != 0;
                data.ShowWidth = data.Widths.Count() != 0;
                data.ShowColor = data.Colors.Count != 0;
                data.ShowLoft = data.Loft.Count() != 0;
            }
            else if (item.LoaiId == (int)ProductTypeEnum.GAY_GOLF)
            {
                data.ShowBrand = data.Brands.Count() != 0;
                data.ShowSize = data.Sizes.Count() != 0;
                data.ShowWidth = data.Widths.Count() != 0;
                data.ShowLoft = data.Loft.Count() != 0;
                data.ShowShaft = data.Shaft.Count() != 0;
                data.ShowInseam = data.Inseam.Count() != 0;
            }
            else if (item.LoaiId == (int)ProductTypeEnum.GIAY)
            {
                data.ShowBrand = data.Brands.Count() != 0;
                data.ShowSize = data.Sizes.Count() != 0;
                data.ShowWidth = data.Widths.Count() != 0;
                data.ShowColor = data.Colors.Count != 0;
                data.ShowLoft = data.Loft.Count() != 0;
            }
            return data;
        }

        private IList<ProductViewModel> GetProductByCategoryCode(int id)
        {
            var lstProduct = new List<ProductViewModel>();
            var lstdata = GetSanPhamByChuDe(id).ToList();

            foreach (var dto in lstdata)
            {
                lstProduct.Add(ConvertDataProduct(dto));
            }
            return lstProduct;
        }

        private IList<SanPham> GetSanPhamByChuDe(int id)
        {
            var item = DataProvider.Entities.ChuDes.Find(id);
            var lstProduct = new List<ProductViewModel>();
            var lstdata = new List<SanPham>();
            if (item.MaCha == 0)
            {
                lstdata = DataProvider.Entities.SanPhams.Where(x => x.LoaiId == item.LoaiId).ToList();
            }
            else
            {
                lstdata = DataProvider.Entities.SanPhams.Where(x => x.ChuDeId == item.Id).ToList();
            }
            return lstdata;
        }

        private IList<ProductViewModel> GetProductByBrandCode(int id, int filter)
        {
            var lstProduct = new List<ProductViewModel>();

            var lstdata = GetSanPhamByChuDe(id).Where(x => x.BrandId == filter).ToList();
            if (filter != 0)
            {
            }
            foreach (var dto in lstdata)
            {
                lstProduct.Add(ConvertDataProduct(dto));
            }
            return lstProduct;
        }

        private IList<ProductViewModel> GetProductByColorCode(int id, int filter)
        {
            var lstProduct = new List<ProductViewModel>();
            var lstdata = GetSanPhamByChuDe(id)
                .Join(DataProvider.Entities.SanPham_Color.Where(x => x.ColorId == filter),
                p => p.Id,
                q => q.SanPhamId,
                (p, q) => p).ToList();
            foreach (var dto in lstdata)
            {
                lstProduct.Add(ConvertDataProduct(dto));
            }
            return lstProduct;
        }

        private IList<ProductViewModel> GetProductByFlexCode(int id, int filter)
        {
            var lstProduct = new List<ProductViewModel>();
            var lstdata = GetSanPhamByChuDe(id)
                .Join(DataProvider.Entities.SanPham_Flex.Where(x => x.FlexId == filter),
                p => p.Id,
                q => q.SanPhamId,
                (p, q) => p).ToList();
            foreach (var dto in lstdata)
            {
                lstProduct.Add(ConvertDataProduct(dto));
            }
            return lstProduct;
        }

        private IList<ProductViewModel> GetProductBySizeCode(int id, int filter)
        {
            var lstProduct = new List<ProductViewModel>();
            var lstdata = GetSanPhamByChuDe(id)
                .Join(DataProvider.Entities.SanPham_Size.Where(x => x.SizeId == filter),
                p => p.Id,
                q => q.SanPhamId,
                (p, q) => p).ToList();
            foreach (var dto in lstdata)
            {
                lstProduct.Add(ConvertDataProduct(dto));
            }
            return lstProduct;
        }

        private IList<ProductViewModel> GetProductByLoftCode(int id, int filter)
        {
            var lstProduct = new List<ProductViewModel>();
            var lstdata = GetSanPhamByChuDe(id)
                .Join(DataProvider.Entities.SanPham_Loft.Where(x => x.LoftId == filter),
                p => p.Id,
                q => q.SanPhamId,
                (p, q) => p).ToList();
            foreach (var dto in lstdata)
            {
                lstProduct.Add(ConvertDataProduct(dto));
            }
            return lstProduct;
        }

        private IList<ProductViewModel> GetProductByInseamCode(int id, int filter)
        {
            var lstProduct = new List<ProductViewModel>();
            var lstdata = GetSanPhamByChuDe(id)
                .Join(DataProvider.Entities.SanPham_Inseam.Where(x => x.InseamId == filter),
                p => p.Id,
                q => q.SanPhamId,
                (p, q) => p).ToList();
            foreach (var dto in lstdata)
            {
                lstProduct.Add(ConvertDataProduct(dto));
            }
            return lstProduct;
        }

        private IList<ProductViewModel> GetProductByWidthCode(int id, int filter)
        {
            var lstProduct = new List<ProductViewModel>();
            var lstdata = GetSanPhamByChuDe(id)
                .Join(DataProvider.Entities.SanPham_Width.Where(x => x.WidthId == filter),
                p => p.Id,
                q => q.SanPhamId,
                (p, q) => p).ToList();
            foreach (var dto in lstdata)
            {
                lstProduct.Add(ConvertDataProduct(dto));
            }
            return lstProduct;
        }

        private IList<ProductViewModel> GetProductByShaftCode(int id, int filter)
        {
            var lstProduct = new List<ProductViewModel>();
            var lstdata = GetSanPhamByChuDe(id)
                .Join(DataProvider.Entities.SanPham_Shaft.Where(x => x.ShaftId == filter),
                p => p.Id,
                q => q.SanPhamId,
                (p, q) => p).ToList();
            foreach (var dto in lstdata)
            {
                lstProduct.Add(ConvertDataProduct(dto));
            }
            return lstProduct;
        }

        private IList<ProductViewModel> GetProductByWaistCode(int id, int filter)
        {
            var lstProduct = new List<ProductViewModel>();
            var lstdata = GetSanPhamByChuDe(id)
                .Join(DataProvider.Entities.SanPham_Waist.Where(x => x.WaistId == filter),
                p => p.Id,
                q => q.SanPhamId,
                (p, q) => p).ToList();
            foreach (var dto in lstdata)
            {
                lstProduct.Add(ConvertDataProduct(dto));
            }
            return lstProduct;
        }

        private IList<ProductViewModel> GetProductByLengthSizeCode(int id, int filter)
        {
            var lstProduct = new List<ProductViewModel>();
            var lstdata = GetSanPhamByChuDe(id)
                .Join(DataProvider.Entities.SanPham_LengthSize.Where(x => x.LengthId == filter),
                p => p.Id,
                q => q.SanPhamId,
                (p, q) => p).ToList();
            foreach (var dto in lstdata)
            {
                lstProduct.Add(ConvertDataProduct(dto));
            }
            return lstProduct;
        }

        private IList<ProductViewModel> GetProductByBrouceCode(int id, int filter)
        {
            var lstProduct = new List<ProductViewModel>();
            var lstdata = GetSanPhamByChuDe(id)
                .Join(DataProvider.Entities.SanPham_Bounce.Where(x => x.BounceId == filter),
                p => p.Id,
                q => q.SanPhamId,
                (p, q) => p).ToList();
            foreach (var dto in lstdata)
            {
                lstProduct.Add(ConvertDataProduct(dto));
            }
            return lstProduct;
        }

        private ProductViewModel ConvertDataProduct(SanPham item)
        {
            var result = new ProductViewModel
            {
                Id = item.Id,
                //CallForPrice = random.NextBool(),
                //Gtin = random.NextString(10, false),
                //SKU = random.NextString(10, false),
                Name = $"{item.TenSanPham}",
                Title = $"{item.TenSanPham} - {item.MaSanPham}",
                //LongDescription = longDesc,
                //ShortDescription = longDesc.Substring(0, 150),
                ShowPrice = true,
                ShowRate = true,
                HideContentHeight = 200,
                HideContentIfLong = true,
                ShowDescription = true,
                ShowSaleOff = true,
                //Images = GetProductImages(item),
                Price = GetProductPrice(item),
                ProductCode = $"{item.TenSanPham}-{item.MaSanPham}",
                //Seo = GetProductSeo(id),
                Reviews = GetProductReviews(item),
                //Category = GetProductCategory(id),
                Attributes = GetProductAttributes(item.Id),
                SpecialAttributes = GetProductSpecialAttributes(item.Id),
                Brand = CreateBrand(item.Brand),
                UpdatedDate = item.NgayCapNhat.Value
            };
            result.Url = $"/san-pham/{item.Id}";
            result.FutureImagePath = $"/{ImageURL.IMG_SANPHAM}{item.AnhDaiDien}";
            return result;
        }

        private BrandViewModel CreateBrand(Brand item)
        {
            if (item == null)
            {
                return null;
            }
            return new BrandViewModel()
            {
                Id = item.Id,
                Name = $"Thương hiệu {item.BrandName}",
                ShowImage = true,
                Title = $"Thương hiệu {item.BrandName}",
                ImagePath = $"/{ImageURL.IMG_BRAND}/{item.Avatar}"
            };
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

        private ReviewViewModel GetProductReviews(SanPham item)
        {
            var data = new ReviewViewModel()
            {
                TotalReview = 12,
                LastReview = $"Last review comment product {item.Id}",
                LastReviewByUserName = $"UserName",
                LastReviewOnTime = DateTime.Now,
                RateValue = item.Review.HasValue ? item.Review.Value : 5
            };
            return data;
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

        private ProductPriceViewModel GetProductPrice(SanPham item)
        {
            var price = item.GiaBan;
            var priceDiscount = item.GiaGiam;
            var data = new ProductPriceViewModel()
            {
                CurrentcyFormat = "{0:0,0} đ",
                OldPrice = priceDiscount.HasValue ? (decimal)priceDiscount.Value : 0,
                Price = price.HasValue ? (decimal)price.Value : 0,
            };
            return data;
        }
    }
}