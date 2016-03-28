using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool;
using DataTrsfer;
using EF;

namespace Server
{
    public class PhotoCategoryServer : BaseService<PhotoCategoryServer>
    {
        public bool Add(PhotoCategoryDt photocategory)
        {
            base.Add(photocategory);
            return Save() > 0;
        }
        public bool Update(PhotoCategoryDt photocategory)
        {
            base.Update(photocategory);
            return Save() > 0;
        }
        public bool Delete(int id)
        {
            base.Delete(id);
            return Save() > 0;
        }
        //public PhotoCategoryDt Get(int id)
        //{
        //    return TransferObject.ConvertObjectByEntity<PhotoCategory, PhotoCategoryDt>(base.Select(id));
        //}
    }
}
