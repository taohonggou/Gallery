﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Tool;
using DataTrsfer;
using System.Web;
using System.IO;

namespace Manager
{
    public class UploadManager
    {
        public OutputModel UploadImg(HttpPostedFileBase img, string userId, string galleryId, string categoryId, string name)
        {
            string bigImgPath;
            OutputModel model = UploadHelper.UploadImg(img, out bigImgPath);
            if (model.StatusCode != 1)
            {
                return model;
            }
            //生成缩略图
            string ThumbnailImgPath;//缩略图的路径
            if (!UploadHelper.SaveThumbnail(img, out ThumbnailImgPath))
            {
                return OutputHelper.GetOutputResponse(ResultCode.Error);
            }
            //向数据库中插入
            int iGallery, iCateGory;
            if (!int.TryParse(galleryId, out iGallery) || !int.TryParse(categoryId, out iCateGory))
            {
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            }
            PhotoDt photo = new PhotoDt
            {
                DateTime = DateTime.Now,
                ImgUrl = ThumbnailImgPath,
                Name = name ?? img.FileName.Substring(0, img.FileName.LastIndexOf('.')),
                PhotoCategoryId = iCateGory,
                PhotoGalleryId = (iGallery == -1 ? (int?)null : iGallery),
                Status = 1,
                UserId = userId,
                LocationId = null
            };
            PhotoServer photoServer = new PhotoServer();
            if (photoServer.AddAndReturnPhotoId(photo))
            {
                    OriginalPhotoDt original = new OriginalPhotoDt
                    {
                        DataTime = DateTime.Now,
                        ImgUrl = bigImgPath,
                        Status = 1,
                        PhotoId = photo.PhotoId
                    };
                OriginalPhotoServer originServer=new OriginalPhotoServer();
                if(originServer.Add(original))
                    return OutputHelper.GetOutputResponse(ResultCode.OK);
            }
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }

        public OutputModel UploadHeadImg(HttpPostedFileBase img, string userId)
        {
            string newPath;
            OutputModel model = UploadHelper.UploadImg(img, out newPath);
            if (model.StatusCode != 1)
            {
                return model;
            }
            if (new UserInfoServer().Update(newPath, userId))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            else
                return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
    }
}
