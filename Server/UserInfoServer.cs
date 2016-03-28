using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTrsfer;
using Tool;
using EF;
namespace Server
{
    public class UserInfoServer : BaseService<UserInfo>
    {
        public bool Add(UserInfoDt userDt,VerifyRegisterDt verifyDt)
        {
            base.Add<VerifyRegister>(TransferObject.ConvertObjectByEntity<VerifyRegisterDt, VerifyRegister>(verifyDt));
            base.Add(TransferObject.ConvertObjectByEntity<UserInfoDt,UserInfo>(userDt));
            return Save() > 0;
        }

        public UserInfoDt GetUserInfo(string userId)
        {
            return TransferObject.ConvertObjectByEntity<UserInfo,UserInfoDt>( Select(o => o.UserId == userId).FirstOrDefault());
        }


        public bool Update(UserInfoDt userDt)
        {
            base.Update(TransferObject.ConvertObjectByEntity<UserInfoDt, UserInfo>(userDt));
            return Save() > 0;
        }
        public bool Delete(int id)
        {
            base.Delete(id);
            return Save() > 0;
        }
    }
}
