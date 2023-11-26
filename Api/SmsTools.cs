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
                var request = (HttpWebRequest)WebRequest.Create($"https://api.kavenegar.com/v1/757064644A434943695970516233355336687757736D7955466650664F38414A7A37486E587441354D74453D" +
                                                                    $"/verify/lookup.json?receptor={mobileNumber}&token={token}" +
                                                                    $"&template=SyncGadgetsVerify");
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
