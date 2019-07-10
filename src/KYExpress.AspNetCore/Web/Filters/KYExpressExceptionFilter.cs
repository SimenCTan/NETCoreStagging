using KYExpress.AspNetCore.Web.Models;
using KYExpress.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace KYExpress.AspNetCore.Web.Filters
{
    public class KYExpressExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;
        public KYExpressExceptionFilter(ILogger<KYExpressExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError("服务发生内部异常", context.Exception);

            HandleAndWrapException(context);
        }

        private void HandleAndWrapException(ExceptionContext context)
        {
            var result = CreateErrorInfoWithoutCode(context.Exception);
            context.Result = new ObjectResult(
                new AjaxResponse(result.errMessage, result.code)
            );


            context.Exception = null; //Handled!
        }

        private (int code,string errMessage) CreateErrorInfoWithoutCode(Exception exception)
        {
            int code = 500;
            string errMessage = "服务器发生内部错误";
            if(exception is CustomException)
            {
                var customException = exception as CustomException;
                return (customException.Code, customException.Message);
            }
            return (code,errMessage);
        }

    }
}
