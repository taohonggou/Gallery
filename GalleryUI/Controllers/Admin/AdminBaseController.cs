using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTrsfer;
using Tool;
namespace GalleryUI.Controllers.Admin
{
    public class AdminBaseController : Controller
    {
        protected ContentResult Content(OutputModel model)
        {
            return Content(JsonHelper.SerializeObject(model), "application/json", System.Text.Encoding.UTF8);
        }

        protected AdminUserDt adminUser { get; set; }

        public AdminBaseController()
        {
            adminUser = System.Web.HttpContext.Current.Session["adminUser"] as AdminUserDt;
            ViewBag.user = adminUser;
            ViewBag.IsLogin = adminUser != null;

        }

        /// <summary>
        /// 判断此用户是否登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        protected bool IsLogin()
        {
            return adminUser != null;
        }

        protected ActionResult RedirectLogin()
        {
            return Redirect("/AdminUserInfo/Login");
        }

	}
}