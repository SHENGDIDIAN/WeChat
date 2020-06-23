using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Wx.Models.WeChat
{
    public class WxAnalysix
    {
        public static string appID = "wx4d41ff7c969b4c87";
        public static string appsecret = "a5bd931e40a26ec2ce8277031ecafdef";
        /// <summary>
        ///对象序列化为jsons
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            ms.Dispose();
            return jsonString;
        }
        /// <summary>
        /// json解析成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            ms.Dispose();
            ms.Close();
            return obj;
        }


    }
    public class GetTemplate
    {
        public int errcode;
        public string errmsg;
        public string template_id;
    }
    public class templateId
    {
        public string template_id_short = "TM0001";
    }
    public class Token
    {
        public string access_token;
        public string expires_in;
    }
    public class api_set_industry
    {
        public int industry_id1 = 22;
        public int industry_id2  = 1;
    }
    public class Pay
    {
        public string touser = "OPENID";
        public string template_id = "ngqIpbwh8bUfcSsECmogfXcV14J0tQlEpBO27izEYtY";
        public string url = "http://weixin.qq.com/download";
        public string topcolor = "#FF0000";
        public string sm_data;
        public string data(List<string> flag,List<string> context,List<string> color)
        {
            JObject jObject = new JObject();
            return "";
        }
       
    }
    public class data
    {
        public data()
        {
        }
    }
}
