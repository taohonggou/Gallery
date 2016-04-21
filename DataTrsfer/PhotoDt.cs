using System;
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
        public int PhotoGalleryId { get; set; }
        public string UserId { get; set; }
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 照片状态:-1删除   0未分享  1已分享
        /// </summary>
        public short Status { get; set; }
        public System.DateTime DateTime { get; set; }
    }
}
