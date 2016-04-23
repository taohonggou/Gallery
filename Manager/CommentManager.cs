using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Tool;
using DataTrsfer;

namespace Manager
{
    public class CommentManager
    {
        private CommentServer server =new CommentServer();
        
        public OutputModel Add(string content,string photoId,string userId,string upId)
        {
            if(FormatVerify.IsNullOrEmpty(content,photoId,userId,upId))
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            int iPhotoId, iUpId;
            if(!int.TryParse(photoId,out iPhotoId)||!int.TryParse(upId,out iUpId))
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            //判断photoId是否存在
            if(!new PhotoServer().IsExist(iPhotoId))
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied);
            if(iUpId!=0&&!server.IsExist(iUpId))
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied);
            CommentDt comment = new CommentDt { 
            Content=content,
            DateTime=DateTime.Now,
            PhotoId=iPhotoId,
            UpId=iUpId,
            UserId=userId,
            };
            if (server.Add(comment))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
       
        public OutputModel Delete(int id)
        {
            bool b = false;
            List<CommentDt> list = server.GetSon(id);
            if (list.Count <= 0)
                b = true;
            else
            {
                foreach (CommentDt c in list)
                {
                    if (Delete(c.CommentId).StatusCode != 1)
                        return OutputHelper.GetOutputResponse(ResultCode.Error);
                }
            }
            if (server.Delete(id) && b)
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
        
        public OutputModel GetByPhotoId(int photoId)
        {
            List<CommentDt> list =server.GetByPhotoId(photoId);
            if (list.Count<=0)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK, list);
        }

        public List<CommentDt> GetListComment(int photoId)
        {
            List<CommentDt> list = server.GetByPhotoId(photoId);
            if (list.Count <= 0)
                return list;
            //获取评论人信息
            UserInfoServer userServer=new UserInfoServer();
            foreach (CommentDt comment in list)
            {
                comment.User = userServer.GetUserInfo(comment.UserId);
            }
            return list;
        }
    }
}
