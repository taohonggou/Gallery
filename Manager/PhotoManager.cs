
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using DataTrsfer;
using Tool;


namespace Manager
{
    public class PhotoManager
    {
        private PhotoServer server = new PhotoServer();

        public OutputModel GetAllPhotoByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);

            List<PhotoDt> list= server.GetList(userId);
            if(list.Count==0)
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            return  OutputHelper.GetOutputResponse(ResultCode.OK,list);
        }

        public OutputModel GetDetails(string photoId)
        {
            int iPhotoId;
            if (!int.TryParse(photoId, out iPhotoId))
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            //获取图片地址
            PhotoDt photo = server.Get(iPhotoId);
            if(photo==null)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            //喜欢点赞数量
            photo.SuppostCount = new ScanOrSupportServer().GetCount(2,iPhotoId);
            //用户信息
            photo.User = new UserInfoServer().GetUserInfo(photo.UserId);
            //显示最近的6照图片
            photo.ListLastPhotos = server.GetList(photo.UserId, 6);
            //获取评论列表
            photo.ListComment=new CommentManager().GetListComment(iPhotoId);
            //是否收藏  是否赞过
            UserInfoDt user= System.Web.HttpContext.Current.Session["user"] as UserInfoDt;
            if(user!=null)
            {
                photo.IsCollection=new LikeServer().IsExist(user.UserId,iPhotoId);
                photo.IsSupport = new ScanOrSupportServer().IsExist(user.UserId, iPhotoId,2);
            }
            return OutputHelper.GetOutputResponse(ResultCode.OK, photo);
        }

        public OutputModel GetListByGallery(string userId,string galleryId)
        {
            int iGalleryId;

            if (!int.TryParse(galleryId,out iGalleryId))
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            List<PhotoDt> list = server.GetListByGallery(userId, iGalleryId);
            if(list.Count==0)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK,list);
        }
        public List<PhotoDt> GetPage(string pageindex, int pagesize, out int pagecount)
        {
            
        }
    }
}
