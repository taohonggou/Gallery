using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tool;
using Manager;

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

        public ActionResult Details(string photoId)
        {
            if (string.IsNullOrEmpty(photoId))
                return RedirectHome();
            return  View();
        }

        public ActionResult GetDetails(string photoId)
        {

            try
            {
                return Content(new PhotoManager().GetDetails(photoId) );
            }
            catch 
            {
                return Content(OutputHelper.GetOutputResponse(ResultCode.Error));
            }
            

        }

        public ActionResult Recent()
        {
            return View();
        }

        public ActionResult GetRecentPhotos(string pageIndex,string pageSize)
        {
            return Content(new PhotoManager().GetPageOrderByDateTime(pageIndex,pageSize));
        }

        public ActionResult GetHottest(string pageIndex,string pageSize)
        {
            return Content(new PhotoManager().GetPageHottest(pageIndex, pageSize));
        }

        public ActionResult GetCategory()
        {
            //PhotoManager
            return Content("") ;
        }
	}
}