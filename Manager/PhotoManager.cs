
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
            OriginalPhotoServer originalServer = new OriginalPhotoServer();
            OriginalPhotoDt original = originalServer.GetByPhotoId(photo.PhotoId);
            if (original != null)
                photo.ImgUrl = original.ImgUrl;

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

        public List<PhotoDt> GetListByGallery(string userId, int galleryId)
        {
            return server.GetListByGallery(userId, galleryId);
        }

        public List<PhotoDt> GetListByGallery(int galleryId)
        {
            return server.GetListByGallery(galleryId);
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


        public OutputModel GetPageHottest(string pageIndex, string pageSize)
        {
            int index, size;
            FormatVerify.PageCheck(pageIndex, pageSize, out index, out size);
            List<PhotoDt> list = server.GetPageOrderByHottest(index, size);
            if (list.Count == 0)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK, list);
        }

        /// <summary>
        /// 获取每个分类下面的最火图片
        /// </summary>
        /// <param name="num">每个分类下面选取几张图片</param>
        /// <returns></returns>
        public OutputModel GetCategoryHottest(int num)
        {
            //获取所有分类
            List<PhotoCategoryDt> list = new PhotoCategoryServer().GetList();
            if (list.Count == 0)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            List<CategoryPhotoDt> listCate = new List<CategoryPhotoDt>();
            foreach (PhotoCategoryDt item in list)
            {
                CategoryPhotoDt cate = new CategoryPhotoDt();
                cate.CategoryName = item.Name;
                cate.CategoryId = item.PhotoCategoryId;
                cate.CategoryPhotos = server.GetListByCategoryHottest(num, item.PhotoCategoryId);
                listCate.Add(cate);
            }
            return OutputHelper.GetOutputResponse(ResultCode.OK, listCate);
        }

        /// <summary>
        /// 根据分类分页获取最火照片
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public OutputModel GetPageByCategoryId(string pageIndex, string pageSize, string categoryId)
        {
            int index, size, iCategoryId;
            FormatVerify.PageCheck(pageIndex, pageSize, out index, out size);

            if (!int.TryParse(categoryId, out iCategoryId))
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);

            List<PhotoDt> list = server.GetPageByCategoryOrderByHottest(index, size, iCategoryId);
            if (list.Count == 0)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK, list);
        }


        public List<PhotoDt> GetPageByUserIdOrderByDatetime(string userId, string pageIndex, string pageSize)
        {
            int index, size;
            FormatVerify.PageCheck(pageIndex, pageSize, out index, out size);
            return server.GetPageByUserIdOrderByDateTime(userId, index, size);
        }

        public OutputModel GetPageByUserId(string userId, string pageIndex, string pageSize)
        {
            int index, size;
            FormatVerify.PageCheck(pageIndex, pageSize, out index, out size);
            UserInfoDt user = new UserInfoServer().GetUserInfo(userId);
            if (user == null)
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied);
            List<PhotoDt> list = server.GetPageByUserIdOrderByDateTime(userId, index, size);
            if (list.Count == 0)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            else
                return OutputHelper.GetOutputResponse(ResultCode.OK, list);
        }

        public List<PhotoDt> GetPageByCollection(string userId, string pageIndex, string pageSize)
        {
            int index, size;
            FormatVerify.PageCheck(pageIndex, pageSize, out index, out size);
            return server.GetPageByUserCollection(index, size, userId);
        }

        public OutputModel GetPageByCollections(string userId, string pageIndex, string pageSize)
        {
            int index, size;
            FormatVerify.PageCheck(pageIndex, pageSize, out index, out size);
            List<PhotoDt> list = server.GetPageByUserCollection(index, size, userId);
            if (list.Count == 0)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK, list);
        }
        public OutputModel DeleteAll(string[] ids)
        {
            int[] id = new int[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                if (!int.TryParse(ids[i], out id[i]))
                    return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            }
            if (server.Delete(id))
            {
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            }
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }

        public OutputModel UpdateGallery(int galleryid, string[] ids)
        {
            int[] id = new int[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                if (!int.TryParse(ids[i], out id[i]))
                    return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            }
            List<PhotoDt> list = new List<PhotoDt>();
            foreach (int i in id)
            {
                PhotoDt p = server.Get(i);
                if (p != null)
                {
                    p.PhotoGalleryId = galleryid;
                    list.Add(p);
                }
                else
                {
                    return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
                }
            }
            if (server.Update(list))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }

        public OutputModel SearchByName(string name)
        {

            List<PhotoDt> listPhotos = SearchByNameReturnList(name);
            if (listPhotos.Count == 0)
            {
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            }
            else
                return OutputHelper.GetOutputResponse(ResultCode.OK, listPhotos);
        }

        public List<PhotoDt> SearchByNameReturnList(string name)
        {
            if (string.IsNullOrEmpty(name))
                return new List<PhotoDt>();
            StringBuilder sbLikeName = new StringBuilder();
            sbLikeName.Append("%");
            for (int i = 0; i < name.Length; i++)
            {
                sbLikeName.Append(name[i] + "%");
            }
            return server.GetListByLikeName(sbLikeName.ToString());
        }

        public OutputModel EditPhoto(string name, string photoId, string userId)
        {
            if (string.IsNullOrEmpty(name))
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            int iPhotoId;
            if (!int.TryParse(photoId, out iPhotoId))
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            PhotoDt photo = server.Get(iPhotoId);
            if (photo == null || photo.UserId != userId)
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied);
            photo.Name = name;
            if (server.Update(photo))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            else
                return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
    }
}


