using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrsfer
{
    public class LikeDt
    {
        public int LikeId { get; set; }
        public Nullable<int> PhotoId { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
    }
}
