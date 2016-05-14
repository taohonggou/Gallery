using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Tool;
using DataTrsfer;
using System.Web;

namespace Manager
{
    public class AdminUserManager
    {
        private AdminUserServer service = new AdminUserServer();

        public OutputModel Login(string userId, string pwd)
        {
            if (FormatVerify.IsNullOrEmpty(userId, pwd))
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            AdminUserDt user = service.Get(userId, MD5Helper.GeneratePwd(pwd));
            if (user == null)
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied, "账号或密码不正确");
            HttpContext.Current.Session["adminUser"] = user;
            return OutputHelper.GetOutputResponse(ResultCode.OK);
        }
    }
}
