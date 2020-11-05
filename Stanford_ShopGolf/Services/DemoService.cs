using Stanford_ShopGolf.Extensitions;
using Stanford_ShopGolf.Models;
using Stanford_ShopGolf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;

namespace Stanford_ShopGolf.Services
{
    public class DemoService
    {
        private readonly ShopGolfEntities _context;

        public DemoService()
        {
            _context = new ShopGolfEntities();
        }

        public List<MenuViewModel> GetTopNavModels()
        {
            var result = new List<MenuViewModel>();
           /* result.Add(new MenuViewModel()
            {
                Id = 0,
                Name = "Golf                                                                                                                                                                                                                                                                                              ",
                Order = 0,
                Url = "/category/0"
            });
            result.Add(new MenuViewModel()
            {
                Id = 0,
                Name = "Tenis",
                Order = 0,
                Url = "/category/1"
            });*/
            return result;
        }

        public MenuViewModel GetFooterMenu(string menuName)
        {
            var result = new MenuViewModel()
            {
                Id = -1,
                Name = $"Menu Name {-1}",
                Url = "/menuName"
            };
            result.Children = new List<MenuViewModel>();
            var random = new Random();
            for (var i = 0; i < random.Next(5, 10); i++)
            {
                result.Children.Add(new MenuViewModel()
                {
                    Id = i,
                    Name = $"Menu Name {i}",
                    Url = "#"
                });
            }
            return result;
        }

        public List<MenuViewModel> GetMenuCategories()
        {
            var result = new List<MenuViewModel>();
            var random = new Random();
            var maxLevel2 = random.Next(2, 5);
            for (var m = 0; m < 8; m++)
            {
                var itemLevel1 = new MenuViewModel()
                {
                    Id = m,
                    Name = $"Menu - {m}",
                    HeadTitle = $"Head Title Menu {m}",
                    Description = $"Description menu {m}",
                    ImagePath = "/Media/Images/CAT-NAV-DRIVER-NEW.png",
                    Order = m,
                    Url = $"/category/{m}"
                };
                for (var i = 0; i < maxLevel2; i++)
                {
                    var itemLevel2 = new MenuViewModel()
                    {
                        Id = i,
                        Name = $"Menu {m}.{i}",
                        HeadTitle = $"Head Title Menu {m}.{i}",
                        Description = $"Description menu {m}.{i}",
                        ImagePath = string.Empty,
                        Order = i,
                        Url = $"/category/{m}-{i}"
                    };
                    var maxLevel3 = random.Next(5, 10);
                    for (var j = 0; j < maxLevel3; j++)
                    {
                        var itemLevel3 = new MenuViewModel()
                        {
                            Id = i,
                            Name = $"Menu {m}.{i}.{j}",
                            HeadTitle = $"Head Title Menu {m}.{i}.{j}",
                            Description = $"Description menu {m}.{i}.{j}",
                            ImagePath = string.Empty,
                            Order = j,
                            Url = $"/category/{m}-{i}-{j}",
                        };
                        itemLevel2.Children.Add(itemLevel3);
                    }
                    itemLevel1.Children.Add(itemLevel2);
                }
                result.Add(itemLevel1);
            }

            return result;
        }

        public List<SlideShowViewModel> GetSlideShows()
        {
            var result = new List<SlideShowViewModel>();
            for (var i = 0; i < 3; i++)
            {
                result.Add(new SlideShowViewModel()
                {
                    Id = i,
                    ImagePath = "/Media/Images/TM-M2-HERO-D.png",
                    Title = $"SlideShow Item title - {i}",
                    SubTitle = $"SlideShow Item Sub Title - {i}",
                    Url = "#"
                });
            }

            result.Add(new SlideShowViewModel()
            {
                Id = 6,
                ImagePath = "/Media/Images/SUNMOUNTAIN-HERO-D.png",
                Title = $"Giày mới về",
                SubTitle = $"Rất nhiều mẫu đẹp dành cho các bạn",
                Url = "#"
            });

            return result;
        }

