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
    public class PhotoGalleryServer:BaseService<PhotoGallery>
    {
        public bool Add(PhotoGalleryDt photoGallery)
        {
            base.Add(TransferObject.ConvertObjectByEntity<PhotoGalleryDt, PhotoGallery>(photoGallery));
            return Save() > 0;
        }
        public bool Update(PhotoGalleryDt photoGallery)
        {
            base.Update(TransferObject.ConvertObjectByEntity<PhotoGalleryDt, PhotoGallery>(photoGallery));
            return Save() > 0;
        }
        public bool Delete(int id)
        {
            base.Delete(id);
            return Save() > 0;
        }
        public PhotoGalleryDt Get(int id)
        {
           return TransferObject.ConvertObjectByEntity<PhotoGallery,PhotoGalleryDt>(base.Select(id));
        }
        /// <summary>
        /// 获取某用户下名为name的相册,防止一个用户拥有相同名称的相册
        /// </summary>
        /// <param name="name"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public PhotoGalleryDt Get(string name,string userid)
        {
            return TransferObject.ConvertObjectByEntity<PhotoGallery, PhotoGalleryDt>(base.Select(o => o.Name == name&&o.UserId==userid).FirstOrDefault());
        }
        /// <summary>
        /// 获取某用户的相册
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<PhotoGalleryDt> GetList(string userid)
        {
            return TransferObject.ConvertObjectByEntity<PhotoGallery, PhotoGalleryDt>(base.Select(o => o.UserId == userid).ToList());
        }
        public List<PhotoGalleryDt> GetList()
        {
            return TransferObject.ConvertObjectByEntity<PhotoGallery, PhotoGalleryDt>(base.Select(o => true).ToList());
        }
    }
}
