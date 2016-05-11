using System;
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
        public OutputModel UploadImg(HttpPostedFileBase img, string userId, string galleryId, string categoryId,string name)
        {
            string newPath;
            OutputModel model = UploadHelper.UploadImg(img, out newPath);
            if (model.StatusCode != 1)
            {
                return model;
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
                ImgUrl = newPath,
                Name =name?? img.FileName.Substring(0,img.FileName.LastIndexOf('.')),
                PhotoCategoryId = iCateGory,
                PhotoGalleryId = (iGallery == -1? (int?)null:iGallery),
                Status = 1,
                UserId = userId,
                LocationId = null
            };
            PhotoServer photoServer = new PhotoServer();
            if (photoServer.Add(photo))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            else
                return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
    }
}
