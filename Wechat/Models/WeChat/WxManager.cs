using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataInterface.Models.Request;
namespace Wx.Models.WeChat
{
    public class WxManager
    {
        public static string appID = "wx4d41ff7c969b4c87";
        public static string appsecret = "a5bd931e40a26ec2ce8277031ecafdef";
        private static string templateUrl = "https://api.weixin.qq.com/cgi-bin/template/";
        private static string sm_token = "";
        private static string sm_Wx_back;
        private static DateTime TokenTime;
        private static Dictionary<string, string> getToken =null;
        private static Dictionary<string, string> GetToken {
            get
            {
                if (getToken == null)
                {
                    getToken = new Dictionary<string, string>();
                    getToken.Add("grant_type", "client_credential");
                    getToken.Add("appid", WxAnalysix.appID);
                    getToken.Add("secret",WxAnalysix.appsecret);
                }
                return getToken;
            }
        }
   
        public static string Token
        {
            get
            {
                if (TokenTime == null || TokenTime.AddSeconds(7200) < DateTime.Now)
                {
                   sm_token= NetManager.GetStaticUrl("https://api.weixin.qq.com/cgi-bin/token" ,GetToken);
                    sm_token = WxAnalysix.JsonDeserialize<Token>(sm_token).access_token;
                }
                return sm_token;
            }
        }
        /// <summary>
        /// 添加模板
        /// </summary>
        /// <returns></returns>
        public static string SetIndustry()
        {
            string form = WxAnalysix.JsonSerializer<api_set_industry>(new api_set_industry());
           return  NetManager.RequestWebAPI(templateUrl + WxRequest.api_set_industry.ToString(), form);
        }
        private static string sm_IndustryId= "si1yaahrUB8ydRMjiBnvDlR09fQvxMTRzO51SFk6Xqw";
        public static string IndustryId
        {
            get
            {
                if (sm_IndustryId == null || sm_IndustryId == "")
                {
                    sm_Wx_back= NetManager.RequestWebAPI(templateUrl + WxRequest.get_industry, WxAnalysix.JsonSerializer<templateId>(new templateId()));
                    sm_IndustryId = WxAnalysix.JsonDeserialize<GetTemplate>(sm_Wx_back).template_id;
                }
                return sm_IndustryId;
            }
        }
        public static string Get_template()
        {
            sm_Wx_back = NetManager.RequestWebAPI(templateUrl + WxRequest.get_all_private_template, WxAnalysix.JsonSerializer<templateId>(new templateId()));
            return sm_Wx_back;
        }
    }
}
public enum WxRequest
{
    api_set_industry,//设置行业
    get_industry,//获取行业
    api_add_template,//获取模板id
    get_all_private_template,//获取模板列表
    del_private_template,//删除私有模板
    send,//发送
}
