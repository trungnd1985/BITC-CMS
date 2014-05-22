using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System
{
    public static class StringExtension
    {
        public static string RemoveVietnameseSign(this string helper)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = helper.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string ToSlug(this string str)
        {
            return RemoveVietnameseSign(str).Trim().Replace(" ", "-").Replace("'", "").Replace(",", "").Replace("&", "").Trim();
        }
    }
}
