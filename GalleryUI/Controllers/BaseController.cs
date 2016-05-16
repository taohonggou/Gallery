using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tool;
using DataTrsfer;
using Manager;

namespace GalleryUI.Controllers
{
    public class BaseController : Controller
    {
        protected ContentResult Content(OutputModel model)
        {
            return Content(JsonHelper.SerializeObject(model), "application/json", System.Text.Encoding.UTF8);
        }
        protected UserInfoDt user { get; set; }
        public BaseController()
        {
            user = System.Web.HttpContext.Current.Session["user"] as UserInfoDt;
            ViewBag.IsLogin = (user != null);
            //如果登陆了就每次获取最新数据
            if(user!=null)
            {
                user = new UserInfoManager().GetByUserId(user.UserId);
            }
            ViewBag.User = user;
            
        }
        /// <summary>
        /// 判断此用户是否登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        protected bool IsLogin()
        {
            return user != null;
        }

        protected ActionResult RedirectHome()
        {
            return Redirect("/");
        }
    }
}