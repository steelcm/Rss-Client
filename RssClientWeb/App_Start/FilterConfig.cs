using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RssClientWeb.Attributes;

namespace RssClientWeb
{
    public class FilterConfig
    {
        public static void RegisterFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleLayoutAttribute());
        }
    }
}