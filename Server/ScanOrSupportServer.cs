
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool;
using DataTrsfer;
using EF;
namespace Server
{
    public class ScanOrSupportServer : BaseService<ScanOrSupport>
    {
        public bool Add(ScanOrSupportDt scanorsupport)
        {
            base.Add(TransferObject.ConvertObjectByEntity<ScanOrSupportDt, ScanOrSupport>(scanorsupport));
            return Save() > 0;
        }
        public bool Update(ScanOrSupportDt scanorsupport)
        {
            base.Update(TransferObject.ConvertObjectByEntity<ScanOrSupportDt, ScanOrSupport>(scanorsupport));
            return Save() > 0;
        }
        public bool Delete(int id)
        {
            base.Delete(id);
            return Save() > 0;
        }
        public bool Delete(ScanOrSupportDt scanorsupport)
        {
            base.Delete(TransferObject.ConvertObjectByEntity<ScanOrSupportDt, ScanOrSupport>(scanorsupport));
            return Save() > 0;
        }
        public ScanOrSupportDt Get(int id)
        {
            return TransferObject.ConvertObjectByEntity<ScanOrSupport, ScanOrSupportDt>(base.Select(id));
        }
        /// <summary>
        /// 获取某照片某用户点赞记录
        /// </summary>
        /// <param name="photoid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ScanOrSupportDt Get(int photoid, string userid)
        {
            return TransferObject.ConvertObjectByEntity<ScanOrSupport, ScanOrSupportDt>(base.Select(o => o.PhotoId == photoid && o.UserId == userid).FirstOrDefault());
        }
        /// <summary>
        /// 获取某用户的点赞记录
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<ScanOrSupportDt> GetList(string userid)
        {
            return TransferObject.ConvertObjectByEntity<ScanOrSupport, ScanOrSupportDt>(base.Select(o => o.UserId == userid).ToList());
        }
        /// <summary>
        /// 获取某照片的点赞记录
        /// </summary>
        /// <param name="photoid"></param>
        /// <returns></returns>
        public List<ScanOrSupportDt> GetList(int photoid)
        {
            return TransferObject.ConvertObjectByEntity<ScanOrSupport, ScanOrSupportDt>(base.Select(o => o.PhotoId == photoid).ToList());
        }
        public List<ScanOrSupportDt> GetList()
        {
            return TransferObject.ConvertObjectByEntity<ScanOrSupport, ScanOrSupportDt>(base.Select(o => true).ToList());
        }

        public bool IsExist(string userId,int photoId,int type)
        {
            return Select(o => o.UserId == userId && o.PhotoId == photoId&&o.Type==type).Any();
        }

        public int GetCount(int type,int photoId)
        {
            return Select(o => o.Type == type && o.PhotoId == photoId).Count();
        }

    }
}
