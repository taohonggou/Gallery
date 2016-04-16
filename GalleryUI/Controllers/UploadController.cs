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
            if (user == null)
                return Redirect("/");
            return View();
        }

        [HttpPost]
        public void PostUpload()
        {
            HttpFileCollectionBase file= Request.Files;
            UploadManager manager = new UploadManager();
            manager.UploadImg(file["file"]);
        }
	}
}