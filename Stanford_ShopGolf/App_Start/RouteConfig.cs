using System.Web.Mvc;
using System.Web.Routing;

namespace Stanford_ShopGolf
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                          name: "Category",
                          url: "category/{id}",
                          defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional }
                      );
            routes.MapRoute(
                name: "Product",
                url: "san-pham/{id}",
                defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "LienHe",
                url: "lien-he/{id}",
                defaults: new { controller = "LienHe", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ChinhSach",
                url: "chinh-sach-ban-hang/{id}",
                defaults: new { controller = "ChinhSachBanHang", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}