using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wx.Models.WeChat;
namespace Wx.Controllers
{
    public class DataOfController : Controller
    {
        public static string show = "";
        public IActionResult Index()
        {
            return View();
        }
        public string back()
        {
            return WxManager.Get_template() ;
        }
        public static string GetMapPath(string strPath)
        {
            strPath = strPath.Replace("/", "\\");
            show += strPath;
            show += "\n";
            if (strPath.StartsWith("\\"))
            {
                //注意路径格式一定要全部转换  例子：F:\\web\\root\\update 或者 F:\\web\\root\\update\\test.img这种格式
                strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                show += (strPath + "\n");
            }
            show += (AppDomain.CurrentDomain.BaseDirectory + "\n");
            show += (Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath));
            return show;
        }

    }
}
