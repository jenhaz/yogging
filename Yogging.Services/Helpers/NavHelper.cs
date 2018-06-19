using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Yogging.Services.Helpers
{
    public static class NavHelper
    {
        public static MvcHtmlString MenuLink(this HtmlHelper helper, string text, string action, string controller)
        {
            RouteValueDictionary routeData = helper.ViewContext.RouteData.Values;
            object currentController = routeData["controller"];
            object currentAction = routeData["action"];

            if (String.Equals(action, currentAction as string, StringComparison.OrdinalIgnoreCase)
                &&
               String.Equals(controller, currentController as string, StringComparison.OrdinalIgnoreCase))
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
