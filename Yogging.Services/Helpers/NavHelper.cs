using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Yogging.Services.Helpers
{
    public static class NavHelper
    {
        public static MvcHtmlString MenuLink(this HtmlHelper helper, string text, string action, string controller)
        {
            var routeData = helper.ViewContext.RouteData.Values;
            var currentController = routeData["controller"];
            var currentAction = routeData["action"];

            if (string.Equals(action, currentAction as string, StringComparison.OrdinalIgnoreCase)
                &&
               string.Equals(controller, currentController as string, StringComparison.OrdinalIgnoreCase))
            {
                return helper.ActionLink(
                    text, action, controller, null,
                    new { @class = "current-page" }
                    );
            }

            return helper.ActionLink(text, action, controller);
        }
    }
}