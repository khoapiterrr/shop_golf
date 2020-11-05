using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopGolf.ViewModels
{
    public class CheckOutItemTotal
    {
        public CheckOutItemTotal()
        {
            Items = new List<CheckOutItem>();
        }

        public List<CheckOutItem> Items { get; set; }

        public int TotalItem
        {
            get
            {
                return Items.Sum(c => c.Quanlity);
                
            }
        }

        public decimal SubTotal
        {
            get
            {
                return Items.Sum(c=>c.Total);
            }
        }

    }

    public class CheckOutItem
    {
        public CheckOutItem()
        {
            Attributes = new List<SpecialAttributeViewModel>();
            
        }

        public int Id { get; set; }
		public Guid guid { get; set; }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public string ProductCode { get; set; }

        public string BrandName { get; set; }

        public string ProductThumbPath { get; set; }

        public string DeliveryOptions { get; set; }

        public int Quanlity { get; set; }

        public string Avability { get; set; }

        public decimal Price { get; set; }

        public List<SpecialAttributeViewModel> Attributes;

        public decimal Total {
            get
            {
                return Quanlity * Price;
            }
        }
    }
}