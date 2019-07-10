namespace KYExpress.ProcessControl
{
    internal static class SqlScriptData
    {
        public static string GetProcessInfoSql()
        {
            var sqlStr = @"SELECT HOPerson,HOType,UpPersonDpt,YDNo,BillNo,AfterHOPerson,BeforHLoad 
		                        FROM TA_PDAScanRecords WHERE YDNo=@YDNo AND [UpDate] BETWEEN @dtStart AND @dtEnd";
            return sqlStr;
        }
    }
}
