using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool;
using DataTrsfer;
using Server;

namespace Manager
{
    public class PhotoGalleryManager
    {
        private PhotoGalleryServer server = new  PhotoGalleryServer();
        public OutputModel Add(string  name,string userId)
        {
            //if (photoGallery == null)
            //    return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            //if (Server.Get(photoGallery.Name, photoGallery.UserId) != null)
            //    return OutputHelper.GetOutputResponse(ResultCode.DataExisted);
            //if (Server.Add(photoGallery))
            //    return OutputHelper.GetOutputResponse(ResultCode.OK);
            if (FormatVerify.IsNullOrWhiteSpace(name, userId))
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            if( server.IsExist(name, userId))
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied,"已经有同名相册");
            PhotoGalleryDt gallery = new PhotoGalleryDt {
            DateTime=DateTime.Now,
            Name=name,
            UserId=userId
            };
            if (server.Add(gallery))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
        public OutputModel Update(PhotoGalleryDt photoGallery)
        {
            if (photoGallery == null)
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            if(server.Update(photoGallery))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
        public OutputModel Delete(int id)
        {
            if(server.Delete(id))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }

        public OutputModel Get(int id)
        {
            PhotoGalleryDt p = server.Get(id);
            if(p==null)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK,p);
        }
        // /// <summary>
        ///// 获取某用户下名为name的相册,防止一个用户拥有相同名称的相册
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="userid"></param>
        ///// <returns></returns>
        //public OutputModel Get(string name,string userid)
        //{
        //    PhotoGalleryDt p = Server.Get(name, userid);
        //    if (p == null)
        //        return OutputHelper.GetOutputResponse(ResultCode.NoData);
        //    return OutputHelper.GetOutputResponse(ResultCode.OK,p);
        //}
        /// <summary>
        /// 获取某用户的相册
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<PhotoGalleryDt> GetList(string userid)
        {
            return  server.GetList(userid);
            //if (list == null)
            //    return OutputHelper.GetOutputResponse(ResultCode.NoData);
            //return OutputHelper.GetOutputResponse(ResultCode.OK, list);
        }
        public OutputModel GetList()
        {
            List<PhotoGalleryDt> list = server.GetList();
            if (list.Count == 0)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK, list);
        }
    }
}
