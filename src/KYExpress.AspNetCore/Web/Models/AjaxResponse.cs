using System;
using System.Collections.Generic;
using System.Text;

namespace KYExpress.AspNetCore.Web.Models
{
    public class AjaxResponse
    {
        public AjaxResponse(object data)
        {
            Code = 0;
            Data = data;
            Message = "成功";
        }
        public AjaxResponse(string message,int code=500)
        {
            Code = code;
            Message = message;
        }
        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public int RetStatus { get; set; }

        
    }
}
