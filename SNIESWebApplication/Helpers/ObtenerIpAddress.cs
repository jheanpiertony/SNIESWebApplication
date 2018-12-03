namespace SNIESWebApplication.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;

    public class ObtenerIpAddress
    {
        public string IpAddress() {
            string ip = string.Empty;
            IPHostEntry ipEntry = Dns.GetHostEntry(GetCompCode());
            IPAddress[] addr = ipEntry.AddressList;
            ip = addr[1].ToString();
            return ip;
        }

        public string GetCompCode() {
            string strHostName = string.Empty;
            strHostName = Dns.GetHostName();
            return strHostName;
        }
    }
}