using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrsfer
{
   public  class CommentDt
    {
        public int CommentId { get; set; }
        public string UserId { get; set; }
        public int PhotoId { get; set; }
        public int UpId { get; set; }
        public string Content { get; set; }
        public System.DateTime DateTime { get; set; }

       /// <summary>
       /// 评论人的信息
       /// </summary>
        public UserInfoDt User { get; set; }
    }
}
