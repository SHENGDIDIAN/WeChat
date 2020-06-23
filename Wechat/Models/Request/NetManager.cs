
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wx.Models.WeChat;

namespace DataInterface.Models.Request
{
    public class NetManager
    {
        static string backSting;
        /// <summary>
        /// 带参数的url Get获取
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string GetStaticUrl(string url,Dictionary<string,string> parameter)
        {
            url += "?";
            foreach (var item in parameter)
            {
                url += (item.Key + "=" + item.Value+"&");
            }
            url.Remove(url.Length-1, 1);
            backSting= GetUrl(url);
            return backSting;
        }
        public static string GetUrl(string url)
        {
            string backMsg = "";
            try
            {
                System.Net.WebRequest httpRquest = System.Net.HttpWebRequest.Create(url);
                httpRquest.Method = "GET";
                //这行代码很关键，不设置ContentType将导致后台参数获取不到值
                httpRquest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                //httpRquest.ContentLength = dataArray.Length;
                System.Net.WebResponse response = httpRquest.GetResponse();
                System.IO.Stream responseStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, System.Text.Encoding.UTF8);
                backMsg = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();

                responseStream.Close();
                responseStream.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return backMsg;
        }
        /// 发送请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="sendData">参数格式 “name=王武&pass=123456”</param>
        /// <returns></returns>
        public static string RequestWebAPI(string url, string sendData)
        {
            url += ("?access_token=" +WxManager.Token);
            string backMsg = "";
            try
            {
                System.Net.WebRequest httpRquest = System.Net.HttpWebRequest.Create(url);
                httpRquest.Method = "POST";
                //这行代码很关键，不设置ContentType将导致后台参数获取不到值
                httpRquest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                byte[] dataArray = System.Text.Encoding.UTF8.GetBytes(sendData);
                //httpRquest.ContentLength = dataArray.Length;
                System.IO.Stream requestStream = null;
                if (string.IsNullOrWhiteSpace(sendData) == false)
                {
                    requestStream = httpRquest.GetRequestStream();
                    requestStream.Write(dataArray,0, dataArray.Length);
                    requestStream.Close();
                }
                System.Net.WebResponse response = httpRquest.GetResponse();
                System.IO.Stream responseStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, System.Text.Encoding.UTF8);
                backMsg = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();

                requestStream.Dispose();
                responseStream.Close();
                responseStream.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return backMsg;
        }
    }
}
