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
        public OutputModel UploadImg(HttpPostedFileBase img,string userId)
        {
            string newPath;
            OutputModel model=  UploadHelper.UploadImg(img,out newPath);
            if (model.StatusCode != 1)
            {
                return model;
            }
            //向数据库中插入


            return new OutputModel();
        }
    }
}
