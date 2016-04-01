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
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string md5Pwd)
        {
            OutputModel model = new UserInfoManager().Login(email, md5Pwd);
            return Content(model);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ContentResult Register(string email,string md5Pwd)
        {
            UserInfoManager manager = new UserInfoManager();
            ContentResult c=  Content(manager.Add(email, md5Pwd));
            return c;
        }

        public ActionResult VerifyEmail(string guid)
        {
            VerifyRegisterManager verifyManager = new VerifyRegisterManager();
            if (verifyManager.VerifyEmail(guid))
                return RedirectToAction("Index", "Home");
            else
                return View();
        }
        public ActionResult LogOut()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }
	}
}