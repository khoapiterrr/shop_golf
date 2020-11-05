using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Extensitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopGolf.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            ShowBrand = true;
            Top5Review = new List<Review>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string ProductCode { get; set; }

        public string SKU { get; set; }

        public string Gtin { get; set; }

        public bool ShowRate { get; set; }

        public bool ShowPrice { get; set; }

        public bool ShowSaleOff { get; set; }

        public bool ShowDescription { get; set; }

        public bool HideContentIfLong { get; set; }

        public int HideContentHeight { get; set; }

        public bool CallForPrice { get; set; }

        public bool ShowBrand { get; set; }

        public string FutureImagePath { get; set; }

        public string Url { get; set; }
        public int? ProductType { get; set; }

        public SeoViewModel Seo { get; set; }

        public ProductPriceViewModel Price { get; set; }

        public IList<PictureViewModel> Images { get; set; }

        public CategoryViewModel Category { get; set; }

        public ReviewViewModel Reviews { get; set; }

        public IList<AttributeViewModel> Attributes { get; set; }

        public IList<SpecialAttributeViewModel> SpecialAttributes { get; set; }
        public SanPham SanPhamEntity { get; set; }
        public BrandViewModel Brand { get; set; }
        public int? ProductInStock { get; set; }
        public List<Review> Top5Review { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class BrandViewModel
    {
        public BrandViewModel()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string ImagePath { get; set; }

        public bool ShowImage { get; set; }
    }

    public class AttributeViewModel
    {
        public AttributeViewModel()
        {
        }

        public string Name { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public ProductAttributeType ValueType { get; set; }
    }

    public class SpecialAttributeViewModel
    {
        public SpecialAttributeViewModel()
        {
        }

        public string Name { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public ProductSpecialAttributeType ValueType { get; set; }
    }

    public class SeoViewModel
    {
        public SeoViewModel()
        {
        }

        public int Id { get; set; }

        public string SeoName { get; set; }

        public string MetaKeywork { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }
    }

    public class ReviewViewModel
    {
        public ReviewViewModel()
        {
        }

        public double RateValue { get; set; }

        public int TotalReview { get; set; }

        public string LastReview { get; set; }

        public DateTime LastReviewOnTime { get; set; }

        public string LastReviewByUserName { get; set; }
        public double Percent5 { get; set; }
        public double Percent4 { get; set; }
        public double Percent3 { get; set; }
        public double Percent2 { get; set; }
        public double Percent1 { get; set; }
    }

    public class PictureViewModel
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public string Title { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string ThumbnailPath { get; set; }
    }

    public class ProductPriceViewModel
    {
        public ProductPriceViewModel()
        {
        }

        public decimal Price { get; set; }

        public decimal OldPrice { get; set; }

        public string CurrentcyFormat { get; set; }

        public string PriceDisplay
        {
            get
            {
                return string.Format(CurrentcyFormat, this.Price);
            }
        }

        public string OldPriceDisplay
        {
            get
            {
                return string.Format(CurrentcyFormat, this.OldPrice);
            }
        }

        public string SaleOffDisplay
        {
            get
            {
                return Price < OldPrice ? string.Empty : $"{Convert.ToInt32((OldPrice - Price) / OldPrice) * 100}%";
            }
        }

        public string Saved
        {
            get
            {
                return string.Format(CurrentcyFormat, OldPrice - Price);
            }
        }
    }
}