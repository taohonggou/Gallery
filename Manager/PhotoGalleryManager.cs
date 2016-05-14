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
        private PhotoGalleryServer server = new PhotoGalleryServer();
        public OutputModel Add(string name, string userId)
        {
            //if (photoGallery == null)
            //    return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            //if (Server.Get(photoGallery.Name, photoGallery.UserId) != null)
            //    return OutputHelper.GetOutputResponse(ResultCode.DataExisted);
            //if (Server.Add(photoGallery))
            //    return OutputHelper.GetOutputResponse(ResultCode.OK);
            if (FormatVerify.IsNullOrWhiteSpace(name, userId))
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            if (server.IsExist(name, userId))
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied, "已经有同名相册");
            PhotoGalleryDt gallery = new PhotoGalleryDt
            {
                DateTime = DateTime.Now,
                Name = name,
                UserId = userId
            };
            if (server.Add(gallery))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }

        public OutputModel Update(PhotoGalleryDt photoGallery)
        {
            if (photoGallery == null)
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            if (server.Update(photoGallery))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }

        public OutputModel Delete(int id)
        {
            if (server.Delete(id))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }

        public OutputModel Get(int id)
        {
            PhotoGalleryDt p = server.Get(id);
            if (p == null)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK, p);
        }


        /// <summary>
        /// 获取某用户的相册
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<PhotoGalleryDt> GetList(string userid)
        {
            return server.GetList(userid);
            //if (list == null)
            //    return OutputHelper.GetOutputResponse(ResultCode.NoData);
            //return OutputHelper.GetOutputResponse(ResultCode.OK, list);
        }
        /// <summary>
        /// 获取某用户的相册
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public OutputModel GetListOutputModel(string userid)
        {
            List<PhotoGalleryDt> list=server.GetList(userid);
            if (list.Count==0)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK, list);
        }
        public OutputModel GetList()
        {
            List<PhotoGalleryDt> list = server.GetList();
            if (list.Count == 0)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK, list);
        }
        public OutputModel UpdateFengmian(string photoid, string photogalleryid)
        {
            int pid;
            int pgid;
            if (!int.TryParse(photoid, out pid) || !int.TryParse(photogalleryid, out pgid))
            {
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            }
            PhotoDt p = new PhotoManager().Get(pid);
            if (p == null)
                return OutputHelper.GetOutputResponse(ResultCode.Error);
            PhotoGalleryDt pg = server.Get(pgid);
            if (pg == null)
                return OutputHelper.GetOutputResponse(ResultCode.Error);
            pg.CoverImg = p.ImgUrl;
            if (server.Update(pg))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
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
    }
}
