namespace  DF.Common {
    public class PathHelper {
        #region 物理路径和相对路径的转换
        //本地路径转换成URL相对路径  
        public static string ToVirtual(string url) {
            string tmpRootDir = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string imagesurl2 = url.Replace(tmpRootDir, ""); //转换成相对路径
            imagesurl2 = imagesurl2.Replace(@"\", @"/");
            //imagesurl2 = imagesurl2.Replace(@"Aspx_Uc/", @"");
            return imagesurl2;
        }
        //相对路径转换成服务器本地物理路径  
        public static string ToLocation(string url) {
            string tmpRootDir = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录 
            string imagesurl2 = tmpRootDir + url.Replace(@"/", @"\"); //转换成绝对路径 
            return imagesurl2;
        }
        #endregion
    }
}
