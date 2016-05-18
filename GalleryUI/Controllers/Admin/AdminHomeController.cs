using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GalleryUI.Controllers.Admin
{
    public class AdminHomeController : AdminBaseController
    {
        //
        // GET: /AdminHome/
        public ActionResult Index()
        {
            if (!IsLogin())
                return RedirectLogin();
            return View();
        }


        
	}
}