using KYExpress.Dapper.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace KYExpress.Dapper
{
    /// <summary>
    /// 配置Dapper映射
    /// </summary>
    public static class DapperConfiguration
    {
        public static void Initialize()
        {
            DapperExtensions.DapperExtensions.DefaultMapper = typeof(KYExpressPluralizedAutoClassMapper<>);
        }
    }
}
