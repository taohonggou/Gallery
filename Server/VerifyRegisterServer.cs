using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF;
using DataTrsfer;
using Tool;

namespace Server
{
    public class VerifyRegisterServer : BaseService<VerifyRegister>
    {
        public VerifyRegisterDt Get(string guid)
        {
            return TransferObject.ConvertObjectByEntity<VerifyRegister,VerifyRegisterDt>( Select(o => o.GUID == guid).FirstOrDefault());
        }

        public bool Update(VerifyRegisterDt verifyDt)
        {
            base.Update(TransferObject.ConvertObjectByEntity<VerifyRegisterDt, VerifyRegister>(verifyDt));
            return Save() > 0;
        }
    }
}
