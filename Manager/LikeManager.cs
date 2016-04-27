using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using DataTrsfer;
using Tool;

namespace Manager
{
    public class LikeManager
    {
        private LikeServer server = new LikeServer();

        public OutputModel AddLike(string photoId, string userId)
        {
            if (FormatVerify.IsNullOrEmpty(photoId, userId))
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            int iPhotoId;
            if(!int.TryParse(photoId,out iPhotoId))
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied);
            if(!new PhotoServer().IsExist(iPhotoId))
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied);

            if(server.IsExist(userId,iPhotoId))
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied,"您已经收藏过此照片");

            LikeDt like = new LikeDt { 
            DateTime=DateTime.Now,
            PhotoId=iPhotoId,
            UserId=userId
            };
            if(server.Add(like))
            {
                //int count = server.GetCount(iPhotoId);
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            }
            else
                return OutputHelper.GetOutputResponse(ResultCode.Error);
        }

        /// <summary>
        /// 获取某人喜欢的图片
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public OutputModel GetLikePhotos(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);

            List<PhotoDt> listPhotos = server.GetList(userId);
            if (listPhotos.Count == 0)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK, listPhotos);
        }
    }
}
