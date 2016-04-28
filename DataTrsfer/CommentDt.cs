using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataTrsfer
{
    public class CommentDt
    {
        [Display(Name = "评论id")]
        public int CommentId { get; set; }
        [Display(Name = "用户id")]
        public string UserId { get; set; }
        [Display(Name = "照片id")]
        public int PhotoId { get; set; }
        [Display(Name = "照片")]
        public string PhotoImgUrl { get; set; }
        [Display(Name = "上一级id")]
        public int UpId { get; set; }
        [Display(Name = "显示id")]
        public int RootId { get; set; }
        [Display(Name = "评论")]
        public string Content { get; set; }
        [Display(Name = "时间")]
        public System.DateTime DateTime { get; set; }

        /// <summary>
        /// 评论人的信息
        /// </summary>
        public UserInfoDt User { get; set; }

        private List<CommentDt> sonComment = new List<CommentDt>();

        /// <summary>
        /// 二级评论
        /// </summary>
        public List<CommentDt> SonComment { get { return sonComment; } set { sonComment = value; } }
    }
}
