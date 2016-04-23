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
    public class PhotoManager
    {
        private PhotoServer server = new PhotoServer();
        public List<PhotoDt> GetAllPhotoByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return new List<PhotoDt>();
           
            return server.GetList(userId);
        }

        public OutputModel GetDetails(string photoId)
        {
            int iPhotoId;
            if (!int.TryParse(photoId, out iPhotoId))
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            //获取图片地址
            PhotoDt photo = server.Get(iPhotoId);
            if(photo==null)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            //喜欢数量
            photo.LikeCount = new LikeServer().GetCount(iPhotoId);
            //用户信息
            photo.User = new UserInfoServer().GetUserInfo(photo.UserId);
            //显示最近的6照图片
            photo.ListLastPhotos = server.GetList(photo.UserId, 6);
            //获取评论列表
            photo.ListComment=new CommentManager().GetListComment(iPhotoId);
            return OutputHelper.GetOutputResponse(ResultCode.OK, photo);
        }
    }
}
