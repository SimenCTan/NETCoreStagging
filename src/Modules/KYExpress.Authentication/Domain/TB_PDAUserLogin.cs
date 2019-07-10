using KYExpress.Core;
using KYExpress.Core.Domain.Entities;
using System;

namespace KYExpress.Authentication.Domain
{
    public class TB_PDAUserLogin : IEntity
    {
        protected TB_PDAUserLogin() { }

        /// <summary>
        /// 巴枪登录表主键
        /// </summary>
        public long UniqueID { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPWD { get; private set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 系统设置权限
        /// </summary>
        public Boolean PopdomSW { get; set; }

        /// <summary>
        /// 分拨权限
        /// </summary>
        public Boolean LoadingSW { get; set; }

        /// <summary>
        /// 包车扫描
        /// </summary>
        public Boolean BarnVehicleSW { get; set; }

        /// <summary>
        /// 数据库默认生成varchar
        /// </summary>
        public string sys_guid { get; private set; }

        /// <summary>
        /// 派前权限
        /// </summary>
        public Boolean QLogisticsYDSW { get; set; }

        /// <summary>
        /// 回单权限
        /// </summary>
        public Boolean QLogisticsHDSW { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string Creater { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime CreateDT { get; set; }

        /// <summary>
        /// 会话ID
        /// </summary>
        public string SessionID { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime SessionDT { get; set; }

        /// <summary>
        /// 运单解封
        /// </summary>
        public Boolean QLogisticsYDJS { get; set; }

        /// <summary>
        /// 员工部门
        /// </summary>
        public string UserWorkshop { get; set; }

        /// <summary>
        /// 私人手机
        /// </summary>
        public string UserTeleMobile { get; set; }

        /// <summary>
        /// 公司电话
        /// </summary>
        public string UserTelephone { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode { get; set; }

        /// <summary>
        /// 验证时间
        /// </summary>
        public DateTime VerificationCodeTime { get; set; }

        /// <summary>
        /// 个人件权限
        /// </summary>
        public Boolean PersonSW { get; set; }

        /// <summary>
        /// 是否所属公司
        /// </summary>
        public string IsCompany { get; set; }

        /// <summary>
        /// 冻结否
        /// </summary>
        public int IsFreeze { get; set; }

        /// <summary>
        /// 冻结备注
        /// </summary>
        public string FreezeRemarks { get; set; }

        /// <summary>
        /// 灰度测试
        /// </summary>
        public Boolean GrayTest { get; set; }


        /// <summary>
        /// 检查密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckPassword(string password)
        {
            return UserPWD == EncryptPassword(password);
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="newPassword"></param>
        /// <param name="oldPassword"></param>
        public void ChangPassword(string newPassword, string oldPassword)
        {
            if (string.IsNullOrEmpty(UserPWD))
            {
                if (CheckPassword(oldPassword))
                {
                    throw new CustomException("密码验证错误");
                }
            }
            UserPWD = EncryptPassword(newPassword);
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string EncryptPassword(string password)
        {
            return password;
        }

    }
}
