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
    public class OriginalPhotoServer : BaseService<OriginalPhoto>
    {
        public bool Add(OriginalPhotoDt originalPhoto)
        {
           Add(TransferObject.ConvertObjectByEntity<OriginalPhotoDt, OriginalPhoto>(originalPhoto));
           return Save() > 0;
        }

        public OriginalPhotoDt GetByPhotoId(int photoId)
        {
            return TransferObject.ConvertObjectByEntity<OriginalPhoto, OriginalPhotoDt>(Select(o => o.PhotoId == photoId).FirstOrDefault());
        }
    }
}
