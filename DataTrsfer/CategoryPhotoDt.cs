using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrsfer
{
   public class CategoryPhotoDt
    {
       public string CategoryName { get; set; }
       public int CategoryId { get; set; }

       private List<PhotoDt> categoryPhotos = new List<PhotoDt>();

       public List<PhotoDt> CategoryPhotos
       {
           get { return categoryPhotos; }
           set { categoryPhotos = value; }
       }
    }
}
