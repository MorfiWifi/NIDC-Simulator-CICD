using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Api
{
    public static class SmsTools
    {
       
        public static bool SendVerifyMessage(string mobileNumber,string token)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create($"https://api.kavenegar.com/v1/7645504F466A37427A717154654F396C546778693330354668434B37644F3673" +
                                                                    $"/verify/lookup.json?receptor={mobileNumber}&token={token}" +
                                                                    $"&template=HafariVerify");
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                {
                    ;
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
