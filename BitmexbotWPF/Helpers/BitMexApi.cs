﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;


namespace BitmexbotWPF.Helpers
{
    class BitMexApi
    {
            private const string domain = "https://testnet.bitmex.com";
            private string apiKey= "SnjlTnOtbFM3m58lcaQkUbyE";
            private string apiSecret= "a4arwa8YsHrWndeoBmJY-2rtDUk45GnQ4r_Sq82K7WRhtLa6";
            private int rateLimit=5000;

            //public BitMEXApi(string bitmexKey = "", string bitmexSecret = "", int rateLimit = 5000)
            //{
            //    this.apiKey = bitmexKey;
            //    this.apiSecret = bitmexSecret;
            //    this.rateLimit = rateLimit;
            //}

            public string BuildQueryData(Dictionary<string, string> param)
            {
                if (param == null)
                    return "";

                StringBuilder b = new StringBuilder();
                foreach (var item in param)
                    b.Append(string.Format("&{0}={1}", item.Key, WebUtility.UrlEncode(item.Value)));

                try { return b.ToString().Substring(1); }
                catch (Exception) { return ""; }
            }
            
            public string BuildJSON(Dictionary<string, string> param)
            {
                if (param == null)
                    return "";

                var entries = new List<string>();
                foreach (var item in param)
                    entries.Add(string.Format("\"{0}\":\"{1}\"", item.Key, item.Value));

                return "{" + string.Join(",", entries) + "}";
            }

            public static string ByteArrayToString(byte[] ba)
            {
                StringBuilder hex = new StringBuilder(ba.Length * 2);
                foreach (byte b in ba)
                    hex.AppendFormat("{0:x2}", b);
                return hex.ToString();
            }

            public long GetExpires()
            {
                return DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 3600; // set expires one hour in the future
            }

            public string Query(string method, string function, Dictionary<string, string> param = null, bool auth = false, bool json = false)
            {
                string paramData = json ? BuildJSON(param) : BuildQueryData(param);
                string url = "/api/v1" + function + ((method == "GET" && paramData != "") ? "?" + paramData : "");
                string postData = (method != "GET") ? paramData : "";

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(domain + url);
                webRequest.Method = method;

                if (auth)
                {
                    string expires = GetExpires().ToString();
                    string message = method + url + expires + postData;
                    byte[] signatureBytes = hmacsha256(Encoding.UTF8.GetBytes(apiSecret), Encoding.UTF8.GetBytes(message));
                    string signatureString = ByteArrayToString(signatureBytes);

                    webRequest.Headers.Add("api-expires", expires);
                    webRequest.Headers.Add("api-key", apiKey);
                    webRequest.Headers.Add("api-signature", signatureString);
                }

                try
                {
                    if (postData != "")
                    {
                        webRequest.ContentType = json ? "application/json" : "application/x-www-form-urlencoded";
                        var data = Encoding.UTF8.GetBytes(postData);
                        using (var stream = webRequest.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }
                    }

                    using (WebResponse webResponse = webRequest.GetResponse())
                    using (Stream str = webResponse.GetResponseStream())
                    using (StreamReader sr = new StreamReader(str))
                    {
                        return sr.ReadToEnd();
                    }
                }
                catch (WebException wex)
                {
                    using (HttpWebResponse response = (HttpWebResponse)wex.Response)
                    {
                        if (response == null)
                            throw;

                        using (Stream str = response.GetResponseStream())
                        {
                            using (StreamReader sr = new StreamReader(str))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }

            public byte[] hmacsha256(byte[] keyByte, byte[] messageBytes)
            {
                using (var hash = new HMACSHA256(keyByte))
                {
                    return hash.ComputeHash(messageBytes);
                }
            }

        #region RateLimiter

                private long lastTicks = 0;
                private object thisLock = new object();

                private void RateLimit()
                {
                    lock (thisLock)
                    {
                        long elapsedTicks = DateTime.Now.Ticks - lastTicks;
                        var timespan = new TimeSpan(elapsedTicks);
                        if (timespan.TotalMilliseconds < rateLimit)
                            Thread.Sleep(rateLimit - (int)timespan.TotalMilliseconds);
                        lastTicks = DateTime.Now.Ticks;
                    }
                }

        #endregion RateLimiter

    }


}

