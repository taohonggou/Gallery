using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Tool;
using DataTrsfer;
using System.Web;

namespace Manager
{
    public class UploadManager
    {
        public OutputModel UploadImg(HttpPostedFileBase img)
        {
            OutputModel model=  UploadHelper.UploadImg(img);
            if (model.StatusCode != 1)
            {
                return model;
            }
            return new OutputModel();
        }
    }
}
