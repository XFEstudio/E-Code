using System;
using System.Net;

namespace TranslationAPI.Class
{
    #region 内部类
    /// <summary>
    /// 有超时的WebClient类
    /// </summary>
    public class WebClientPro : WebClient
    {
        /// <summary>
        /// 过期时间
        /// </summary>
        public int Timeout { get; set; }

        public WebClientPro(int timeout = 10000)
        {
            Timeout = timeout;
        }

        /// <summary>
        /// 重写GetWebRequest,添加WebRequest对象超时时间
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
            request.Timeout = Timeout;
            request.ReadWriteTimeout = Timeout;
            return request;
        }
    }
    public class TCTransmission
    {
        public string Title { get; private set; }
        public string Text { get; private set; }
        public TCTransmission(string title, string text)
        {
            Title = title;
            Text = text;
        }
    }
    #endregion
}
