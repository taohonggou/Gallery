using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manager;

namespace GalleryUI.Controllers.Admin
{
    public class AdminUserInfoController : AdminBaseController
    {
        //
        // GET: /AdminUserInfo/
        public ActionResult Index()
        {

            return View(new UserInfoManager().GetList());
        }

        public ActionResult Delete(string id)
        {
            return Content(new UserInfoManager().Delete(id));
        }
	}
}