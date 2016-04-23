﻿using System;
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
            return TransferObject.ConvertObjectByEntity<Photo,PhotoDt>(list);
        }

        /// <summary>
        /// 获取某人最近的count张图片
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<LastPhotosDt> GetList(string userId,int count)
        {
            List<Photo> list= Select(o => o.UserId == userId).OrderByDescending(o => o.DateTime).Take(count).ToList();
            return TransferObject.ConvertObjectByEntity<Photo, LastPhotosDt>(list);
        }

        public PhotoDt Get(int photoId)
        {
            return TransferObject.ConvertObjectByEntity<Photo, PhotoDt>(Select( o => o.PhotoId == photoId).FirstOrDefault());
        }


    }
}
