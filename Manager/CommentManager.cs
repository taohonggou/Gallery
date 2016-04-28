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
        
        public OutputModel Add(string content,string photoId,string userId,string upId,string rootId)
        {
            if(FormatVerify.IsNullOrEmpty(content,photoId,userId,upId))
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            int iPhotoId, iUpId,iRootId;
            if(!int.TryParse(photoId,out iPhotoId)||!int.TryParse(upId,out iUpId)||!int.TryParse(rootId,out iRootId))
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            //判断photoId是否存在
            if(!new PhotoServer().IsExist(iPhotoId))
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied);
            if(iUpId!=0&&!server.IsExist(iUpId))
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied);
            if (iRootId != 0 && !server.IsExist(iRootId))
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied);
            CommentDt comment = new CommentDt { 
            Content=content,
            DateTime=DateTime.Now,
            PhotoId=iPhotoId,
            UpId=iUpId,
            UserId=userId,
            RootId = iRootId,
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
        

        public OutputModel GetListByPhotoId(string  photoId)
        {
            int iPhotoId;
            if(!int.TryParse(photoId,out iPhotoId))
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);

            List<CommentDt> list = server.GetListByPhotoId(iPhotoId);
            if (list.Count<=0)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);

            return OutputHelper.GetOutputResponse(ResultCode.OK, InitComment(list));
        }

        public List<CommentDt> GetListComment(int photoId)
        {
            List<CommentDt> list = server.GetListByPhotoId(photoId);
            if (list.Count <= 0)
                return list;

            return InitComment(list);
        }

        private List<CommentDt>  InitComment(List<CommentDt> list)
        {
            List<CommentDt> result = new List<CommentDt>();
            result.AddRange(list.Where(o => o.UpId == 0));
            list.RemoveAll(o => o.UpId == 0);

            UserInfoServer userServer =ObjectContainer.GetInstance<UserInfoServer>();
            foreach (CommentDt comment in result)
            {
                //获取评论人信息
                comment.User = userServer.GetUserInfo(comment.UserId);
                //获取子评论
                comment.SonComment = list.Where(o => o.RootId == comment.CommentId).ToList();
                comment.SonComment.ForEach(o =>
                {
                    o.User = userServer.GetUserInfo(o.UserId);
                });
                list.RemoveAll(o => o.RootId == comment.CommentId);
            }
            return result;
        }
    }
}
