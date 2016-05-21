using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrsfer
{
    public class OriginalPhotoDt
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public string ImgUrl { get; set; }
        /// <summary>
        /// 1正常  0已删除
        /// </summary>
        public int Status { get; set; }
        public System.DateTime DataTime { get; set; }
    }
}
