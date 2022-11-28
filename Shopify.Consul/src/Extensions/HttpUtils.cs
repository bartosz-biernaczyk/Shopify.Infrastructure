using System;
using System.Collections.Generic;
using System.Text;

namespace Shopify.Consul.Extensions
{
    internal static class HttpUtils
    {
        private const string FirstParamSign = "?";
        private const string NextParamSign = "&";
        private const string EqualSign = "=";

        internal static Uri BuildUri(string url, params KeyValuePair<string, string>[] queryParameters)
        {
            var builder = new UriBuilder(url);

            if (! (queryParameters is null) && queryParameters.Length != 0)
            {
                builder.Query = BuildQueryPart(queryParameters);
            }

            return builder.Uri;
        }

        private static string BuildQueryPart(KeyValuePair<string, string>[] queryParameters)
        {
            var sb = new StringBuilder();
            var isFirst = true;

            for (int i = 0; i < queryParameters.Length; i++)
            {
                sb.Append(isFirst ? FirstParamSign : NextParamSign);
                isFirst = false;

                sb.Append(queryParameters[i].Key);
                sb.Append(EqualSign);
                sb.Append(queryParameters[i].Value);
            }

            return sb.ToString();
        }

    }
}
