using KYExpress.Core.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using KYExpress.ProcessControl.Events;

namespace KYExpress.Transfer.Domain
{
    [Table("TA_PDAHWLoadingScan")]
    public class TA_PDAHWLoadingScan : AggregateRoot, IEntity
    {
        protected TA_PDAHWLoadingScan()
        {

        }

        public TA_PDAHWLoadingScan(string ydNo, string chk_No, string returnDept,
            string uploadBy, DateTime uploadDT, string department, string deviceNo
            , string errorPrompt, string unloader, string pullover, string kndType, string isError
            , string afterHOPerson, string returnFB, string beforHLoad,string sys_guid)
        {
            YD_No = ydNo;
            CHK_No = chk_No;
            ReturnDpt = returnDept;
            UploadBy = uploadBy;
            UploadDT = uploadDT;
            Department = department;
            DeviceNo = deviceNo;
            ErrorPrompt = errorPrompt;
            Unloader = unloader;
            Pullover = pullover;
            KndType = kndType;
            isError = "0";
            AfterHOPerson = afterHOPerson;
            ReturnFB = returnFB;
            BeforHLoad = beforHLoad;
            Sys_guid = sys_guid;
            UploadDT = DateTime.Now;
            DomainEvents.Add(new CreateTransferLogEvent(YD_No, BeforHLoad, UploadBy, KndType, AfterHOPerson));
        }
        /// <summary>
        /// 运单号
        /// </summary>
        public string YD_No{set;get;}

        /// <summary>
        /// 母单号
        /// </summary>
        public string CHK_No{set;get;}

        /// <summary>
        /// 返回分拨
        /// </summary>
        public string ReturnDpt{set; get;}

        /// <summary>
        /// 上传人
        /// </summary>
        public string UploadBy{set;get;}

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime? UploadDT{set;get;}

        /// <summary>
        /// 上传点部
        /// </summary>
        public string Department{set;get; }

        /// <summary>
        /// 巴枪编码
        /// </summary>
        public string DeviceNo{set;get; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string ErrorPrompt{set;get;}

        [Column("sys_guid")]
        public string Sys_guid{set;get;}

        /// <summary>
        /// 卸货人
        /// </summary>
        public string Unloader{set;get;}

        /// <summary>
        /// 扫描类型
        /// </summary>
        public string KndType{set;get;}

        /// <summary>
        /// 是否有效
        /// </summary>
        public string IsError{ get; set; }

        /// <summary>
        /// 计费重量
        /// </summary>
        [Column("Col_006")]
        public string JfWeight { get; set; }

        /// <summary>
        /// 交接前
        /// </summary>
        public string AfterHOPerson { get; set; }

        /// <summary>
        /// 交接后
        /// </summary>
        public string BeforHLoad { get; set; }

        /// <summary>
        /// 拉货人
        /// </summary>
        public string Pullover { get; set; }

        /// <summary>
        /// 现返回分拨
        /// </summary>
        public string ReturnFB { get; set; }    
    }
}
