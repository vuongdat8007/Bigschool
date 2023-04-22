using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bigschool_TH_11.Helpers
{
    
    public static class HtmlExtensions
    {
        public static string IsActive(this HtmlHelper html, string controller, string action)
        {
            var routeData = html.ViewContext.RouteData;

            var routeController = (string)routeData.Values["controller"];
            var routeAction = (string)routeData.Values["action"];

            var isActive = string.Equals(controller, routeController, StringComparison.OrdinalIgnoreCase) &&
                            string.Equals(action, routeAction, StringComparison.OrdinalIgnoreCase);

            return isActive ? "active" : "";
        }
    }
    
}