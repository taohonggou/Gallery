using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;
using Tool;
using DataTrsfer;

namespace Server
{
    public class LikeServer : BaseService<Like>
    {
        /// <summary>
        /// 根据照片id获取照片的喜欢数量
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns></returns>
        public int GetCount(int photoId)
        {
            return  Select(o => o.PhotoId == photoId).Count();
        }

        public bool Add(LikeDt like)
        {
            Add(TransferObject.ConvertObjectByEntity<LikeDt, Like>(like));
            return Save() > 0;
        }

        public bool IsExist(string userId,int photoId)
        {
            return Select(o => o.UserId == userId && o.PhotoId == photoId).Any();
        }
    }
}
