//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserInfo
    {
        public UserInfo()
        {
            this.Comment = new HashSet<Comment>();
            this.Like = new HashSet<Like>();
            this.Photo = new HashSet<Photo>();
            this.PhotoGallery = new HashSet<PhotoGallery>();
            this.ScanOrSupport = new HashSet<ScanOrSupport>();
            this.UserRole = new HashSet<UserRole>();
        }
    
        public string UserId { get; set; }
        public string Pwd { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string HeadUrl { get; set; }
        public System.DateTime DateTime { get; set; }
    
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Like> Like { get; set; }
        public virtual ICollection<Photo> Photo { get; set; }
        public virtual ICollection<PhotoGallery> PhotoGallery { get; set; }
        public virtual ICollection<ScanOrSupport> ScanOrSupport { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
