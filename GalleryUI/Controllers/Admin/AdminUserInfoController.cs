using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manager;
using Tool;
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

        public ActionResult Login()
        {
            ViewBag.Display = "none";
            ViewBag.Message ="";
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userId, string pwd)
        {
            OutputModel model = new AdminUserManager().Login(userId, pwd);
            if (model.StatusCode == 1)
                return Redirect("/adminhome/index");
            ViewBag.Display = "block";
            ViewBag.Message = model.Message;
            return View();
        }

        public ActionResult LogOut()
        {
            Session["adminUser"] = null;
            return RedirectToAction("Index", "AdminHome");
        }
	}
}