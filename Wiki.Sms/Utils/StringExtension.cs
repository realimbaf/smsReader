using System.Linq;

namespace Wiki.Sms.Utils
{
    public static class StringExtension
    {
        public static string CharCodeAt(this string str)
        {
           return str.Aggregate("", (current, t) => current + ((int) t).ToString());
        }
    }
}
