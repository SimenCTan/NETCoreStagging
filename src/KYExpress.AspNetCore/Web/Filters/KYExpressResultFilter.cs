using KYExpress.AspNetCore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text;

namespace KYExpress.AspNetCore.Web.Filters
{
    public class KYExpressResultFilter : IResultFilter
    {

        public KYExpressResultFilter()
        {
        }

        public virtual void OnResultExecuting(ResultExecutingContext context)
        {
            StringBuilder errMessage = new StringBuilder();
            foreach (var state in context.ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    errMessage.AppendLine(error.ErrorMessage);
                }
            }
            if (errMessage.Length > 0)
            {
                context.Result = new ObjectResult(new AjaxResponse(errMessage.ToString(), (int)HttpStatusCode.BadRequest));
                return;
            }
            if (context.Result is ObjectResult)
            {
                var result = context.Result as ObjectResult;
                result.Value = new AjaxResponse(result.Value);
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new ObjectResult(new AjaxResponse(default(object)));
            }
        }

        public virtual void OnResultExecuted(ResultExecutedContext context)
        {
            //no action
        }
    }
}
