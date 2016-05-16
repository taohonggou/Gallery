using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manager;
using Tool;
using DataTrsfer;

namespace GalleryUI.Controllers
{
    public class OtherCenterController : BaseController
    {
        //
        // GET: /OtherCenter/
        public ActionResult Index(string userId, string pageIndex = "1", string pageSize = "20")
        {
            if (string.IsNullOrEmpty(userId))
                return RedirectHome();
            UserInfoDt otherUser = new UserInfoManager().GetByUserId(userId);
            if (otherUser == null)
                return RedirectHome();
            ViewBag.OtherUser = otherUser;
            ViewBag.ListPhotos = new PhotoManager().GetPageByUserIdOrderByDatetime(userId, pageIndex, pageSize);
            return View();

        }


        public ActionResult Gallery(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return RedirectHome();
            UserInfoDt otherUser = new UserInfoManager().GetByUserId(userId);
            if (otherUser == null)
                return RedirectHome();
            ViewBag.OtherUser = otherUser;
            ViewBag.ListGallery = new PhotoGalleryManager().GetList(userId);
            return View();
        }

        public ActionResult GalleryPhoto(string userId,int galleryId=-1)
        {
            if (galleryId == -1)
                return RedirectHome();
            if (string.IsNullOrEmpty(userId))
                return RedirectHome();
            UserInfoDt otherUser = new UserInfoManager().GetByUserId(userId);
            if (otherUser == null)
                return RedirectHome();
            ViewBag.OtherUser = otherUser;
            PhotoGalleryDt pg = (PhotoGalleryDt)new PhotoGalleryManager().Get(galleryId).Data;
            if (pg == null)
                return RedirectHome();
            ViewBag.galleryName = pg.Name;
            ViewBag.galleryid = galleryId;
            ViewBag.GalleryPhotos = new PhotoManager().GetListByGallery(galleryId);
            return View();
        }

        public ActionResult PersonalData(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return RedirectHome();
            UserInfoDt otherUser = new UserInfoManager().GetByUserId(userId);
            if (otherUser == null)
                return RedirectHome();
            ViewBag.OtherUser = otherUser;
            return View(otherUser);
        }
    }
}