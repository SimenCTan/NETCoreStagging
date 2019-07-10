using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KYExpress.Authentication.Application.Dtos.TokenAuth
{
    public class AuthenticateModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空", AllowEmptyStrings = false)]
        public string UserID { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Required(ErrorMessage = "用户密码不能为空", AllowEmptyStrings = false)]
        public string UserPWD { get; set; }

        /// <summary>
        /// 客户端当前版本号
        /// </summary>
        [Required(ErrorMessage = "请上传当前版本号", AllowEmptyStrings = false)]
        public string Version { get; set; }

        /// <summary>
        /// 巴枪编码
        /// </summary>
        [Required(ErrorMessage = "巴枪编码不能为空", AllowEmptyStrings = false)]
        public string DeviceID { get; set; }
    }
}