        public CategoryViewModel GetCategory(int id)
        {
            var random = new Random();
            var result = new CategoryViewModel()
            {
                Id = id,
                Icon = "fa fa-list",
                Name = $"Danh mục {id}",
                Description = "hehehehehheheh",
                Title = $"đây là title {id}",
                UrlPath = $"category/{id}",
                SubTitle = $"đây là sub title {id}",
                PageIndex = 0,
                PageSize = 10,
                ImagePath = $"/Media/Images/category.png",
                ShowProducts = true,
                TotalProduct = 100,
                ShowSubcategories = true,
                SubCategories = GetSubCategories(id)
            };
            result.SideBars = GetAllSideBars();
            result.Products = GetProductByCategory(result);
            return result;
        }

        private SidebarViewModel GetAllSideBars()
        {
            var data = new SidebarViewModel();
            //data.Brands = DataProvider.Entities.Brands.Select(x => new BrandViewModel
            //{
            //	Id = x.Id,
            //	Name = x.BrandName,
            //	Title = x.Description
            //}).ToList();
            //data.ShowBrand = true;
            //data.Colors = DataProvider.Entities.Colors.ToList();
            //data.ShowColor = true;
            //data.ShowSize = true;
            //data.Widths = DataProvider.Entities.Widths.ToList();
            //data.ShowSize = true;
            //data.Sizes = DataProvider.Entities.Sizes.ToList();
            return data;
        }

        private IList<ProductViewModel> GetProductByCategory(CategoryViewModel category)
        {
            var result = GetAllProducts();
            foreach (var p in result)
            {
                p.Category = category;
            }
            return result;
        }

        private IList<CategoryViewModel> GetSubCategories(int id)
        {
            var result = new List<CategoryViewModel>();
            for (var i = 0; i < 3; i++)
            {
                result.Add(new CategoryViewModel()
                {
                    Id = i,
                    Title = $"Sub category {i} of {id}",
                    Name = $"Sub category {i} of {id}",
                    Description = $"Description Sub category {i} of {id}",
                    SubTitle = $"Subtitle of sub category {i} of {id}",
                    UrlPath = $"category/{i}"
                });
            }
            return result;
        }

        public IList<ProductViewModel> GetAllProducts()
        {
            var random = new Random();
            var maxProduct = random.Next(20, 50);
            var result = new List<ProductViewModel>();
            for (var i = 0; i < maxProduct; i++)
            {
                result.Add(GetProduct(i));
            }
            return result;
        }

        public ProductViewModel GetProduct(int id)
        {
            var random = new Random();
            var longDesc = random.NextString(random.Next(500, 1000));
            var result = new ProductViewModel()
            {
                Id = id,
                CallForPrice = random.NextBool(),
                Gtin = random.NextString(10, false),
                SKU = random.NextString(10, false),
                Name = $"Product {id}",
                Title = $"Product Title {id}",
                LongDescription = longDesc,
                ShortDescription = longDesc.Substring(0, 150),
                ShowPrice = random.NextBool(),
                ShowRate = random.NextBool(),
                HideContentHeight = 200,
                HideContentIfLong = true,
                ShowDescription = true,
                ShowSaleOff = true,
                Images = GetProductImages(id),
                Price = GetProductPrice(id),
                ProductCode = $"PRODUCT-{id}",
                Seo = GetProductSeo(id),
                Reviews = GetProductReviews(id),
                Category = GetProductCategory(id),
                Attributes = GetProductAttributes(id),
                SpecialAttributes = GetProductSpecialAttributes(id),
                Brand = CreateBrand(id)
            };
            result.Url = $"/san-pham/{id}";
            result.FutureImagePath = result.Images[0].ImagePath;
            return result;
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
            return null;
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

        private IList<PictureViewModel> GetProductImages(int id)
        {
            var result = new List<PictureViewModel>();
            for (var i = 0; i < 3; i++)
            {
                result.Add(new PictureViewModel()
                {
                    Id = i,
                    ImagePath = $"/Media/Images/product-{i}.jpg",
                    ThumbnailPath = $"/Media/Images/product-thumb-{i}.jpg",
                    Title = $"Product {i}",
                    Width = 600,
                    Height = 400
                });
            }
            return result;
        }
    }
}