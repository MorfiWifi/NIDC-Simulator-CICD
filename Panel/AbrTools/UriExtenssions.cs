using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrBlazorTools
{
    public static class UriExtenssions
    {
        public static string GetQueryStringValue(string uri, string key)
        {
            try
            {
                var parts = uri.Split("?", options: StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                    return "";
                var qparts = parts[1].Split("&");
                foreach (var kv in qparts)
                {
                    var k = kv.Split("=")[0];
                    var v = kv.Split("=")[1];
                    if (k.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                        return v;
                }
                return "";
            }
            catch (Exception)
            {

                return "";
            }
        }

        public static string GetSubDomain(string stringUrl)
        {
            return Statics.DefaultOrganization;
            try
            {
                var url = new Uri(stringUrl);

                if (url.HostNameType == UriHostNameType.Dns)
                {

                    string host = url.Host;

                    var nodes = host.Split('.');
                    int startNode = 0;
                    if (nodes[0] == "www") startNode = 1;

                    return string.Format("{0}.{1}", nodes[startNode], nodes[startNode + 1]).ToLower().Replace(Statics.AppDomain, "").Replace(".", "");

                }
            }
            catch (Exception)
            {

                return Statics.DefaultOrganization; // "nidc";

            }

            return Statics.DefaultOrganization; //"nidc";
        }
    }

}
