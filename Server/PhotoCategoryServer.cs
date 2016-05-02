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
    public class PhotoCategoryServer : BaseService<PhotoCategory>
    {
        public bool Add(PhotoCategoryDt photocategory)
        {
            base.Add(TransferObject.ConvertObjectByEntity<PhotoCategoryDt,PhotoCategory>(photocategory));
            return Save() > 0;
        }
        public bool Update(PhotoCategoryDt photocategory)
        {
            base.Update(TransferObject.ConvertObjectByEntity<PhotoCategoryDt, PhotoCategory>(photocategory));
            return Save() > 0;
        }
        public bool Delete(int id)
        {
            base.Delete(id);
            return Save() > 0;
        }
        public PhotoCategoryDt Get(int id)
        {
            return TransferObject.ConvertObjectByEntity<PhotoCategory, PhotoCategoryDt>(base.Select(o=>o.PhotoCategoryId==id).FirstOrDefault());
        }
        public PhotoCategoryDt Get(string name)
        {
            return TransferObject.ConvertObjectByEntity<PhotoCategory, PhotoCategoryDt>(base.Select(o => o.Name == name).FirstOrDefault());
        }
        public List<PhotoCategoryDt> GetList()
        {
            return TransferObject.ConvertObjectByEntity<PhotoCategory, PhotoCategoryDt>(base.Select(o => true).OrderByDescending(o=>o.Priority).ToList());
        }
        public List<PhotoCategoryDt> GetPage(int pageindex, int pagesize, out int rowcount)
        {
            List<PhotoCategory> list = SelectDesc(pageindex, pagesize, o => true, o => o.Priority, out rowcount).ToList();
            return TransferObject.ConvertObjectByEntity<PhotoCategory, PhotoCategoryDt>(list);
        }
    }
}
