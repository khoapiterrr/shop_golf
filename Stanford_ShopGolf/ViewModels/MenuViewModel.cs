using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopGolf.ViewModels
{
    public class MenuViewModel
    {
        public MenuViewModel()
        {
            Children = new List<MenuViewModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string CssClass { get; set; }

        public int Order { get; set; }

        public string ImagePath { get; set; }

        public string HeadTitle { get; set; }

        public virtual List<MenuViewModel> Children { get; set; }

    }
}