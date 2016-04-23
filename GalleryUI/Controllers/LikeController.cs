using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Manager;
using Tool;

namespace GalleryUI.Controllers
{
    public class LikeController : BaseController
    {
        [HttpPost]
        public ActionResult AddLike(string photoId)
        {
            if (!IsLogin())
                return Content(OutputHelper.GetOutputResponse(ResultCode.NoLogin));
            return Content(new LikeManager().AddLike(photoId,user.UserId));
        }
	}
}