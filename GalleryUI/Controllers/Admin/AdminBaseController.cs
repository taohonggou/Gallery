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

	}
}