using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataTrsfer
{
    public  class PhotoDt
    {
        public int PhotoId { get; set; }
        [Display(Name = "定位")]
        public int? LocationId { get; set; }
        [Display(Name = "分类")]
        public string PhotoCategoryName { get; set; }
        public int PhotoCategoryId { get; set; }
        [Display(Name = "相册")]
        public string PhotoGalleryName { get; set; }
        public int? PhotoGalleryId { get; set; }
        [Display(Name = "作者")]
        public string UserId { get; set; }
        [Display(Name = "图片")]
        public string ImgUrl { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "状态")]
        /// <summary>
        /// 照片状态:-1删除   0未分享  1已分享
        /// </summary>
        public short Status { get; set; }
        [Display(Name = "时间")]
        public System.DateTime DateTime { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfoDt User { get; set; }
        [Display(Name = "点赞量")]
        /// <summary>
        /// 点赞数量
        /// </summary>
        public int SuppostCount { get; set; }

        private List<CommentDt> listComment = new List<CommentDt>();

        /// <summary>
        /// 评论集合
        /// </summary>
        public List<CommentDt> ListComment { get { return listComment; } set {this.listComment=value ;} }

        private List<LastPhotosDt> listLastPhotos = new List<LastPhotosDt>();
        /// <summary>
        /// 最近的图片集合
        /// </summary>
        public List<LastPhotosDt> ListLastPhotos { get { return listLastPhotos; } set { this.listLastPhotos = value; } }

        /// <summary>
        /// 是否收藏
        /// </summary>
        public bool IsCollection { get; set; }

        /// <summary>
        /// 是否点赞
        /// </summary>
        public bool IsSupport { get; set; }
    }

    public class LastPhotosDt
    {
        public int PhotoId { get; set; }
        public string ImgUrl { get; set; }
    }
    
}
