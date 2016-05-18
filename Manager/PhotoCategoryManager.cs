using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool;
using Server;
using DataTrsfer;
namespace Manager
{
    public class PhotoCategoryManager
    {
        private PhotoCategoryServer Server = ObjectContainer.GetInstance<PhotoCategoryServer>();
        public OutputModel Add(string name, string priority)
        {
            int p;//显示级别，数越大，显示越靠前
            if (!int.TryParse(priority, out p))
                p = 0;
            if (string.IsNullOrWhiteSpace(name))
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            PhotoCategoryDt photocategory = new PhotoCategoryDt { Name = name, Priority = p };
            if (Server.Get(photocategory.Name) != null)
                return OutputHelper.GetOutputResponse(ResultCode.DataExisted);
            if (Server.Add(photocategory))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
        public OutputModel Update(string id, string name, string priority)
        {
            int i; int p;
            if (!int.TryParse(priority, out p))
                p = 0;
            if (string.IsNullOrWhiteSpace(name))
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            if (!int.TryParse(id, out i))
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            PhotoCategoryDt photocategory = new PhotoCategoryDt { PhotoCategoryId = i, Name = name, Priority = p };
            if (Server.Update(photocategory))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
        public OutputModel Delete(string id)
        {
            int i;
            if (!int.TryParse(id, out i))
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            if (Server.Delete(i))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
        public OutputModel Get(int id)
        {
            PhotoCategoryDt p = Server.Get(id);
            if (p == null)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK, p);
        }

        public PhotoCategoryDt GetPhotoCategory(string  photoCategoryId)
        {
            int iPhotoCategoryId;
            if (!int.TryParse(photoCategoryId, out iPhotoCategoryId))
                return new PhotoCategoryDt();
            return Server.Get(iPhotoCategoryId);
        }

        public OutputModel Get(string name)
        {
            PhotoCategoryDt p = Server.Get(name);
            if (p == null)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK, p);
        }
        public OutputModel GetList()
        {
            List<PhotoCategoryDt> list = Server.GetList();
            if (list.Count == 0)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK, list);
        }

        public List<PhotoCategoryDt> GetAll()
        {
            return  Server.GetList();
        }

        public List<PhotoCategoryDt> GetPage(string pageindex, int pagesize, out int pagecount)
        {
            int pageIndex;
            FormatVerify.PageCheck(pageindex, out pageIndex);
            int rowcount;
            List<PhotoCategoryDt> list = Server.GetPage(pageIndex, pagesize, out rowcount);
            pagecount = (int)Math.Ceiling(rowcount * 1.0 / pagesize);
            return list;
        }

        
    }
}
