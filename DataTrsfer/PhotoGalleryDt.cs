using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrsfer
{
    public  class PhotoGalleryDt
    {
        public int PhotoGalleryId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string CoverImg { get; set; }
        public System.DateTime DateTime { get; set; }
    }
}
