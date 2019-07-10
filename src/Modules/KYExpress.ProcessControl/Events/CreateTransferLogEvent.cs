using KYExpress.Core.EventBus;

namespace KYExpress.ProcessControl.Events
{
    public class CreateTransferLogEvent : INotification
    {
        public CreateTransferLogEvent(string hONo,string hLoad,string upPerson,string scanType,string beforHLoad)
        {
            HONo = hONo;
            HLoad = hLoad;
            UpPerson = upPerson;
            ScanType = scanType;
            BeforHLoad = beforHLoad;
        }
        /// <summary>
        /// 扫描单号
        /// </summary>
        public string HONo { get; set; }

        /// <summary>
        /// 交接后位置
        /// </summary>
        public string HLoad { get; set; }

        /// <summary>
        /// 上传人
        /// </summary>
        public string UpPerson { get; set; }

        /// <summary>
        /// 扫描类型
        /// </summary>
        public string ScanType { get; set; }

        /// <summary>
        /// 交接前位置
        /// </summary>
        public string BeforHLoad { get; set; }
    }
}
