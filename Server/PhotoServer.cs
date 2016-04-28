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
            return TransferObject.ConvertObjectByEntity<Photo,PhotoDt>(list);
        }

        internal List<PhotoDt> GetList(string userId,IQueryable<Like> queryLike)
        {
            IQueryable<int> photoIds = queryLike.Select(o => o.PhotoId);
            return TransferObject.ConvertObjectByEntity<Photo,PhotoDt>( Select(o => photoIds.Contains(o.PhotoId)).ToList());
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

        public List<PhotoDt>  GetListByGallery(string userId,int galleryId)
        {
            List<Photo> list = Select(o => o.UserId == userId && o.PhotoGalleryId == galleryId).ToList();
            return TransferObject.ConvertObjectByEntity<Photo, PhotoDt>(list);
        }

        public PhotoDt Get(int photoId)
        {
            return TransferObject.ConvertObjectByEntity<Photo, PhotoDt>(Select( o => o.PhotoId == photoId).FirstOrDefault());
        }

        public bool IsExist(int photoId)
        {
            return Select(o => o.PhotoId == photoId).Any();
        }

        public List<PhotoDt> GetPageOrderByDateTime(int pageIndex,int pageSize,out int rowCount)
        {
            List<Photo> list = SelectDesc(pageIndex, pageSize, o => true, o => o.DateTime, out rowCount).ToList();
            return TransferObject.ConvertObjectByEntity<Photo, PhotoDt>(list);
        }

    }
}
