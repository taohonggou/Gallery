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
        public bool Add(VerifyRegisterDt verifyDt)
        {
            base.Add<VerifyRegister>(TransferObject.ConvertObjectByEntity<VerifyRegisterDt, VerifyRegister>(verifyDt));
            return Save() > 0;
        }

        /// <summary>
        /// 判断发给此人的链接是否已经使用
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsSend(string email)
        {
            return  Select(o => o.UserId == email && (!o.IsUsed || o.OutDate < DateTime.Now)).Any();
        }
    }
}
