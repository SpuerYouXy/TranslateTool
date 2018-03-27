using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConvertFrm
{
    public static class StringExtensions
    {
        public static string ToFirstUpper(this string str)
        {
            string[] arr = Regex.Split(str, "\\s+", RegexOptions.IgnoreCase);
            StringBuilder sb = new StringBuilder();
            foreach (var a in arr)
            {
                var charArr = a.ToCharArray();
                var len = charArr.Length;
                for (var i = 0; i < len; i++)
                {
                    if (i == 0)
                    {
                        sb.Append(charArr[i].ToString().ToUpper());
                    }
                    else
                    {
                        sb.Append(charArr[i].ToString().ToLower());
                    }
                }
                sb.Append(" ");
            }
            sb.Remove(sb.Length - 1,1);
            return sb.ToString();
        }
    }
}
