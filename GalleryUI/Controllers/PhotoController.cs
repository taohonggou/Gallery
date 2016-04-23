﻿using System;
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

        public ActionResult Details()
        {
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
	}
}