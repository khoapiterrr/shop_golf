using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            ShowDescription = true;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string UrlPath { get; set; }

        public string ImagePath { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Icon { get; set; }

        public int TotalProduct { get; set; }

        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int TotalItem { get; set; }

        public int PageIndex { get; set; }

        public bool ShowSubcategories { get; set; }

        public bool ShowProducts { get; set; }

        public Boolean ShowDescription { get; set; }

        public virtual IList<CategoryViewModel> SubCategories { get; set; }

        public virtual IList<ProductViewModel> Products { get; set; }
        public virtual SidebarViewModel SideBars { get; set; }
    }
}