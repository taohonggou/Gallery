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
        public bool Add(UserInfoDt userDt)
        {
            base.Add(TransferObject.ConvertObjectByEntity<UserInfoDt,UserInfo>(userDt));
            return Save() > 0;
        }

        public UserInfoDt GetUserInfo(string userId)
        {
            return TransferObject.ConvertObjectByEntity<UserInfo,UserInfoDt>( Select(o => o.UserId == userId).FirstOrDefault());
        }

    }
}
