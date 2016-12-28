using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MyNamespace.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateHeaderAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //if (filterContext == null)
            //{
            //    throw new ArgumentNullException("filterContext");
            //}

            //var httpContext = filterContext.HttpContext;
            //var cookie = httpContext.Request.Cookies[AntiForgeryConfig.CookieName];
            //if (httpContext.Request.Cookies["__RequestVerificationToken"] != null)
            //    AntiForgery.Validate(cookie != null ? cookie.Value : null, httpContext.Request.Cookies["__RequestVerificationToken"].Value);
        }
    }
}