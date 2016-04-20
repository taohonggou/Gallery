using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;
using DataTrsfer;
using Tool;

namespace Server
{
    public class PhotoServer : BaseService<Photo>
    {
        public bool Add(PhotoDt photo)
        {
            Add(TransferObject.ConvertObjectByEntity<PhotoDt, Photo>(photo));
            return Save() > 0;
        }

        public List<PhotoDt> GetList(string userId)
        {
            return TransferObject.ConvertObjectByEntity<Photo,PhotoDt>(Select(o=>o.UserId==userId).OrderByDescending(o=>o.DateTime).ToList());
        }
    }
}
