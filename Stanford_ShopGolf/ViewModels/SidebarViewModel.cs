using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stanford.ShopGolf.Business.Entity;

namespace Stanford_ShopGolf.ViewModels
{
    public class SidebarViewModel
    {
        public bool ShowBrand { get; set; }
        public bool ShowColor { get; set; }
        public bool ShowSize { get; set; }
        public bool ShowWidth { get; set; }
        public bool ShowLoft { get; set; }
        public bool ShowWaist { get; set; }
        public bool ShowInseam { get; set; }
        public bool ShowBrouce { get; set; }
        public bool ShowShaft { get; set; }
        public bool ShowFlex { get; set; }
        public bool ShowLengthSize { get; set; }
        public bool ShowChuDe { get; set; }
        public IList<SideBarSubViewModel> Brands { get; set; }
        public IList<SideBarSubViewModel> Colors { get; set; }
        public IList<SideBarSubViewModel> Sizes { get; set; }
        public IList<SideBarSubViewModel> Widths { get; set; }
        public IList<SideBarSubViewModel> Loft { get; set; }
        public IList<SideBarSubViewModel> Waist { get; set; }
        public IList<SideBarSubViewModel> Inseam { get; set; }
        public IList<SideBarSubViewModel> Brouce { get; set; }
        public IList<SideBarSubViewModel> Shaft { get; set; }
        public IList<SideBarSubViewModel> Flex { get; set; }
        public IList<SideBarSubViewModel> LengthSize { get; set; }
        public IList<SideBarSubViewModel> ChuDes { get; set; }
    }

    public class SideBarSubViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string Url { get; set; }
    }
}