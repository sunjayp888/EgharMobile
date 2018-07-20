using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Helpers;
using Egharpay.Business.Interfaces;

namespace Egharpay.Business.Services
{
    public class SmsBusinessService : ISmsBusinessService
    {
        public bool SendSMS(string to, string message)
        {
            try
            {
                if (!ConfigHelper.SendSMS)
                    return false;
                var url = CreateSmsUrl(to, message);
                GetResponse(url);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GetResponse(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.MaximumAutomaticRedirections = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var receiveStream = response.GetResponseStream();
                var readStream = new StreamReader(receiveStream, Encoding.UTF8);
                var sResponse = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                return sResponse;
            }
            catch
            {
                return string.Empty;
            }
        }

        public string CreateSmsUrl(string to, string message)
        {
            var smsBaseUrl = ConfigurationManager.AppSettings["SMSBaseUrl"];
            var smsUserId = ConfigurationManager.AppSettings["SMSUserId"];
            var smsPassword = ConfigurationManager.AppSettings["SMSPassword"];
            var smsSenderId = ConfigurationManager.AppSettings["SMSSenderId"];
            string url = smsBaseUrl + smsUserId + "&password=" + smsPassword + "&msisdn=91" + to + "&sid=" + smsSenderId + "&msg=" + message + "&fl=0&gwid=2";
            return url;
        }
    }
}
