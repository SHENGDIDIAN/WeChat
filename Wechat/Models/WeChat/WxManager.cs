using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using DataInterface.Models.Request;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2;

namespace Wx.Models.WeChat
{
    public class WxManager
    {
        public static string appID = "wx4d41ff7c969b4c87";
        public static string appsecret = "a5bd931e40a26ec2ce8277031ecafdef";
        private static string sm_openid = "";
        private static string templateUrl = "https://api.weixin.qq.com/cgi-bin/template/";
        private static string sm_token = "35_-tpFXz-cJab5_9gwyCMh0mcCiqcC_MTcZCPbwg0XxH0JYWFnADiFJEVwXHqLgKbsQ_H5-2cmlaEwltsp527gJ5Lct4_iBdbM8evoVDeLMjm1ZMuPldsR72XLmDDCiGozTBa17GWuGHQrbBrlCZTiADALIC";
        private static string sm_Wx_back;
        private static DateTime TokenTime;
        private static Dictionary<string, string> info = new Dictionary<string, string>();
        public static string Info  {
            get
            {
                if (sm_openid == "")
                {
                    info.Clear();
                    info.Add("access_token", Token);
                    info.Add("openid", "oJGyJvyADzhL1zl5Vrkutjp8-w9I");
                    info.Add("lang", "zh_CN");
                    sm_Wx_back = NetManager.GetStaticUrl("https://api.weixin.qq.com/cgi-bin/user/info", info);
                }
                return sm_Wx_back;
            }
        }
        public static string Send()
        {
            info.Clear();
            info.Add("access_token", Token);
            sm_Wx_back = "ces";
            sm_Wx_back = NetManager.RequestWebAPI(templateUrl, WxAnalysix.JsonSerializer<Pay>(new Pay()));
            return sm_Wx_back;
        }
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
                if (sm_token == ""||TokenTime.AddSeconds(7200) < DateTime.Now)
                {
                   sm_token= NetManager.GetStaticUrl("https://api.weixin.qq.com/cgi-bin/token" ,GetToken);
                    sm_token = WxAnalysix.JsonDeserialize<Token>(sm_token).access_token;
                    TokenTime = System.DateTime.Now;
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
