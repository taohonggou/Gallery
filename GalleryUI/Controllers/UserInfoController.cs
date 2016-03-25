using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tool;
using Manager;

namespace GalleryUI.Controllers
{
    public class UserInfoController : BaseController
    {
        //
        // GET: /UserInfo/
        public ActionResult Login(string email, string md5Pwd)
        {
            OutputModel model = new UserInfoManager().Login(email, md5Pwd);
            //if (model.StatusCode == 1)
            //    return RedirectToAction("Index", "Home");
            return Content(model);
        }

        [HttpPost]
        public ContentResult Register(string email,string md5Pwd)
        {
            return Content(new UserInfoManager().Add(email, md5Pwd));
        }

        public ActionResult VerifyEmail(string guid)
        {
            VerifyRegisterManager verifyManager = new VerifyRegisterManager();
            if (verifyManager.VerifyEmail(guid))
                return RedirectToAction("Index", "Home");
            else
                return View();
        }
	}
}