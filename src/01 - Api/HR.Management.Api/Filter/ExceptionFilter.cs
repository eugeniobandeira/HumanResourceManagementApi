using HR.Management.Domain.Exception;
using HR.Management.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HR.Management.Api.Filter
{
    /// <summary>
    /// Filter
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Exception treatment
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is HrManagementException)
            {
                HandleProjectException(context);
            } 
            else
            {
                ThrowUnknownError(context);
            }
        }

        private static void HandleProjectException(ExceptionContext context)
        {
            var hrManagementException = context.Exception as HrManagementException;
            var errorResponse = new ErrorResponse(hrManagementException!.GetErrors());

            context.HttpContext.Response.StatusCode = hrManagementException.StatusCode;
            context.Result = new ObjectResult(errorResponse);
        }

        private static void ThrowUnknownError(ExceptionContext context)
        {
            var errorResponse = new ErrorResponse(ErrorMessageResource.UNKNOWN_ERROR);

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
