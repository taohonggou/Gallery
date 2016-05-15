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
    public class AdminUserServer : BaseService<AdminUser>
    {
        public AdminUserDt Get(string userId,string pwd)
        {
            return TransferObject.ConvertObjectByEntity<AdminUser, AdminUserDt>(Select(o=>o.UserId==userId&&o.Pwd==pwd).FirstOrDefault());
        }
    }
}
