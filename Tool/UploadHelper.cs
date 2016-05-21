using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tool
{
    public static class UploadHelper
    {
        private static  Random ran = new Random();

        public static OutputModel UploadImg(HttpPostedFileBase img, out  string newPath, string savePath = "/Upload/Images/")
        {
            newPath = "";
            if (img == null || img.ContentLength == 0)
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
           
            string newImgName;
           if(  !GenerateNewFileName(img.FileName,out newImgName ))
           {
               return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
           }
            string path = System.Web.HttpContext.Current.Server.MapPath(savePath);
            img.SaveAs(path + newImgName);
            newPath = savePath + newImgName;
            return OutputHelper.GetOutputResponse(ResultCode.OK);
        }

        /// <summary>
        /// 生成缩略图方法
        /// </summary>
        /// <param name="bigImg">原图</param>
        /// <param name="newPath">新路径</param>
        /// <param name="savePath">保存路径</param>
        /// <returns></returns>
        public static bool SaveThumbnail(HttpPostedFileBase bigImg, out string newPath, string savePath = "/Upload/Images/Thumbnail/")
        {
            newPath = "";
            int thumbWidth = 150;
            Stream stream = bigImg.InputStream;
            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
            int srcWidth = image.Width;
            int srcHeight = image.Height;
            double height = (srcHeight * 1.0 / srcWidth ) * thumbWidth;
            int thumbHeight = Convert.ToInt32(height);
            Bitmap bmp = new Bitmap(thumbWidth, thumbHeight);
            //从Bitmap创建一个System.Drawing.Graphics对象，用来绘制高质量的缩小图。
            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
            //设置 System.Drawing.Graphics对象的SmoothingMode属性为HighQuality

            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //下面这个也设成高质量

            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //下面这个设成High

            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //把原始图像绘制成上面所设置宽高的缩小图
            System.Drawing.Rectangle rectDestination = new Rectangle(0, 0, thumbWidth, thumbHeight);

            gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
            //保存图像，大功告成！

            string newImgName;
            if (!GenerateNewFileName(bigImg.FileName, out newImgName))
           {
               return false;
           }
            string path = System.Web.HttpContext.Current.Server.MapPath(savePath);
            bmp.Save(path+newImgName);
            newPath = savePath + newImgName;
            bmp.Save(path+newImgName);
            return true;
        }


        private static   bool GenerateNewFileName(string oldFileName,out string  newFileName)
        {
            newFileName="";
            string extension = Path.GetExtension(oldFileName);
            if (!FormatVerify.CheckImgExtension(extension))
                return false;
           
            newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") +ran.Next(1000,10000)+ extension;
            return true;
        }
    }
}
