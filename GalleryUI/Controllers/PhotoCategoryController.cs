using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manager;
using DataTrsfer;
namespace GalleryUI.Controllers
{
    public class PhotoCategoryController : BaseController
    {
        //
        // GET: /PhotoCategory/
        public ActionResult GetList()
        {
           return Content(new PhotoCategoryManager().GetList());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowCategory(string categoryId)
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                return RedirectHome();
            return View();
        }
	}
}