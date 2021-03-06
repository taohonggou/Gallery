﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace Tool
{
public static     class UploadHelper
    {
    public static OutputModel UploadImg(HttpPostedFileBase img,out  string newPath, string savePath = "/Upload/Images/")
    {
        newPath = "";
        if (img == null || img.ContentLength == 0)
            return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
        string extension = Path.GetExtension(img.FileName);
        if (!FormatVerify.CheckImgExtension(extension))
            return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
        string newImgName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension; //Guid.NewGuid().ToString().Replace("-", "") + extension;
        string path = System.Web.HttpContext.Current.Server.MapPath(savePath);
        img.SaveAs(path + newImgName);
        newPath = savePath + newImgName;
        return OutputHelper.GetOutputResponse(ResultCode.OK);
    }
    }
}
