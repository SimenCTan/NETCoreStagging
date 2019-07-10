using System;
using System.ComponentModel.DataAnnotations;

namespace KYExpress.Transfer.Application.Dto
{
    public class PDAHWLoadingScanModel
    {
        /// <summary>
        /// 运单号
        /// </summary>
        [Required(ErrorMessage ="运单号不能为空",AllowEmptyStrings =false)]
        [MinLength(5,ErrorMessage ="运单号最短为5位")]
        public String YD_No { get; set; }

        /// <summary>
        /// 校正单号
        /// </summary>
        [Required(ErrorMessage = "校正单号不能为空", AllowEmptyStrings = false)]
        [MinLength(5, ErrorMessage = "校正单号最短为5位")]
        public String CHK_No { get; set; }

        /// <summary>
        /// 返回分拨
        /// </summary>
        [Required(ErrorMessage ="返回分拨不能为空",AllowEmptyStrings =false)]
        public String ReturnDpt { get; set; }

        /// <summary>
        /// 上传人
        /// </summary>
        [Required(ErrorMessage ="上传人不能为空",AllowEmptyStrings =false)]
        public String UploadBy { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        [DataType(DataType.DateTime,ErrorMessage ="上传字段必须是时间格式")]
        public DateTime UploadDT { get; set; }

        /// <summary>
        ///上传人点部 
        /// </summary>
        [Required(ErrorMessage ="上传人点部不能为空",AllowEmptyStrings =false)]
        public String Department { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public String DeviceNo { get; set; }

        /// <summary>
        /// 员工职级
        /// </summary>
        public String EmployeeRank { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        public String ErrorPrompt { get; set; }

        /// <summary>
        /// 卸货人
        /// </summary>
        [Required(ErrorMessage ="卸货人不能为空",AllowEmptyStrings =false)]
        public String Unloader { get; set; }

        /// <summary>
        /// 扫描类型
        /// </summary>
        [Required(ErrorMessage ="请上传扫描类型",AllowEmptyStrings =false)]
        public String KndType { get; set; }

        /// <summary>
        /// 点部名称
        /// </summary>
        public String PointName { get; set; }

        /// <summary>
        /// 拉货人
        /// </summary>
        public String Pullover { get; set; }

        /// <summary>
        /// 目的中转场
        /// </summary>
        public String DR_262 { get; set; }

       
    }
}
