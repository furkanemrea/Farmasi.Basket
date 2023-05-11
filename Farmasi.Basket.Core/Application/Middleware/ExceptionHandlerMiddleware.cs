using Farmasi.Basket.Common.Concrete;
using Farmasi.Basket.Core.Application.Concrete.ExceptionTypes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi.Basket.Core.Application.Middleware
{
    public class ExceptionHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BusinessException businessException)
            {
                var responseContent = BaseResponse<bool>.Builder().SetErrorCode().SetMessage(businessException.AdditionalMessage).Build();
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(responseContent.ToString());

                // to do : something
            }
            catch (ValidationException validationException)
            {
                var responseContent = BaseResponse<bool>.Builder().SetErrorCode().SetMessage(validationException.AdditionalMessage).Build();
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(responseContent.ToString());

                // to do : something
            }
            catch (Exception ex)
            {
                var responseContent = BaseResponse<bool>.Builder().SetErrorCode().SetMessage("Unexpected Error").Build();
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(responseContent.ToString());

                // warning !! do something
                // we must use log there.
            }
        }
    }
}
