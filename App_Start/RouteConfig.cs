using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bigschool_TH_11
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "CBNVChucVuQuyenTruyCaps",
                url: "CBNVChucVuQuyenTruyCaps/{action}/{id}",
                defaults: new { controller = "CBNVChucVuQuyenTruyCaps", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ChucNang",
                url: "ChucNang/{action}/{id}",
                defaults: new { controller = "ChucNang", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ChucVu",
                url: "ChucVu/{action}/{id}",
                defaults: new { controller = "ChucVu", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "QuyenTruyCap",
                url: "QuyenTruyCap/{action}/{id}",
                defaults: new { controller = "QuyenTruyCap", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
