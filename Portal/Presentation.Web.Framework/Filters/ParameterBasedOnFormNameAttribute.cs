using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Web.Framework.Filters
{
    /// <summary>
    /// If form name exists, then specified "actionParameterName" will be set to "true"
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ParameterBasedOnFormNameAttribute : Attribute, IActionFilter
    {
        private readonly string _name;
        private readonly string _actionParameterName;

        public ParameterBasedOnFormNameAttribute(string name, string actionParameterName)
        {
            this._name = name;
            this._actionParameterName = actionParameterName;
        }



        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //we check "name" only. uncomment the code below if you want to check whether "value" attribute is specified

            //filterContext.ActionParameters[_actionParameterName] = !string.IsNullOrEmpty(formValue);
            //var dd = filterContext.HttpContext.Request.Form.Keys.Any(x => x.Equals(_name));
            filterContext.ActionArguments[_actionParameterName] =
                filterContext.HttpContext.Request.Form.Keys.Any(x => x.Equals(_name));


        }
    }
}
