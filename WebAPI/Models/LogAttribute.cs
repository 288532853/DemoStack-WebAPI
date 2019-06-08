using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPI.Models
{
    public class LogAttribute:ActionFilterAttribute
    {
        public LogAttribute()
        {

        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Trace.WriteLine(string.Format("Action Method {0} Controller Name {1} executing at {2}", actionContext.ActionDescriptor.ActionName, actionContext.ControllerContext.ControllerDescriptor.ControllerName, DateTime.Now.ToString()), "Web API Logs");
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            Trace.WriteLine(string.Format("Action Method {0} Controller Name {1} executed at {2}", actionExecutedContext.ActionContext.ActionDescriptor.ActionName, actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName, DateTime.Now.ToString()), "Web API Logs");

            //Trace.WriteLine(string.Format("Action Method {0} executed at {1}", actionExecutedContext.ActionContext.ActionDescriptor.ActionName, DateTime.Now.ToString()), "Web API Logs");
        }
    }
}