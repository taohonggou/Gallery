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
        public ActionResult Upload(int? galleryId=null)
        {
            if (!IsLogin())
                return RedirectHome();
            //获取个人的相册
            PhotoGalleryManager galleryManager = new PhotoGalleryManager();
            List<PhotoGalleryDt> list = galleryManager.GetList(user.UserId);
            ViewBag.Gallery = list;
            if (galleryId != null)
                ViewBag.galleryId = galleryId;
            ViewBag.Category= new PhotoCategoryManager().GetAll();
            return View();
        }

        [HttpPost]
        public void PostUpload()
        {
            if (!IsLogin())
                return ;
            string photoGalleryId = Request.Form["photoGalleryId"];
            string PhotoCategoryId = Request.Form["PhotoCategoryId"];
            string name=Request.Form["name"];
            HttpFileCollectionBase file = Request.Files;
            UploadManager manager = new UploadManager();
            manager.UploadImg(file["file"], user.UserId, photoGalleryId, PhotoCategoryId,name);
        }
    }
}