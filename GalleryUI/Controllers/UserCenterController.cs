using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manager;
using DataTrsfer;

namespace GalleryUI.Controllers
{
    public class UserCenterController : BaseController
    {
        //
        // GET: /UserCenter/
        public ActionResult UserCenter()
        {
            if (!IsLogin())
                return Redirect("/");
            //获取此人所有的图片
            PhotoManager manager = new PhotoManager();
            List<PhotoDt> list= manager.GetAllPhotoByUserId(user.UserId);
            ViewBag.Photos = list;
            return View();
        }
	}
}