using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTrsfer;
using Tool;
using EF;
using System.Data.SqlClient;
namespace Server
{
    public class UserInfoServer : BaseService<UserInfo>
    {
        public bool Add(UserInfoDt userDt, VerifyRegisterDt verifyDt)
        {
            base.Add<VerifyRegister>(TransferObject.ConvertObjectByEntity<VerifyRegisterDt, VerifyRegister>(verifyDt));
            base.Add(TransferObject.ConvertObjectByEntity<UserInfoDt, UserInfo>(userDt));
            return Save() > 0;
        }

        public UserInfoDt GetUserInfo(string userId)
        {
            return TransferObject.ConvertObjectByEntity<UserInfo, UserInfoDt>(Select(o => o.UserId == userId).FirstOrDefault());
        }

        public UserInfoDt Login(string userId)
        {
            return TransferObject.ConvertObjectByEntity<UserInfo, UserInfoDt>(Select(o => o.UserId == userId&&o.Status==1).FirstOrDefault());
        }


        public bool Update(UserInfoDt userDt)
        {
            base.Update(TransferObject.ConvertObjectByEntity<UserInfoDt, UserInfo>(userDt));
            return Save() > 0;
        }


        public bool Update(UserInfoDt userDt, VerifyRegisterDt verifyDt)
        {
            base.Update(TransferObject.ConvertObjectByEntity<UserInfoDt, UserInfo>(userDt));
            base.Update<VerifyRegister>(TransferObject.ConvertObjectByEntity<VerifyRegisterDt, VerifyRegister>(verifyDt));
            return Save() > 0;
        }

        /// <summary>
        /// 更新头像
        /// </summary>
        /// <param name="headImgUrl"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Update(string headImgUrl,string userId)
        {
            string sql = "update userInfo set headurl=@headurl where userid=@userId";
            SqlParameter[] param = { 
                                   new SqlParameter("@headurl",headImgUrl),
                                   new SqlParameter("@userId",userId)
                                   };
            return  ExecuteCUD(sql, param) > 0;
        }


        public bool Delete(string id)
        {
            return  ExecuteCUD("delete from UserInfo where userId=@userId", new SqlParameter("@userId", id))>0;
        }

        public List<UserInfoDt> GetList()
        {
            return TransferObject.ConvertObjectByEntity<UserInfo, UserInfoDt>(base.Select(o => true).ToList());
        }
    }
}
