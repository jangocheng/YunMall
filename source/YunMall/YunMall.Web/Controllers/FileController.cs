using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YunMall.Entity.json;

namespace YunMall.Web.Controllers
{
    public class FileController : BaseController
    {
        [HttpPost]
        public ActionResult Upload() {
            var savePath = "/content/images/";

            // 获取文件
            var files = Request.Files;
            if (files.Count == 0) return Json(new HttpResp(1, "请上传文件"));
            var file = files[0];
            if (file == null || file.ContentLength == 0) return Json(new HttpResp(1, "请上传正常文件"));

            // 生成文件名
            var nowTicks = DateTime.Now.Ticks;
            var random = new Random().Next() * 10;
            var fileName = nowTicks + random + Path.GetExtension(file.FileName);


            // 定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            extTable.Add("flash", "swf,flv");
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

            String dirPath = Server.MapPath(savePath);
            if (!Directory.Exists(dirPath))
            {
                return Json(new HttpResp(1, "上传目录不存在"));
            }


            String dirName = Request.QueryString["dir"];
            if (String.IsNullOrEmpty(dirName))
            {
                dirName = "image";
            }
            if (!extTable.ContainsKey(dirName))
            {
                return Json(new HttpResp(1, "目录名不正确"));
            }

            // 最大文件大小
            int maxSize = 1000000;

            String fileExt = Path.GetExtension(fileName).ToLower();

            if (file.InputStream == null || file.InputStream.Length > maxSize)
            {
                return Json(new HttpResp(1, "上传文件大小超过限制"));
            }

            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                return Json(new HttpResp(1, fileExt.Substring(1).ToLower() + "上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。")); 
            }

            // 创建文件夹
            dirPath += dirName + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            String filePath = dirPath + fileName;

            file.SaveAs(filePath);

            String fileUrl = savePath + fileName;

            return Json(new HttpResp(fileUrl));
        }
    }
}