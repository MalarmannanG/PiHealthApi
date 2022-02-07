using Microsoft.AspNetCore.Mvc.Filters;

namespace PiHealth.Web.Filter
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {       
        public CustomExceptionFilterAttribute()
        {
       
        }

        public override void OnException(ExceptionContext context)
        {            
            context.Result = null;
        }
    }
}
