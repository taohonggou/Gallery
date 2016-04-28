
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrsfer
{
    public  class PhotoDt
    {
        public int PhotoId { get; set; }
        public int? LocationId { get; set; }
        public int PhotoCategoryId { get; set; }
        public int? PhotoGalleryId { get; set; }
        public string UserId { get; set; }
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 照片状态:-1删除   0未分享  1已分享
        /// </summary>
        public short Status { get; set; }
        public System.DateTime DateTime { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfoDt User { get; set; }

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
