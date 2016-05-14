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
        public ActionResult Gallerys()
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
            return Content(new PhotoManager().GetListByGallery(user.UserId, galleryId));
        }

        public ActionResult GetAllPhotos()
        {
            if (!IsLogin())
                return Content(OutputHelper.GetOutputResponse(ResultCode.NoLogin));
            return Content(new PhotoManager().GetAllPhotoByUserId(user.UserId));
        }

        //public ActionResult GetLikePhotos()
        //{
        //    if (!IsLogin())
        //        return Content(OutputHelper.GetOutputResponse(ResultCode.NoLogin));
        //    return Content(new LikeManager().GetLikePhotos(user.UserId));
        //}


        public ActionResult Photos(string pageIndex = "1", string pageSize = "20")
        {
            if (!IsLogin())
                return RedirectHome();
            ViewBag.ListPhotos = new PhotoManager().GetPageByUserIdOrderByDatetime(user.UserId, pageIndex, pageSize);
            return View();
        }

        public ActionResult PersonalData()
        {
            if (!IsLogin())
                return RedirectHome();
            return View();
        }

        public ActionResult Collection()
        {
            if (!IsLogin())
                return RedirectHome();
            //先获取20张照片
            ViewBag.ListCollections = new PhotoManager().GetPageByCollection(user.UserId, "1", "20");
            return View();
        }

        public ActionResult GalleryPhotos(int galleryId)
        {
            if (!IsLogin())
                return RedirectHome();
            PhotoGalleryDt pg = (PhotoGalleryDt)new PhotoGalleryManager().Get(galleryId).Data;
            if (pg != null)
                ViewBag.galleryName = pg.Name;
            ViewBag.galleryid = galleryId;
            ViewBag.GalleryPhotos = new PhotoManager().GetListByGallery(user.UserId, galleryId);
            return View();

        }
    }
}