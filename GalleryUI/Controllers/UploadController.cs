using DataTrsfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manager;

namespace GalleryUI.Controllers
{
    public class UploadController : BaseController
    {
        //
        // GET: 
        [HttpGet]
        public ActionResult Upload()
        {
            if (!IsLogin())
                return Redirect("/");
            //获取个人的相册
            PhotoGalleryManager galleryManager = new PhotoGalleryManager();
            List<PhotoGalleryDt> list = galleryManager.GetList(user.UserId);
            ViewBag.Gallery = list;
            return View();
        }

        [HttpPost]
        public void PostUpload()
        {
            if (!IsLogin())
                return ;
            string photoGalleryId = Request.Form["photoGalleryId"];
            string PhotoCategoryId = Request.Form["PhotoCategoryId"];
            HttpFileCollectionBase file = Request.Files;
            UploadManager manager = new UploadManager();
            manager.UploadImg(file["file"], user.UserId, photoGalleryId, PhotoCategoryId);
        }
    }
}