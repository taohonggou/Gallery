using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tool;
using Manager;
using DataTrsfer;

namespace GalleryUI.Controllers.Admin
{
    public class AdminPhotoCategoryController : AdminBaseController
    {
        //
        // GET: /AdminPhotoCategory/
        public ActionResult Index(string pageindex)
        {
            int pagesize = 5;
            int pagecount;
            List<PhotoCategoryDt> list = new PhotoCategoryManager().GetPage(pageindex, pagesize, out pagecount);
            ViewBag.pageIndex = pageindex;
            ViewBag.pageCount = pagecount;
            return View(list);
        }
        public ActionResult Add(string name, string priority)
        {
           return Content(new PhotoCategoryManager().Add(name, priority));
        }
        public ActionResult Edit(string id, string name, string priority)
        {
           return Content(new PhotoCategoryManager().Update(id, name, priority));
        }
        public ActionResult Delete(string id)
        {
            return Content(new PhotoCategoryManager().Delete(id));
        }
    }
}