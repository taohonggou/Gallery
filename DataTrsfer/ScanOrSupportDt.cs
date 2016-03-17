using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrsfer
{
    public  class ScanOrSupportDt
    {
        public int ScanOrSupportId { get; set; }
        public int PhotoId { get; set; }
        public string UserId { get; set; }
        public short Type { get; set; }
        public System.DateTime DateTime { get; set; }
    }
}
