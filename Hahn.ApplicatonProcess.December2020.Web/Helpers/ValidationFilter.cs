using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Helpers
{
    public class ValidationFilter : IActionFilter
    {
        private readonly IEnumerable<string> _supportedCultures;
        public ValidationFilter(IConfiguration configuration)
        {
            _supportedCultures = configuration.GetSection("JsonLocalizationOptions:SupportedCultureInfos").Get<string[]>();
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var culture = context.HttpContext.Request.Headers["Accept-Language"].ToString();
            if (!string.IsNullOrWhiteSpace(culture) && _supportedCultures.Contains(culture))
            {
                var cultureInfo = new CultureInfo(culture);
                CultureInfo.CurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            }
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }        
    }
}
