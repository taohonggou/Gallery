using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manager;
using DataTrsfer;
using Tool;

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
            //PhotoManager manager = new PhotoManager();
            //List<PhotoDt> list= manager.GetAllPhotoByUserId(user.UserId);
            //ViewBag.Photos = list;
            ViewBag.ListGallery = new PhotoGalleryManager().GetList(user.UserId);
            return View();
        }


        public ActionResult PhotoGallery(string galleryId)
        {
            if (!IsLogin())
                return Content(OutputHelper.GetOutputResponse(ResultCode.NoLogin));
            return Content(new PhotoManager().GetListByGallery(user.UserId,galleryId));
        }

        public ActionResult GetAllPhotos()
        {
            if (!IsLogin())
                return Content(OutputHelper.GetOutputResponse(ResultCode.NoLogin));
            return Content(new PhotoManager().GetAllPhotoByUserId(user.UserId));
        }

        public ActionResult GetLikePhotos()
        {
            if (!IsLogin())
                return Content(OutputHelper.GetOutputResponse(ResultCode.NoLogin));
            return Content(new LikeManager().GetLikePhotos(user.UserId));
        }

        public ActionResult Photos()
        {
            return View();
        }
	}
}