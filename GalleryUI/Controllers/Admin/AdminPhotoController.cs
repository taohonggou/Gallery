using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manager;
using Tool;
using DataTrsfer;

namespace GalleryUI.Controllers.Admin
{
    public class AdminPhotoController : AdminBaseController
    {
        //
        // GET: /AdminPhoto/
        public ActionResult Index(string pageindex, string name, string author, string category)
        {
            int pagesize = 5;
            int pagecount;
            List<PhotoDt> list = new PhotoManager().GetPage(pageindex, pagesize,name,author,category,out pagecount);
            ViewBag.pageIndex = pageindex;
            ViewBag.pageCount = pagecount;
            ViewBag.name = name;
            ViewBag.author = author;
            ViewBag.category = category;
            return View(list);
        }
        public ActionResult Delete(string id)
        {
           return Content(new PhotoManager().Delete(id));
        }
    }
}