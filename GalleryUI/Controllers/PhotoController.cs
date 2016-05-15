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

        public ActionResult GetCategory(int num)
        {
            return Content(new PhotoManager().GetCategoryHottest(num));
        }

        public ActionResult GetPageCategory(string pageIndex,string pageSize,string categoryId)
        {
            return Content(new PhotoManager().GetPageByCategoryId(pageIndex, pageSize, categoryId));
        }

        public ActionResult GetPageByUserId(string pageIndex,string pageSize)
        {
            if (!IsLogin())
                return Content(OutputHelper.GetOutputResponse(ResultCode.NoLogin));
            return Content(new PhotoManager().GetPageByUserId(user.UserId,pageIndex, pageSize));
        }

        public ActionResult GetPageByUserCollection(string pageIndex,string pageSize)
        {
            if(!IsLogin())
                return Content( OutputHelper.GetOutputResponse(ResultCode.NoLogin));
            return Content(new PhotoManager().GetPageByCollections(user.UserId,pageIndex,pageSize));
        }


        public ActionResult DeleteAll(string idstr)
        {
            string[] ids = idstr.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
            return Content(new PhotoManager().DeleteAll(ids));
        }
        public ActionResult UpdateGallery(int galleryid, string pids)
        {
            string[] ids = pids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return Content(new PhotoManager().UpdateGallery(galleryid, ids));
        }

        public ActionResult Search(string name)
        {
            if (string.IsNullOrEmpty(name))
                return RedirectHome();
           // ViewBag.SearchResult = new PhotoManager().SearchByNameReturnList(name);
            return View(new PhotoManager().SearchByNameReturnList(name));
        }

       
        [HttpPost]
        public ActionResult Edit(string name,string photoId)
        {
            if(!IsLogin())
                return Content(OutputHelper.GetOutputResponse(ResultCode.NoLogin));
            return Content(new PhotoManager().EditPhoto(name,photoId,user.UserId));
        }
	}
}