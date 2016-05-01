﻿using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;
using DataTrsfer;
using Tool;

namespace Server
{
    public class PhotoServer : BaseService<Photo>
    {
        public bool Add(PhotoDt photo)
        {
            Add(TransferObject.ConvertObjectByEntity<PhotoDt, Photo>(photo));
            return Save() > 0;
        }

        public List<PhotoDt> GetList(string userId)
        {
            List<Photo> list = Select(o => o.UserId == userId).OrderByDescending(o => o.DateTime).ToList();
            return TransferObject.ConvertObjectByEntity<Photo, PhotoDt>(list);
        }

        internal List<PhotoDt> GetList(string userId, IQueryable<Like> queryLike)
        {
            IQueryable<int> photoIds = queryLike.Select(o => o.PhotoId);
            return TransferObject.ConvertObjectByEntity<Photo, PhotoDt>(Select(o => photoIds.Contains(o.PhotoId)).ToList());
        }

        /// <summary>
        /// 获取某人最近的count张图片
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<LastPhotosDt> GetList(string userId, int count)
        {
            List<Photo> list = Select(o => o.UserId == userId).OrderByDescending(o => o.DateTime).Take(count).ToList();
            return TransferObject.ConvertObjectByEntity<Photo, LastPhotosDt>(list);
        }

        public List<PhotoDt> GetListByGallery(string userId, int galleryId)
        {
            List<Photo> list = Select(o => o.UserId == userId && o.PhotoGalleryId == galleryId).ToList();
            return TransferObject.ConvertObjectByEntity<Photo, PhotoDt>(list);
        }

        public PhotoDt Get(int photoId)
        {
            return TransferObject.ConvertObjectByEntity<Photo, PhotoDt>(Select(o => o.PhotoId == photoId).FirstOrDefault());
        }

        public bool IsExist(int photoId)
        {
            return Select(o => o.PhotoId == photoId).Any();
        }

        public List<PhotoDt> GetPageOrderByDateTime(int pageIndex, int pageSize, out int rowCount)
        {
            List<Photo> list = SelectDesc(pageIndex, pageSize, o => true, o => o.DateTime, out rowCount).ToList();
            return TransferObject.ConvertObjectByEntity<Photo, PhotoDt>(list);
        }
        public List<PhotoDt> GetPage(int pageindex, int pagesize, string name, string author, string category, out  int rowcount)
        {
            List<Photo> list = new List<Photo>();
            int c = 0;
            rowcount = 0;
            if (!string.IsNullOrWhiteSpace(category))
            {
                if (!int.TryParse(category, out c))
                    return TransferObject.ConvertObjectByEntity<Photo, PhotoDt>(list);
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                if (!string.IsNullOrWhiteSpace(author))
                {
                    if (!string.IsNullOrWhiteSpace(category))
                        list = SelectDesc(pageindex, pagesize, o => o.Name.Contains(name) && o.UserId.Contains(author) && o.PhotoCategoryId == c, o => o.DateTime, out rowcount).ToList();
                    else
                        list = SelectDesc(pageindex, pagesize, o => o.Name.Contains(name) && o.UserId.Contains(author), o => o.DateTime, out rowcount).ToList();
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(category))
                        list = SelectDesc(pageindex, pagesize, o => o.Name.Contains(name) && o.PhotoCategoryId == c, o => o.DateTime, out rowcount).ToList();
                    else
                        list = SelectDesc(pageindex, pagesize, o => o.Name.Contains(name), o => o.DateTime, out rowcount).ToList();
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(author))
                {
                    if (!string.IsNullOrWhiteSpace(category))
                    {
                        list = SelectDesc(pageindex, pagesize, o => o.UserId.Contains(author) && o.PhotoCategoryId == c, o => o.DateTime, out rowcount).ToList();
                    }
                    else
                    {

                        list = SelectDesc(pageindex, pagesize, o => o.UserId.Contains(author), o => o.DateTime, out rowcount).ToList();
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(category))
                        list = SelectDesc(pageindex, pagesize, o => o.PhotoCategoryId == c, o => o.DateTime, out rowcount).ToList();
                    else
                        list = SelectDesc(pageindex, pagesize, o => true, o => o.DateTime, out rowcount).ToList();
                }
            }
            return TransferObject.ConvertObjectByEntity<Photo, PhotoDt>(list);
        }
        public bool Delete(int id)
        {
            base.Delete(id);
            return Save() > 0;
        }

        public List<PhotoDt> GetPageOrderByHottest(int pageIndex, int pageSize)
        {
            string sql = "select * from (select * ,row_number() over (order by d.rank desc) AS 'ranking' from (select  * ,rank=((select count(*) from [like] where photoId=c.photoId)+(select Count(*) from scanorsupport where photoid=c.photoid)+(select count(*) from comment where photoid =c.photoid)) from photo as c  ) as d  ) as e  where e.ranking between ((@pageIndex-1)*@pageSize+1) and @pageIndex*@pageSize";
            SqlParameter[] param = { 
                                   new SqlParameter("@pageIndex",pageIndex),
                                   new SqlParameter("@pageSize",pageSize)
                                   };
            return  SqlQuery<PhotoDt>(sql, param);

        }
    }
}
