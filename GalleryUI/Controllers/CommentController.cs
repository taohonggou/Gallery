using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tool;
using Manager;

namespace GalleryUI.Controllers
{
    public class CommentController : BaseController
    {
        public ActionResult AddComment(string content, string photoId)
        {
            try
            {
                if (!IsLogin())
                    return Content( OutputHelper.GetOutputResponse(ResultCode.NoLogin));
                return Content(new CommentManager().Add(content,photoId));
            }
            catch 
            {
                return Content(OutputHelper.GetOutputResponse(ResultCode.Error));
                
            }
        }

	}
}