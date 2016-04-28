using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manager;
using DataTrsfer;
using Tool;
namespace GalleryUI.Controllers.Admin
{
    public class AdminCommentController : AdminBaseController
    {
        //
        // GET: /AdminComment/
        public ActionResult Index(string pageindex)
        {
            int pagesize = 10;
            int pagecount;
            List<CommentDt> list = new CommentManager().GetPage(pageindex, pagesize, out pagecount);
            ViewBag.pageIndex = pageindex;
            ViewBag.pageCount = pagecount;
            return View(list);
        }
        public ActionResult Delete(string id)
        {
            int i;
            if(!int.TryParse(id, out i))
            {
                return Content(new OutputModel { StatusCode = 3 });//3是错误参数
            }
            return Content(new CommentManager().Delete(i));
        }
    }
}