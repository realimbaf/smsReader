using System;
using System.Text.RegularExpressions;

namespace Wiki.Sms.Utils
{
    public static class DecodeMessage
    {
        public static string Decode(string str)
        {
            const string pattern = "[A-Fa-f0-9]{1,4}";
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            var result = "";
            foreach (Match match in Regex.Matches(str,pattern))
            {
                if (match.Value != "0009" && match.Value != "0000")
                {
                    result += HexToChar(match.Value);
                }
                else
                {
                    result += "";
                }
            }
            return result;
        }

        private static string HexToChar(string hex)
        {
            var result = "";
            var n = Convert.ToInt32(hex, 16);
            if (n <= 0xFFFF)
            {
                result += Convert.ToChar(n);
            }
            else if (n <= 0x10FFFF)
            {
                n -= 0x10000;
                result += Convert.ToChar(0xD800 | (n >> 10)) + Convert.ToChar(0xDC00 | (n & 0x3FF));
            }
            return result;
        }
        private static string DecToHex(int number)
        {
            return number.ToString("X");
        }
        public static string EncodeMessage(string str)
        {
                var haut = 0;
                var result = "";
                if (string.IsNullOrEmpty(str))
                return result;
                for (var i = 0; i < str.Length; i++)
                {
                    var b = (int)str[i];
                    if (haut != 0)
                    {
                        if (0xDC00 <= b && b <= 0xDFFF)
                        {
                            result += DecToHex(0x10000 + ((haut - 0xD800) << 10) + (b - 0xDC00));
                            haut = 0;
                            continue;
                        }
                        haut = 0;
                    }
                    if (0xD800 <= b && b <= 0xDBFF)
                    {
                        haut = b;
                    }
                    else
                    {
                        var cp = DecToHex(b);
                        while (cp.Length < 4)
                        {
                            cp = '0' + cp;
                        }
                        result += cp;
                    }
                }
                return result;
            
        }
        public static DateTime ParseDate(string str)
        {
            var array = str.Split(',');
            return new DateTime(int.Parse("20" + array[0]), int.Parse(array[1]), int.Parse(array[2]), int.Parse(array[3]),
                int.Parse(array[4]), int.Parse(array[5]));
        }

        public static string GetCurrentTimeString()
        {
            var date = DateTime.Now;
            return string.Format("{0};{1};{2};{3};{4};{5};+3", date.Year.ToString().Substring(2), date.Month,
                date.Day, date.Hour, date.Minute, date.Second);
        }
    }

}
