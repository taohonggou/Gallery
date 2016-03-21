﻿using System;
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
            if (userServer.Add(user))
            {
                EmailHelper.SendEmail("[Gallery]感谢注册Gallery,请验证邮箱" + email, "cl889521：您好，感谢您注册Gallery，请点击下面的链接验证您的邮箱：<a href='http://www.baidu.com?token=o1e2d70h9wu1tcg6ojpcxx3380pqt9x1'>http://www.baidu.com?token=o1e2d70h9wu1tcg6ojpcxx3380pqt9x1</a>该链接7天后失效。", email);
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
    }
}