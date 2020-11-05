using Stanford.ShopGolf.Business;
using Stanford.ShopGolf.Business.Entity;
using Stanford_ShopGolf.Services;
using Stanford_ShopGolf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopGolf.Controllers
{
    public class MenuController : Controller
    {
        private readonly DemoService demoService;
        public MenuController()
        {
            demoService = new DemoService();
        }

        // GET: Menu
        public ActionResult MainMenu()
        {
            var model = GetMenuCategories();// demoService.GetMenuCategories();


            return PartialView(model);
        }

        /// <summary>
        /// Hàm lấy danh sách menu con theo cha và có thể sắp xếp
        /// Author                  Date                    Comments
        /// DangBQ              01/10/2019          Tạo mới
        /// </summary>
        /// <param name="parentId">Mã cha</param>
        /// <param name="order">Kiểu sắp xếp</param>
        /// <returns></returns>
        public List<MenuViewModel> GetChidrenByParentId(int parentId, string order = "")
        {
            var lstMenu = from m in DataProvider.Entities.Menus.Where(m => m.ParentId == parentId)
                          select new MenuViewModel
                          {
                              Id = m.Id,
                              Name = m.TenItem,
                              HeadTitle = m.HeadTitle,
                              Description = m.MoTa,
                              ImagePath = m.ImagePath,
                              Order = m.SapXep.HasValue ? m.SapXep.Value : 0,
                              //Nếu menu có chủ đề id tương ứng thì ưu tiên lấy đó trước
                              Url = (m.ChuDeId!=null && m.ChuDeId.HasValue ? "/category/" + m.ChuDeId.Value : m.Link)
                          };

           switch(order)
            {
                //Tăng dần
                case "asc": lstMenu = lstMenu.OrderBy(p => p.Order);
                    break;
                //Giảm dần
                case "desc": lstMenu = lstMenu.OrderByDescending(p => p.Order);
                    break;
            }
            
            return lstMenu.ToList();
        }

        public List<MenuViewModel> GetMenuCategories()
        {
            var result = new List<MenuViewModel>();
          
            //Lấy menu cấp 1
            var lstMenu1 = GetChidrenByParentId(0, "asc");

            //Duyệt các menu cấp 1 để lấy con của nó
            foreach(MenuViewModel m1 in lstMenu1)
            {
                MenuViewModel menu1 = new MenuViewModel();
                menu1.Id = m1.Id;
                menu1.Name = m1.Name;
                menu1.HeadTitle = m1.HeadTitle;
                menu1.ImagePath = m1.ImagePath;
                menu1.Description = m1.Description;
                menu1.Url = m1.Url;
                var itemMenu2 = new List<MenuViewModel>();
                //Lấy các menu con của m2
                var lstMenu2 = GetChidrenByParentId(m1.Id, "asc");

                foreach (MenuViewModel m2 in lstMenu2)
                {
                    MenuViewModel menu2 = new MenuViewModel();
                    menu2.Id = m2.Id;
                    menu2.Name = m2.Name;
                    menu2.HeadTitle = m2.HeadTitle;
                    menu2.ImagePath = m2.ImagePath;
                    menu2.Description = m2.Description;
                    menu2.Url = m2.Url;

                    var lstMenu3 = GetChidrenByParentId(m2.Id, "asc");

                    menu2.Children.AddRange(lstMenu3);
                    itemMenu2.Add(menu2);
                }

                menu1.Children.AddRange(itemMenu2);
                result.Add(menu1);
            }

            return result;
        }

        public ActionResult FooterMenu(string menuName)
        {
            var model = demoService.GetFooterMenu(menuName);
            return PartialView(model);
        }
    }
}