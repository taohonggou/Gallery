using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrsfer
{
   public  class VerifyRegisterDt
    {
        public string GUID { get; set; }
        public string UserId { get; set; }
        public bool IsUsed { get; set; }
        public System.DateTime OutDate { get; set; }
    }
}
