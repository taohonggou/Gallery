using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GalleryUI.Controllers
{
    public class PhotoController : BaseController
    {
        //
        // GET: /Photo/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            return  View();
        }
	}
}