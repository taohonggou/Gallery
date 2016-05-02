using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tool;
using Manager;

namespace GalleryUI.Controllers
{
    public class ScanOrSupportController : BaseController
    {
        [HttpPost]
        public ActionResult AddSupport(string photoId)
        {
            if (!IsLogin())
                return Content(OutputHelper.GetOutputResponse(ResultCode.NoLogin));
            return Content(new ScanOrSupportManager().AddSupport(photoId,user.UserId));
        }
	}
}