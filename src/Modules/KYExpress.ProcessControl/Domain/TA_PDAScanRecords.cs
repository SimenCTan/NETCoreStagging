using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using KYExpress.Core.Domain.Entities;

namespace KYExpress.ProcessControl.Domain
{
    [Table("TA_PDAScanRecords")]
    public class TA_PDAScanRecords:IEntity
    {

        public TA_PDAScanRecords(string hONo, string hLoad, string upPerson, string scanType, string beforHLoad,DateTime dtTimeDiff)
        {
            HONo = hONo;
            HLoad = hLoad;
            UpPerson = upPerson;
            ScanType = scanType;
            BeforHLoad = beforHLoad;
            TimeDifference = dtTimeDiff;
            UpDate = DateTime.Now;
        }
        /// <summary>
        /// 交接编号
        /// 类型为varchar
        /// </summary>
        public string HONo { get; set; }

        /// <summary>
        /// 装车人
        /// </summary>
        public string LoadPerson { get; set; }

        /// <summary>
        /// 交接前位置
        /// </summary>
        public string HOPerson { get; set; }

        /// <summary>
        /// 交接后位置
        /// </summary>
        public string HLoad { get; set; }

        /// <summary>
        /// 取派类型
        /// </summary>
        public string HOType { get; set; }

        /// <summary>
        /// 上传人
        /// </summary>
        public string UpPerson { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UpDate { get; set; }

        /// <summary>
        /// 上传人点部
        /// </summary>
        public string UpPersonDpt { get; set; }

        /// <summary>
        /// 运单号
        /// 类型为varchar
        /// </summary>
        public string YDNo { get; set; }

        /// <summary>
        /// 单据编码
        /// 类型为varchar
        /// </summary>
        public string BillNo { get; set; }

        /// <summary>
        /// 是否第一次上传
        /// </summary>
        public Boolean IsFirst { get; set; }

        /// <summary>
        /// 巴枪编码
        /// </summary>
        public string PDANO { get; set; }

        /// <summary>
        /// 扫描类型
        /// </summary>
        public string ScanType { get; set; }

        /// <summary>
        /// 标志号
        /// </summary>
        [Column("flag")]
        public int Flag { get; set; }

        /// <summary>
        /// 是否有作别嫌疑
        /// </summary>
        public string IsCheat { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime TimeDifference { get; set; }

        /// <summary>
        /// 时差
        /// </summary>
        public string TimeDiff { get; set; }

        /// <summary>
        /// 交接前
        /// </summary>
        public string AfterHOPerson { get; set; }

        /// <summary>
        /// 交接后
        /// </summary>
        public string BeforHLoad { get; set; }

        /// <summary>
        /// 交接是否有效
        /// </summary>
        public Boolean IsEnabled { get; set; }

        /// <summary>
        /// 子件数
        /// </summary>
        public int SonBagCount { get; set; }

        /// <summary>
        /// 总件数
        /// </summary>
        public int AllBagCount { get; set; }

        /// <summary>
        /// 是否是母单号默认为0
        /// </summary>
        public int MDFlag { get; set; }

        /// <summary>
        /// 是否执行
        /// </summary>
        [Column("isExcute")]
        public Boolean IsExcute { get; set; }

        /// <summary>
        /// 是否运行
        /// </summary>
        [Column("isRun")]
        public Boolean IsRun { get; set; }
    }
}
