using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace EF
{
   public static  class EFDataAccess
    {
       public static DbContext CreateContext()
       {
           if (CallContext.GetData("context") == null)
               CallContext.SetData("context", new DbContext("GalleryEntities"));
           return CallContext.GetData("context") as DbContext;
       }
    }
}
