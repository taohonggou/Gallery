using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tool;
using Manager;
using DataTrsfer;

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
        public ContentResult Register(string email, string md5Pwd)
        {
            UserInfoManager manager = new UserInfoManager();
            return Content(manager.Add(email, md5Pwd));
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
        public ActionResult LostPwd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LostPwd(string email)
        {
            UserInfoManager manager = new UserInfoManager();
            OutputModel o = manager.LostPwd(email);
            if (o.StatusCode == 1)
                Session["email"] = email;
            return Content(o);
        }
        public ActionResult LostPwdVerifyEmail(string guid)
        {
            VerifyRegisterManager verifyManager = new VerifyRegisterManager();
            string email = "";
            if (verifyManager.VerifyEmailResetPwd(guid, out email))
            {
                ViewBag.Email = email;
                return View("ResetPwd");
            }
            else
                return View("VerifyEmail");
        }
        public ActionResult ResetPwd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPwd(string email, string md5Pwd)
        {
            UserInfoManager manager = new UserInfoManager();
            UserInfoDt u = new UserInfoDt();
            OutputModel o = manager.Get(email);
            if (o.StatusCode == 1)
            {
                u = (UserInfoDt)o.Data;
                u.Pwd = md5Pwd;
                return Content(manager.Update(u));
            }
            return Content(o);
        }
    }
}