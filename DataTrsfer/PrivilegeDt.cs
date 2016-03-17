using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrsfer
{
    public  class PrivilegeDt
    {
        public int PrivilegeId { get; set; }
        public short PrivilegeMaster { get; set; }
        public string PrivilegeMasterValue { get; set; }
        public short PrivilegeAccess { get; set; }
        public int PrivilegeAccessValue { get; set; }
        public short PrivilegeOperation { get; set; }
    }
}
