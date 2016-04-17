using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using DataTrsfer;
using Tool;

namespace Manager
{
    public class PhotoManager
    {
        public List<PhotoDt> GetAllPhotoByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return new List<PhotoDt>();
            PhotoServer server = new PhotoServer();
            return server.GetList(userId);
        }
    }
}
