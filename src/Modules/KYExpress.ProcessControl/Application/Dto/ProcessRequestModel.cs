using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace KYExpress.ProcessControl.Application.Dto
{
    public class ProcessRequestModel
    {
        [Required(ErrorMessage ="运单号不能为空",AllowEmptyStrings =false)]
        [MinLength(5,ErrorMessage ="运单号长度不能小于5")]
        public string YDNo { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DTimeStart { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DTimeEnd { get; set; }
    }
}
