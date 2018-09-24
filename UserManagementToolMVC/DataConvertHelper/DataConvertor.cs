using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserManagementToolMVC.DataConvertHelper
{
    public static class DataConvertor
    {
        public static string ConvertDate(string date)
        {
            return Convert.ToDateTime(date).ToString("yyyyMMdd");
        }

        public static string GenerateCustCode(string a, string b, string c)
        {
            return (a + b + c).ToLower();
        }
    }
}