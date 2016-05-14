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
        private UserInfoServer server = ObjectContainer.GetInstance<UserInfoServer>();
        private VerifyRegisterServer vServer = ObjectContainer.GetInstance<VerifyRegisterServer>();
        public OutputModel Add(string email, string md5Pwd)
        {
            if (FormatVerify.IsNullOrEmpty(email, md5Pwd))
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            if (!FormatVerify.VerifyEmail(email))
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            UserInfoDt user = server.GetUserInfo(email);
            if (user != null)
                return OutputHelper.GetOutputResponse(ResultCode.DataExisted, "此邮箱已经注册");
            user = new UserInfoDt
            {
                DateTime = DateTime.Now,
                Name = email,
                Pwd = MD5Helper.GeneratePwd(md5Pwd),
                Status = 0,
                UserId = email
            };
            VerifyRegisterDt verifyDt = new VerifyRegisterDt
            {
                GUID = Guid.NewGuid().ToString().Replace("-", ""),
                IsUsed = false,
                OutDate = DateTime.Now.AddDays(7.0),
                UserId = email
            };
            if (server.Add(user, verifyDt))
            {
                EmailHelper.SendEmail("[Gallery]感谢注册Gallery,请验证邮箱" + email, email.Substring(0, email.IndexOf('@')) + "：您好，感谢您注册Gallery，请点击下面的链接验证您的邮箱：<a href='http://121.42.58.78/UserInfo/VerifyEmail?guid=" + verifyDt.GUID + "'>http://121.42.58.78/UserInfo/VerifyEmail?guid=" + verifyDt.GUID + "</a>该链接7天后失效。", email);
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
            UserInfoDt user = server.GetUserInfo(email);
            if (user == null)
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied, "邮箱或密码错误");
            string pwd = MD5Helper.GeneratePwd(md5Pwd);
            if (!user.Pwd.Equals(pwd, StringComparison.OrdinalIgnoreCase))
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied, "邮箱或密码错误");
            HttpContext.Current.Session["user"] = user;
            return OutputHelper.GetOutputResponse(ResultCode.OK);
        }

        public OutputModel LostPwd(string email)
        {
            if (FormatVerify.IsNullOrEmpty(email))
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            if (!FormatVerify.VerifyEmail(email))
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);
            UserInfoDt user = server.GetUserInfo(email);
            if (user == null)
                return OutputHelper.GetOutputResponse(ResultCode.DataExisted, "此邮箱未注册");
            if (new VerifyRegisterServer().IsSend(email))
                return OutputHelper.GetOutputResponse(ResultCode.ConditionNotSatisfied, "您的邮箱已经发送,不用重复点击");
            VerifyRegisterDt verifyDt = new VerifyRegisterDt
            {
                GUID = Guid.NewGuid().ToString().Replace("-", ""),
                IsUsed = false,
                OutDate = DateTime.Now.AddDays(7.0),
                UserId = email
            };
            if (vServer.Add(verifyDt))
            {
                EmailHelper.SendEmail("[Gallery]欢迎来到Gallery,点击重置密码" + email, email.Substring(0, email.IndexOf('@')) + "：您好，请点击下面的链接验证您的邮箱：<a href='http://121.42.58.78/UserInfo/LostPwdVerifyEmail?guid=" + verifyDt.GUID + "'>http://121.42.58.78/UserInfo/LostPwdVerifyEmail?guid=" + verifyDt.GUID + "</a>该链接7天后失效。", email);
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            }
            else
                return OutputHelper.GetOutputResponse(ResultCode.Error);

        }


        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public OutputModel Update(UserInfoDt userInfo)
        {
            if (userInfo == null)
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            userInfo.Pwd = MD5Helper.GeneratePwd(userInfo.Pwd);
            if (server.Update(userInfo))
            {
                HttpContext.Current.Session["user"] = userInfo;
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            }
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }

        /// <summary>
        /// 编辑个人信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public OutputModel Edit(UserInfoDt userInfo)
        {
            if (userInfo == null)
                return OutputHelper.GetOutputResponse(ResultCode.NoParameter);
            if(FormatVerify.IsNullOrEmpty(userInfo.Address,userInfo.Name)||!FormatVerify.VerifyMobilePhone(userInfo.Phone))
                return OutputHelper.GetOutputResponse(ResultCode.ErrorParameter);

            UserInfoDt user = server.GetUserInfo(userInfo.UserId);
            user.Address = userInfo.Address;
            user.Age = userInfo.Age;
            user.Gender = userInfo.Gender;
            user.Name = userInfo.Name;
            user.Phone = userInfo.Phone;
            if (server.Update(user))
            {
                HttpContext.Current.Session["user"] = user;
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            }else
                return OutputHelper.GetOutputResponse(ResultCode.Error);
        }

        /// <summary>
        /// 根据邮箱获取用户对象
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public OutputModel Get(string loginId)
        {
            UserInfoDt u = server.GetUserInfo(loginId);
            if (u == null)
                return OutputHelper.GetOutputResponse(ResultCode.NoData);
            return OutputHelper.GetOutputResponse(ResultCode.OK, u);
        }

        public UserInfoDt GetByUserId(string userId)
        {
            return server.GetUserInfo(userId);
        }

        public List<UserInfoDt> GetList()
        {
            return server.GetList();
        }

        public OutputModel Delete(string id)
        {
            if (server.Delete(id))
                return OutputHelper.GetOutputResponse(ResultCode.OK);
            return OutputHelper.GetOutputResponse(ResultCode.Error);
        }
    }
}
