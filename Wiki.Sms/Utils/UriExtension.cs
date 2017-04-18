using System;
using System.Web;

namespace Wiki.Sms.Utils
{
    public static class UriExtension
    {
        public static Uri AddQuery(this Uri uri, string name, string value)
        {
            var ub = new UriBuilder(uri);
            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);
            httpValueCollection.Add(name, value);
            ub.Query = httpValueCollection.ToString();
            return ub.Uri;
        }
    }
}
