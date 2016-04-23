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
        
        public OutputModel Add(CommentDt comment)
        {
            if (comment == null)
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
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
