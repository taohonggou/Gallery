using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTrsfer;
using EF;
using Tool;

namespace Server
{
    public class CommentServer : BaseService<Comment>
    {
        public bool Add(CommentDt comment)
        {
            base.Add(TransferObject.ConvertObjectByEntity<CommentDt, Comment>(comment));
            return base.Save() > 0;
        }
        public bool Delete(int id)
        {
            base.Delete(id);
            return base.Save() > 0;
        }
        public bool Delete(List<CommentDt> list)
        {
            base.Delete(TransferObject.ConvertObjectByEntity<CommentDt, Comment>(list));
            return base.Save() > 0;
        }
        public bool Update(CommentDt comment)
        {
            base.Update(TransferObject.ConvertObjectByEntity<CommentDt, Comment>(comment));
            return base.Save() > 0;
        }
        /// <summary>
        /// 获取子评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CommentDt> GetSon(int id)
        {
            return TransferObject.ConvertObjectByEntity<Comment, CommentDt>(base.Select(o => o.UpId == id).ToList());
        }
        public List<CommentDt> GetListByPhotoId(int photoId)
        {
            return TransferObject.ConvertObjectByEntity<Comment, CommentDt>(base.Select(o => o.PhotoId == photoId).ToList());
        }

        public bool IsExist(int commentId)
        {
            return Select(o => o.CommentId == commentId).Any();
        }
        public List<CommentDt> GetPage(int pageindex, int pagesize, out int rowcount)
        {
            List<Comment> list = SelectDesc(pageindex, pagesize, o => true, o => o.DateTime,out rowcount).ToList();
            return TransferObject.ConvertObjectByEntity<Comment, CommentDt>(list);
        }
    }
}
