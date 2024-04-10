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

        public static string RemoveFontTag(this string helper)
        {
            if (!string.IsNullOrWhiteSpace(helper))
            {
                helper = Regex.Replace(helper, @"<font[^>]*>", "");
            }

            return helper;
        }

        public static string RemoveStyle(this string helper)
        {
            if (!string.IsNullOrWhiteSpace(helper))
            {
                helper = Regex.Replace(helper, "\\s*style=[\"'][^\"']*[\"']", "");
            }

            return helper;
        }

        public static string RemoveHtmlTag(this string helper)
        {
            if (!string.IsNullOrWhiteSpace(helper))
            {
                helper = Regex.Replace(helper, @"<[^>]*>", string.Empty);
            }

            return helper;
        }

        public static string ToSlug(this string str)
        {
            return RemoveVietnameseSign(str).Trim().Replace(" ", "-").Replace("'", "").Replace(",", "").Replace("&", "").Trim();
        }

        public static string Brief(this string helper, int length)
        {
            if (helper.Length < length)
            {
                return helper.RemoveHtmlTag();
            }
            else
            {
                var lastDotIndex = helper.RemoveHtmlTag().LastIndexOf('.');
                if (lastDotIndex < length && lastDotIndex > 0)
                {
                    length = lastDotIndex;
                }
                return helper.RemoveHtmlTag().Substring(0, length);
            }
        }
    }
}
