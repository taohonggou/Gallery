using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GalleryUI;
using GalleryUI.Controllers;
using Tool;
namespace GalleryUI.Tests.Controllers
{
    [TestClass]
    public class UserInfoControllerTest
    {
        [TestMethod]
        public void Register()
        {
            string email = "1084727879@qq.com";
            string pwd = "123456";
            //e10adc3949ba59abbe56e057f20f883e
            string md5Pwd = MD5Helper.convertToMD5(pwd);

            UserInfoController controller = new UserInfoController();
            OutputModel model=JsonHelper.DeserializeStr<OutputModel>(  controller.Register(email, md5Pwd).Content);

            Assert.AreEqual(model.StatusCode, 1);
        }
    }
}
