
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

            List<PhotoDt> list = server.GetList(userId);
            if (list.Count == 0)
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            return OutputHelper.GetOutputResponse(ResultCode.OK, list);
        }

        public OutputModel GetDetails(string photoId)
        {
            int iPhotoId;
            if (!int.TryParse(photoId, out iPhotoId))
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            //获取图片地址
            PhotoDt photo = server.Get(iPhotoId);
            if (photo == null)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            //喜欢点赞数量
            photo.SuppostCount = new ScanOrSupportServer().GetCount(2, iPhotoId);
            //用户信息
            photo.User = new UserInfoServer().GetUserInfo(photo.UserId);
            //显示最近的6照图片
            photo.ListLastPhotos = server.GetList(photo.UserId, 6);
            //获取评论列表
            photo.ListComment = new CommentManager().GetListComment(iPhotoId);
            //是否收藏  是否赞过
            UserInfoDt user = System.Web.HttpContext.Current.Session["user"] as UserInfoDt;
            if (user != null)
            {
                photo.IsCollection = new LikeServer().IsExist(user.UserId, iPhotoId);
                photo.IsSupport = new ScanOrSupportServer().IsExist(user.UserId, iPhotoId, 2);
            }
            return OutputHelper.GetOutputResponse(ResultCode.OK, photo);
        }

        public OutputModel GetListByGallery(string userId, string galleryId)
        {
            int iGalleryId;

            if (!int.TryParse(galleryId, out iGalleryId))
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            List<PhotoDt> list = server.GetListByGallery(userId, iGalleryId);
            if (list.Count == 0)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK, list);
        }

        public OutputModel GetPageOrderByDateTime(string pageIndex, string pageSize)
        {
            int index, size, rowCount, pageCount;
            FormatVerify.PageCheck(pageIndex, pageSize, out index, out size);
            List<PhotoDt> list = server.GetPageOrderByDateTime(index, size, out rowCount);
            if (list.Count == 0)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            pageCount = Convert.ToInt32(Math.Ceiling(rowCount * 1.0 / size));

            return OutputHelper.GetOutputResponse(ResultCode.OK, new { List = list, PageCount = pageCount });
        }
        public List<PhotoDt> GetPage(string pageindex, int pagesize, string name, string author, string category, out int pagecount)
        {
            int pageIndex;
            int rowcount;
            FormatVerify.PageCheck(pageindex, out pageIndex);
            List<PhotoDt> list = server.GetPage(pageIndex, pagesize, name, author, category, out rowcount);
            pagecount = (int)Math.Ceiling(rowcount * 1.0 / pagesize);
            foreach (PhotoDt item in list)
            {
                item.SuppostCount = new ScanOrSupportServer().GetCount(2, item.PhotoId);
                item.PhotoCategoryName = new PhotoCategoryServer().Get(item.PhotoCategoryId).Name;
                item.PhotoGalleryName = new PhotoGalleryServer().Get(item.PhotoGalleryId.Value).Name;
            }
            return list;
        }
        public OutputModel Delete(string id)
        {
            int i;
            if (!int.TryParse(id, out i))
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            if (server.Delete(i))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }

        public PhotoDt Get(int photoId)
        {
            return server.Get(photoId);
        }
<<<<<<< HEAD

        public OutputModel GetPageHottest(string pageIndex,string pageSize)
        {
            int index, size;
            FormatVerify.PageCheck(pageIndex, pageSize, out index, out size);
            List<PhotoDt> list = server.GetPageOrderByHottest(index, size);
            if (list.Count == 0)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK,list);
        }

        /// <summary>
        /// 获取没个分类下面的最火图片
        /// </summary>
        /// <param name="num">每个分类下面选取几张图片</param>
        /// <returns></returns>
        public OutputModel GetCategoryHottest(int num)
        {
            //获取所有分类
            List<PhotoCategoryDt> list = new PhotoCategoryServer().GetList();
            if (list.Count == 0)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);



            return new OutputModel();
        }
    }
}
=======
    }
}
>>>>>>> 47d249e6aa66a6fabd0a7be6931852ced70617ed
