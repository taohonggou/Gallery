﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool;
using DataTrsfer;
using Server;
namespace Manager
{
    public class ScanOrSupportManager
    {
        private ScanOrSupportServer Server = ObjectContainer.GetInstance<ScanOrSupportServer>();
        
        public OutputModel AddSupport(string photoId,string userId)
        {
            int iPhotoId;
            if(!int.TryParse(photoId,out iPhotoId))
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            if(!new PhotoServer().IsExist(iPhotoId))
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied);


            if(Server.IsExist(userId,iPhotoId,2))
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied, "您已经赞过此照片");
            ScanOrSupportDt support = new ScanOrSupportDt { 
            DateTime=DateTime.Now,
            PhotoId = iPhotoId,
            Type=2,//2是点赞
            UserId=userId
            };
            if (Server.Add(support))
            {
                int count= Server.GetCount(2, iPhotoId);
                return OutputHelper.GetOutputResponse(ResultCode.OK, new { SupportCount=count});
            }
                
            return OutputHelper.GetOutputResponse(ResultCode.Error);

        }
        public OutputModel Update(ScanOrSupportDt scanorsupport)
        {
            if (scanorsupport == null)
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            if (Server.Update(scanorsupport))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
        public OutputModel Delete(int id)
        {
            if (Server.Delete(id))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
        public OutputModel Delete(ScanOrSupportDt scanorsupport)
        {
            if (Server.Delete(scanorsupport))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
        public OutputModel Get(int id)
        {
            ScanOrSupportDt s = Server.Get(id);
            if (s != null)
                return OutputHelper.GetOutputResponse(ResultCode.OK, s);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
        /// <summary>
        /// 获取某照片某用户点赞记录
        /// </summary>
        /// <param name="photoid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public OutputModel Get(int photoid, string userid)
        {
            ScanOrSupportDt s = Server.Get(photoid, userid);
            if (s != null)
                return OutputHelper.GetOutputResponse(ResultCode.OK, s);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
        /// <summary>
        /// 获取某用户的点赞记录
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public OutputModel GetList(string userid)
        {
            List<ScanOrSupportDt> list = Server.GetList(userid);
            if (list.Count > 0)
                return OutputHelper.GetOutputResponse(ResultCode.OK, list);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
        /// <summary>
        /// 获取某照片的点赞记录
        /// </summary>
        /// <param name="photoid"></param>
        /// <returns></returns>
        public OutputModel GetList(int photoid)
        {
            List<ScanOrSupportDt> list = Server.GetList(photoid);
            if (list.Count > 0)
                return OutputHelper.GetOutputResponse(ResultCode.OK, list);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
        public OutputModel GetList()
        {
            List<ScanOrSupportDt> list = Server.GetList();
            if (list.Count > 0)
                return OutputHelper.GetOutputResponse(ResultCode.OK, list);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
    }
}
