using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;
using DataTrsfer;
using Tool;
using System.Web;

namespace Manager
{
    public class UserInfoManager
    {
        private UserInfoServer userServer = ObjectContainer.GetInstance<UserInfoServer>();
        public OutputModel Add(string email, string md5Pwd)
        {
            if (FormatVerify.IsNullOrEmpty(email, md5Pwd))
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            if (!FormatVerify.VerifyEmail(email))
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            UserInfoDt user = userServer.GetUserInfo(email);
            if (user != null)
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied, "此邮箱已经注册");
            user = new UserInfoDt { 
                              DateTime=DateTime.Now,
                              Name=email,
                              Pwd=MD5Helper.GeneratePwd(md5Pwd),
                              Status=0,
                              UserId=email
                              };
            VerifyRegisterDt verifyDt = new VerifyRegisterDt {
            GUID =Guid.NewGuid().ToString().Replace("-",""),
            IsUsed=false,
            OutDate=DateTime.Now.AddDays(7.0),
            UserId=email
            };
            if (userServer.Add(user,verifyDt))
            {
                EmailHelper.SendEmail("[Gallery]感谢注册Gallery,请验证邮箱" + email, email.Substring(0,email.IndexOf('@'))+"：您好，感谢您注册Gallery，请点击下面的链接验证您的邮箱：<a href='http://121.42.58.78/UserInfo/VerifyEmail?guid=" + verifyDt.GUID + "'>http://121.42.58.78/UserInfo/VerifyEmail?guid=" + verifyDt.GUID + "</a>该链接7天后失效。", email);
                //设置登录状态
                System.Web.HttpContext.Current.Session["user"] = user;
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            }
            else
                return OutputHelper.GetOutputResponse(ResultCode.Error);

        }

        public OutputModel Login(string email, string md5Pwd)
        {
            if (FormatVerify.IsNullOrEmpty(email, md5Pwd))
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            if (!FormatVerify.VerifyEmail(email))
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            UserInfoDt user = userServer.GetUserInfo(email);
            if(user==null)
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied, "邮箱或密码错误");
            string pwd=MD5Helper.GeneratePwd(md5Pwd);
            if (!user.Pwd.Equals(pwd, StringComparison.OrdinalIgnoreCase))
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied, "邮箱或密码错误");
            HttpContext.Current.Session["user"] = user;
            return OutputHelper.GetOutputResponse(ResultCode.OK);
        }


         /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool Update(UserInfoDt userInfo)
        {
            return userServer.Update(userInfo);
        }
        

        /// <summary>
        /// 根据邮箱获取用户对象
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public OutputModel Get(string loginId)
        {
            UserInfoDt u = userServer.GetUserInfo(loginId);
            if (u == null)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK, u);
        }

    }
}
