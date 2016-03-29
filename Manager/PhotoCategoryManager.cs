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
        public OutputModel Add(PhotoCategoryDt photocategory)
        {
            if (photocategory == null)
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            if (Server.Get(photocategory.Name) != null)
                return OutputHelper.GetOutputResponse(ResultCode.DataExisted);
            if (Server.Add(photocategory))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
        public OutputModel Update(PhotoCategoryDt photocategory)
        {
            if (photocategory == null)
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            if (Server.Update(photocategory))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
        public OutputModel Delete(int id)
        {
            if (Server.Delete(id))
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
    }
}
