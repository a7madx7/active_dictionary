using System;

namespace Active_Dictionary.Classes
{
    internal static class GoogleSearch
    {
        public static string Search(string search)
        {
            // Build Query
            var query = string.Empty;

            query += string.Format("q={0}", search);
            //query += string.Format("&key={0}", this.Key);
            //query += string.Format("&cx={0}", this.CX);
            //query += string.Format("&safe={0}", "off");
            //query += string.Format("&alt={0}", "json");
            //query += string.Format("&num={0}", "10");
            //query += string.Format("&start={0}", "1");

            // Construct URL
            var builder = new UriBuilder
            {
                Scheme = Uri.UriSchemeHttps,
                Host = "www.google.com",
                Path = "search",
                Query = query
            };

            return builder.ToString();
        }
    }
}