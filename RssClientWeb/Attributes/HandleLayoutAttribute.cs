using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RssClientWeb.Attributes
{
    public class HandleLayoutAttribute : ActionFilterAttribute
    {
        private readonly string _masterName;
        public HandleLayoutAttribute(string masterName = null)
        {
            if(!string.IsNullOrEmpty(masterName))
            {
                _masterName = masterName;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var result = filterContext.Result as ViewResult;
            if(result != null && string.IsNullOrEmpty(result.MasterName))
            {
                if(!string.IsNullOrEmpty(_masterName))
                {
                    result.MasterName = _masterName;
                }else{
                    result.MasterName = "_PublicLayout";
                }
            }
        }
    }
}