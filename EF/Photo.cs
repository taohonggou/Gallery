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
    
    public partial class Photo
    {
        public int PhotoId { get; set; }
        public Nullable<int> LocationId { get; set; }
        public int PhotoCategoryId { get; set; }
        public Nullable<int> PhotoGalleryId { get; set; }
        public string UserId { get; set; }
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public short Status { get; set; }
        public System.DateTime DateTime { get; set; }
    
        public virtual UserInfo UserInfo { get; set; }
    }
}
