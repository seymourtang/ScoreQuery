using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Http
{
    public class HttpHelper
    {
        static CookieContainer cookies = null;
        static string strCookies = string.Empty;
        /// <summary>
        /// GET形式获取验证码和Cookie
        /// </summary>
        public BitmapImage GetValidateImage(string url)
        {
            cookies = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "*/*";
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0";
            request.CookieContainer = new CookieContainer(); //暂存到新实例
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var stream = response.GetResponseStream();
            cookies = request.CookieContainer; //保存cookies
            strCookies = request.CookieContainer.GetCookieHeader(request.RequestUri); //把cookies转换成字符串
            Bitmap bi = new Bitmap(stream);
            MemoryStream ms = new MemoryStream();
            bi.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage bImage = new BitmapImage();
            bImage.BeginInit();
            bImage.StreamSource = new MemoryStream(ms.ToArray());
            bImage.EndInit();
            ms.Dispose();
            bi.Dispose();
            cookies = request.CookieContainer; //保存cookies
            strCookies = request.CookieContainer.GetCookieHeader(request.RequestUri); //把cookies转换成字符串
            stream.Close();
            response.Close();
            return bImage;
        }

        /// <summary>
        /// 用于登录和相关POST
        /// </summary>
        /// <returns></returns>
        public string HttpPost(string url, string postData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "*/*;";
            request.UserAgent = "Mozilla/5.0";
            request.ContentType = "application/x-www-form-urlencoded";
            request.AllowAutoRedirect = true;
            request.CookieContainer = new CookieContainer();
            cookies = request.CookieContainer;
            request.KeepAlive = true;           
            byte[] postdatabyte = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = postdatabyte.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(postdatabyte, 0, postdatabyte.Length);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string strWebData = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                strWebData = reader.ReadToEnd();
            }
            return strWebData;
        }

        /// <summary>
        /// 用于GET数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "*/*;";
            request.UserAgent = "Mozilla/5.0";
            request.ContentType = "application/x-www-form-urlencoded";
            request.AllowAutoRedirect = true;
            request.CookieContainer = cookies;
            request.KeepAlive = true;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string strWebData = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                strWebData = reader.ReadToEnd();
            }
            return strWebData;
        }
    }
}
