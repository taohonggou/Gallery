using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace DataTrsfer
{
    public  class UserInfoDt
    {
        public string UserId { get; set; }
        [JsonIgnore]
        public string Pwd { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 用户状态：0没有进行邮箱验证   1进行邮箱验证
        /// </summary>
        public int Status { get; set; }
        public string HeadUrl { get; set; }
        public System.DateTime DateTime { get; set; }
    }
}
