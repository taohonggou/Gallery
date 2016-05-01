using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrsfer
{
   public class CategoryPhoto
    {
       public string CategoryName { get; set; }

       private List<PhotoDt> categoryPhotos = new List<PhotoDt>();

       public List<PhotoDt> CategoryPhotos
       {
           get { return categoryPhotos; }
           set { categoryPhotos = value; }
       }
    }
}
