using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace vevisoft.WebUtility
{
    /// <summary>
    /// this　class is the base of http get & post methods class.
    /// </summary>
    public class HttpUtility
    {

        private static readonly string DefaultUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:27.0) Gecko/20100101 Firefox/27.0";

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
        #region Common Methodes
        private static HttpWebResponse CreateGetResponse(HttpParam httpparam)
        {
            if (string.IsNullOrEmpty(httpparam.Url))
            {
                throw new ArgumentNullException("url");
            }
            var request = WebRequest.Create(httpparam.Url) as HttpWebRequest;
            if (request == null)
            {
                throw new ArgumentNullException("request Create Failed!");
            }

            request.Method = "GET";
            return (PrepareParams(request, httpparam).GetResponse() as HttpWebResponse);
        }
        private static HttpWebRequest PrepareParams(HttpWebRequest request, HttpParam hparam)
        {
            request.UserAgent = hparam.UserAgent;
            request.KeepAlive = true;
            request.Accept = hparam.Accpt;
            request.Headers.Add("Accept-Encoding:" + hparam.AccptEncoding);
            request.Headers.Add("Accept-Language: :" + hparam.AccptLanguage);
            if (hparam.OtherParams.Count > 0)
            {
                foreach (string str in hparam.OtherParams.Keys)
                {
                    if (str.ToLower().Equals("range"))
                    {
                        request.AddRange(0, 1);
                    }
                    else
                        request.Headers.Set(str, hparam.OtherParams[str]);
                }
            }

            request.Timeout = 10000;
            request.Referer = hparam.Refer;
            if (!string.IsNullOrEmpty(hparam.Cookies))
            {
                request.Headers.Set("Cookie", hparam.Cookies);
            }
            if (!string.IsNullOrEmpty(hparam.ContentType))
                request.ContentType = hparam.ContentType;
            return request;
        }

        public static string GetPostResponseTextFromResponse(HttpWebResponse response)
        {
            if (response == null)
                return "";
            if ((response.Headers.Get("Content-Disposition") != null) && response.Headers.Get("Content-Disposition").Contains("filename"))
            {
                return ("Response File" + response.Headers.Get("Content-Disposition"));
            }

            Encoding coding =
                //    Encoding.Default;
                //if (response.ContentType.ToLower().Contains("utf-8"))
                coding = Encoding.UTF8;
            //else 
            if (response.ContentType.ToLower().Contains("gb2312"))
                coding = Encoding.Default;
            if (response.ContentEncoding.ToLower() == "gzip")
            {
                using (Stream stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        using (var stream2 = new GZipStream(stream, CompressionMode.Decompress))
                        {
                            using (var reader = new StreamReader(stream2, coding))
                            {
                                return reader.ReadToEnd();
                            }
                        }
                    }

                }
            }

            using (Stream stream3 = response.GetResponseStream())
            {
                if (stream3 != null)
                {
                    using (var reader2 = new StreamReader(stream3, coding))
                    {
                        return reader2.ReadToEnd();
                    }
                }
            }
            return null;
        }
        #endregion


        public static string GetHtmlStringFromUrlByGet(HttpParam hparam)
        {
            int num = 0;
            HttpWebResponse response = null;
            try
            {
                response = CreateGetResponse(hparam);
            }
            catch
            {
                num++;
                if (num > hparam.ReTryCount)
                {
                    throw new Exception("can not open this url！please check web or url address.");
                }
            }
            return GetPostResponseTextFromResponse(response);
        }


    }

}
