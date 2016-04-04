using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using Tool;
using DataTrsfer;

namespace Manager
{
    public class VerifyRegisterManager
    {
        private VerifyRegisterServer verifyServer = ObjectContainer.GetInstance<VerifyRegisterServer>();
        public bool VerifyEmail(string guid)
        {
            if (string.IsNullOrEmpty(guid))
                return false;
            VerifyRegisterDt verifyDt = verifyServer.Get(guid);
            if (verifyDt == null || (verifyDt.OutDate < DateTime.Now) || verifyDt.IsUsed)
                return false;
            //验证通过
            UserInfoServer userServer = ObjectContainer.GetInstance<UserInfoServer>();
            UserInfoDt userDt = userServer.GetUserInfo(verifyDt.UserId);
            if (userDt == null)
                return false;

            userDt.Status = 1;
            verifyDt.IsUsed = true;

            if (!userServer.Update(userDt, verifyDt))
                return false;
            System.Web.HttpContext.Current.Session["user"] = userDt;
            return true;
        }

        /// <summary>
        /// 忘记密码的验证邮箱
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool VerifyEmailResetPwd(string guid, out string email)
        {
            email = "";
            if (string.IsNullOrEmpty(guid))
                return false;
            VerifyRegisterDt verifyDt = verifyServer.Get(guid);
            if (verifyDt == null || (verifyDt.OutDate < DateTime.Now) || verifyDt.IsUsed)
                return false;
            email = verifyDt.UserId;
            verifyDt.IsUsed = true;
            if (verifyServer.Update(verifyDt))
                return true;
            else
                return false;
        }


    }
}
