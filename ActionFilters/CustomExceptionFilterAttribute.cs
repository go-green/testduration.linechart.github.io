using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace TestDuration.LineChart.ActionFilters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionFilterAttribute> _logger;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CustomExceptionFilterAttribute(
            IModelMetadataProvider modelMetadataProvider, 
            ILogger<CustomExceptionFilterAttribute> logger,
            IHostingEnvironment env)
        {
            _logger = logger;
            _modelMetadataProvider = modelMetadataProvider;
            _hostingEnvironment = env;
        }

        public override void OnException(ExceptionContext context)
        {
            if (!_hostingEnvironment.IsDevelopment())
            {
                return;
            }
            _logger.LogError(context.Exception, context.Exception.Message);
            var result = new ViewResult { ViewName = "Error" };
            result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState)
            {
                { "Exception", context.Exception }
            };
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
