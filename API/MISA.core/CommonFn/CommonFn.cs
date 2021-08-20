using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Core.CommonFunction
{
    public class CommonFn
    {
        public static DateTime? ConvertToYMD(string dateString)
        {   
            var str = dateString.Replace('-', '/').Split("/");
            switch (str.Length)
            {
                case 1:
                    return DateTime.Parse($"{str[0]}/01/01");
                case 2:
                    return DateTime.Parse($"{str[1]}/{str[0]}/01");
                case 3:
                    return DateTime.Parse($"{str[2]}/{str[1]}/{str[0]}");
                default:
                    return null;
            }
        }

        public static string FormatPhoneNumber(string phoneNumer)
        {
            var str = phoneNumer.Replace('.', ' ');

            Regex trimmer = new Regex(@"\s\s+"); // Xóa khoảng trắng thừa trong chuỗi
            str = trimmer.Replace(str, "");

            return str;
        }
    }
}
