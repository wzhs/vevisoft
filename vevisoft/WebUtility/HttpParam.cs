using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vevisoft.WebUtility
{
    public class HttpDefaultParam
    {
        // Fields
        public static string DefaultAcceptEncoding = "gzip,deflate,sdch";
        public static string DefaultAcceptLanguage = "zh-CN,zh;q=0.8";
        public static string DefaultAccpt = "*/*";
        public static string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 9.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
    }


    public class HttpParam : ICloneable
    {
        // Fields
        public int _reTryCount;

        // Methods
        public HttpParam(string url)
        {
            this._reTryCount = 3;
            this.Url = url;
            this.UserAgent = HttpDefaultParam.DefaultUserAgent;
            this.Accpt = HttpDefaultParam.DefaultAccpt;
            this.AccptEncoding = HttpDefaultParam.DefaultAcceptEncoding;
            this.AccptLanguage = HttpDefaultParam.DefaultAcceptLanguage;
            this.OtherParams = new Dictionary<string, string>();
        }

        public HttpParam(string url, string cookies)
            : this(url)
        {
            this.Cookies = cookies;
        }

        public object Clone()
        {
            return new HttpParam(this.Url, this.Cookies) { UserAgent = this.UserAgent, Accpt = this.Accpt, AccptEncoding = this.AccptEncoding, AccptLanguage = this.AccptLanguage };
        }

        // Properties
        public string Accpt { get; set; }

        public string AccptEncoding { get; set; }

        public string AccptLanguage { get; set; }

        public string Cookies { get; set; }

        public Dictionary<string, string> OtherParams { get; set; }

        public string Refer { get; set; }

        public string ContentType { get; set; }

        public int ReTryCount
        {
            get
            {
                return this._reTryCount;
            }
            set
            {
                this._reTryCount = value;
            }
        }

        public string Url { get; set; }

        public string UserAgent { get; set; }

        public string VisitMethoed { get; set; }
    }

   
}
