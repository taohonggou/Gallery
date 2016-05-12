using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manager;
using Tool;

namespace GalleryUI.Controllers
{
    public class PhotoGalleryController : BaseController
    {
        /// <summary>
        /// 添加相册
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(string name)
        {
            if (!IsLogin())
                return Content(OutputHelper.GetOutputResponse(ResultCode.NoLogin));
            return Content(new PhotoGalleryManager().Add(name, user.UserId));
        }

        public ActionResult UpdateFengmian(string photoid, string photogalleryid)
        {
            return Content(new PhotoGalleryManager().UpdateFengmian(photoid, photogalleryid));
        }
        public ActionResult GetList()
        {
            return Content(new PhotoGalleryManager().GetListOutputModel(user.UserId));
        }
        public ActionResult DeleteAll(string idstr)
        {
            string[] ids = idstr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return Content(new PhotoGalleryManager().DeleteAll(ids));
        }

    }
}