/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2016/3/17 14:22:12                           */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Comment') and o.name = 'FK_COMMENT_REFERENCE_PHOTO')
alter table Comment
   drop constraint FK_COMMENT_REFERENCE_PHOTO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Comment') and o.name = 'FK_COMMENT_REFERENCE_USERINFO')
alter table Comment
   drop constraint FK_COMMENT_REFERENCE_USERINFO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('"Like"') and o.name = 'FK_LIKE_REFERENCE_PHOTO')
alter table "Like"
   drop constraint FK_LIKE_REFERENCE_PHOTO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('"Like"') and o.name = 'FK_LIKE_REFERENCE_USERINFO')
alter table "Like"
   drop constraint FK_LIKE_REFERENCE_USERINFO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Photo') and o.name = 'FK_PHOTO_REFERENCE_USERINFO')
alter table Photo
   drop constraint FK_PHOTO_REFERENCE_USERINFO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Photo') and o.name = 'FK_PHOTO_REFERENCE_LOCATION')
alter table Photo
   drop constraint FK_PHOTO_REFERENCE_LOCATION
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Photo') and o.name = 'FK_PHOTO_REFERENCE_PHOTOCAT')
alter table Photo
   drop constraint FK_PHOTO_REFERENCE_PHOTOCAT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Photo') and o.name = 'FK_PHOTO_REFERENCE_PHOTOGAL')
alter table Photo
   drop constraint FK_PHOTO_REFERENCE_PHOTOGAL
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PhotoGallery') and o.name = 'FK_PHOTOGAL_REFERENCE_USERINFO')
alter table PhotoGallery
   drop constraint FK_PHOTOGAL_REFERENCE_USERINFO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ScanOrSupport') and o.name = 'FK_SCANORSU_REFERENCE_PHOTO')
alter table ScanOrSupport
   drop constraint FK_SCANORSU_REFERENCE_PHOTO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ScanOrSupport') and o.name = 'FK_SCANORSU_REFERENCE_USERINFO')
alter table ScanOrSupport
   drop constraint FK_SCANORSU_REFERENCE_USERINFO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('UserRole') and o.name = 'FK_USERROLE_REFERENCE_USERINFO')
alter table UserRole
   drop constraint FK_USERROLE_REFERENCE_USERINFO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('UserRole') and o.name = 'FK_USERROLE_REFERENCE_ROLE')
alter table UserRole
   drop constraint FK_USERROLE_REFERENCE_ROLE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Comment')
            and   type = 'U')
   drop table Comment
go

if exists (select 1
            from  sysobjects
           where  id = object_id('"Like"')
            and   type = 'U')
   drop table "Like"
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Location')
            and   type = 'U')
   drop table Location
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Menu')
            and   type = 'U')
   drop table Menu
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Photo')
            and   type = 'U')
   drop table Photo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PhotoCategory')
            and   type = 'U')
   drop table PhotoCategory
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PhotoGallery')
            and   type = 'U')
   drop table PhotoGallery
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Privilege')
            and   type = 'U')
   drop table Privilege
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Role')
            and   type = 'U')
   drop table Role
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ScanOrSupport')
            and   type = 'U')
   drop table ScanOrSupport
go

if exists (select 1
            from  sysobjects
           where  id = object_id('UserInfo')
            and   type = 'U')
   drop table UserInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('UserRole')
            and   type = 'U')
   drop table UserRole
go

/*==============================================================*/
/* Table: Comment                                               */
/*==============================================================*/
create table Comment (
   CommentId            int                  identity,
   UserId               varchar(100)         not null,
   PhotoId              int                  not null,
   UpId                 int                  not null default 0,
   Content              nvarchar(Max)        not null,
   DateTime             datetime             not null,
   constraint PK_COMMENT primary key (CommentId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Comment') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Comment' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '照片评论表', 
   'user', @CurrentUser, 'table', 'Comment'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Comment')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Comment', 'column', 'UserId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主要为邮箱',
   'user', @CurrentUser, 'table', 'Comment', 'column', 'UserId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Comment')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PhotoId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Comment', 'column', 'PhotoId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '照片的Id',
   'user', @CurrentUser, 'table', 'Comment', 'column', 'PhotoId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Comment')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UpId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Comment', 'column', 'UpId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '默认为0,回复别人的评论时为被回复评论的Id',
   'user', @CurrentUser, 'table', 'Comment', 'column', 'UpId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Comment')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Content')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Comment', 'column', 'Content'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '评论内容',
   'user', @CurrentUser, 'table', 'Comment', 'column', 'Content'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Comment')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Comment', 'column', 'DateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '评论时间',
   'user', @CurrentUser, 'table', 'Comment', 'column', 'DateTime'
go

/*==============================================================*/
/* Table: "Like"                                                */
/*==============================================================*/
create table "Like" (
   LikeId               int                  identity,
   PhotoId              int                  null,
   UserId               varchar(100)         null,
   DateTime             datetime             null,
   constraint PK_LIKE primary key (LikeId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('"Like"') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Like' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '用户喜欢的照片记录表', 
   'user', @CurrentUser, 'table', 'Like'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('"Like"')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PhotoId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Like', 'column', 'PhotoId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '照片的Id',
   'user', @CurrentUser, 'table', 'Like', 'column', 'PhotoId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('"Like"')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Like', 'column', 'UserId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主要为邮箱',
   'user', @CurrentUser, 'table', 'Like', 'column', 'UserId'
go

/*==============================================================*/
/* Table: Location                                              */
/*==============================================================*/
create table Location (
   LocationId           int                  identity,
   Province             nvarchar(10)         not null,
   City                 nvarchar(10)         not null,
   District             nvarchar(10)         null,
   constraint PK_LOCATION primary key (LocationId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Location') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Location' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '照片位置表', 
   'user', @CurrentUser, 'table', 'Location'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Location')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Province')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Location', 'column', 'Province'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '省',
   'user', @CurrentUser, 'table', 'Location', 'column', 'Province'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Location')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'City')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Location', 'column', 'City'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '市',
   'user', @CurrentUser, 'table', 'Location', 'column', 'City'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Location')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'District')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Location', 'column', 'District'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '区 县',
   'user', @CurrentUser, 'table', 'Location', 'column', 'District'
go

/*==============================================================*/
/* Table: Menu                                                  */
/*==============================================================*/
create table Menu (
   MenuId               int                  identity,
   Name                 nvarchar(20)         not null,
   Url                  varchar(400)         null,
   PId                  int                  not null,
   Level                int                  not null,
   constraint PK_MENU primary key (MenuId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Menu') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Menu' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '菜单表', 
   'user', @CurrentUser, 'table', 'Menu'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Name')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'Name'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '菜单名称',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'Name'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Url')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'Url'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '跳转地址',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'Url'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'PId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '上一级菜单的Id',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'PId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Menu')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Level')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Menu', 'column', 'Level'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '菜单深度,父菜单为0,子菜单为1',
   'user', @CurrentUser, 'table', 'Menu', 'column', 'Level'
go

/*==============================================================*/
/* Table: Photo                                                 */
/*==============================================================*/
create table Photo (
   PhotoId              int                  identity,
   LocationId           int                  not null,
   PhotoCategoryId      int                  not null,
   PhotoGalleryId       int                  not null,
   UserId               varchar(100)         not null,
   ImgUrl               varchar(100)         not null,
   Name                 nvarchar(100)        not null,
   Status               smallint             not null default 0,
   DateTime             datetime             not null,
   constraint PK_PHOTO primary key (PhotoId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Photo') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Photo' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '照片表', 
   'user', @CurrentUser, 'table', 'Photo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Photo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PhotoId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Photo', 'column', 'PhotoId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '照片的Id',
   'user', @CurrentUser, 'table', 'Photo', 'column', 'PhotoId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Photo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Photo', 'column', 'UserId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主要为邮箱',
   'user', @CurrentUser, 'table', 'Photo', 'column', 'UserId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Photo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ImgUrl')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Photo', 'column', 'ImgUrl'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '照片地址(名称)',
   'user', @CurrentUser, 'table', 'Photo', 'column', 'ImgUrl'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Photo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Name')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Photo', 'column', 'Name'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '照片名称(默认为随机生成)',
   'user', @CurrentUser, 'table', 'Photo', 'column', 'Name'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Photo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Status')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Photo', 'column', 'Status'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '照片状态:-1删除   0未分享  1已分享',
   'user', @CurrentUser, 'table', 'Photo', 'column', 'Status'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Photo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Photo', 'column', 'DateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '上传时间',
   'user', @CurrentUser, 'table', 'Photo', 'column', 'DateTime'
go

/*==============================================================*/
/* Table: PhotoCategory                                         */
/*==============================================================*/
create table PhotoCategory (
   PhotoCategoryId      int                  identity,
   Name                 nvarchar(50)         not null,
   constraint PK_PHOTOCATEGORY primary key (PhotoCategoryId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PhotoCategory') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PhotoCategory' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '照片类别表', 
   'user', @CurrentUser, 'table', 'PhotoCategory'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PhotoCategory')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Name')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PhotoCategory', 'column', 'Name'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分类名称',
   'user', @CurrentUser, 'table', 'PhotoCategory', 'column', 'Name'
go

/*==============================================================*/
/* Table: PhotoGallery                                          */
/*==============================================================*/
create table PhotoGallery (
   PhotoGalleryId       int                  identity,
   UserId               varchar(100)         not null,
   Name                 nvarchar(50)         not null,
   DateTime             datetime             not null,
   constraint PK_PHOTOGALLERY primary key (PhotoGalleryId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('PhotoGallery') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'PhotoGallery' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '相册表', 
   'user', @CurrentUser, 'table', 'PhotoGallery'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PhotoGallery')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PhotoGallery', 'column', 'UserId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主要为邮箱',
   'user', @CurrentUser, 'table', 'PhotoGallery', 'column', 'UserId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PhotoGallery')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Name')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PhotoGallery', 'column', 'Name'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '相册名称',
   'user', @CurrentUser, 'table', 'PhotoGallery', 'column', 'Name'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('PhotoGallery')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'PhotoGallery', 'column', 'DateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '建立相册时间',
   'user', @CurrentUser, 'table', 'PhotoGallery', 'column', 'DateTime'
go

/*==============================================================*/
/* Table: Privilege                                             */
/*==============================================================*/
create table Privilege (
   PrivilegeId          int                  identity,
   PrivilegeMaster      smallint             not null,
   PrivilegeMasterValue varchar(100)         not null,
   PrivilegeAccess      smallint             not null,
   PrivilegeAccessValue int                  not null,
   PrivilegeOperation   smallint             not null,
   constraint PK_PRIVILEGE primary key (PrivilegeId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Privilege') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Privilege' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '权限表记录表', 
   'user', @CurrentUser, 'table', 'Privilege'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Privilege')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PrivilegeMaster')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Privilege', 'column', 'PrivilegeMaster'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分配给用户或角色:0用户   1角色',
   'user', @CurrentUser, 'table', 'Privilege', 'column', 'PrivilegeMaster'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Privilege')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PrivilegeMasterValue')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Privilege', 'column', 'PrivilegeMasterValue'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'UserID或RoleID',
   'user', @CurrentUser, 'table', 'Privilege', 'column', 'PrivilegeMasterValue'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Privilege')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PrivilegeAccess')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Privilege', 'column', 'PrivilegeAccess'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是菜单或按钮:0菜单  1按钮',
   'user', @CurrentUser, 'table', 'Privilege', 'column', 'PrivilegeAccess'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Privilege')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PrivilegeAccessValue')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Privilege', 'column', 'PrivilegeAccessValue'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'MenuId或buttonId',
   'user', @CurrentUser, 'table', 'Privilege', 'column', 'PrivilegeAccessValue'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Privilege')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PrivilegeOperation')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Privilege', 'column', 'PrivilegeOperation'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1可用   0禁用',
   'user', @CurrentUser, 'table', 'Privilege', 'column', 'PrivilegeOperation'
go

/*==============================================================*/
/* Table: Role                                                  */
/*==============================================================*/
create table Role (
   RoleId               varchar(20)          not null,
   Name                 nvarchar(50)         not null,
   "Desc"               nvarchar(200)        null,
   DateTime             datetime             not null,
   constraint PK_ROLE primary key (RoleId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Role') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Role' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '角色表', 
   'user', @CurrentUser, 'table', 'Role'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Name')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Role', 'column', 'Name'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色名称',
   'user', @CurrentUser, 'table', 'Role', 'column', 'Name'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Role')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Desc')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'Role', 'column', 'Desc'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色描述',
   'user', @CurrentUser, 'table', 'Role', 'column', 'Desc'
go

/*==============================================================*/
/* Table: ScanOrSupport                                         */
/*==============================================================*/
create table ScanOrSupport (
   ScanOrSupportId      int                  identity,
   PhotoId              int                  not null,
   UserId               varchar(100)         not null,
   Type                 smallint             not null,
   DateTime             datetime             not null,
   constraint PK_SCANORSUPPORT primary key (ScanOrSupportId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('ScanOrSupport') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'ScanOrSupport' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '浏览跟点赞表', 
   'user', @CurrentUser, 'table', 'ScanOrSupport'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ScanOrSupport')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'PhotoId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ScanOrSupport', 'column', 'PhotoId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '照片的Id',
   'user', @CurrentUser, 'table', 'ScanOrSupport', 'column', 'PhotoId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ScanOrSupport')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ScanOrSupport', 'column', 'UserId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主要为邮箱',
   'user', @CurrentUser, 'table', 'ScanOrSupport', 'column', 'UserId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('ScanOrSupport')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Type')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'ScanOrSupport', 'column', 'Type'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1scan   2support',
   'user', @CurrentUser, 'table', 'ScanOrSupport', 'column', 'Type'
go

/*==============================================================*/
/* Table: UserInfo                                              */
/*==============================================================*/
create table UserInfo (
   UserId               varchar(100)         not null,
   Pwd                  varchar(100)         not null,
   Name                 nvarchar(50)         null,
   Status               char(10)             null,
   HeadUrl              varchar(100)         null,
   DateTime             datetime             not null,
   constraint PK_USERINFO primary key (UserId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('UserInfo') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'UserInfo' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '用户信息表', 
   'user', @CurrentUser, 'table', 'UserInfo'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'UserInfo', 'column', 'UserId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主要为邮箱',
   'user', @CurrentUser, 'table', 'UserInfo', 'column', 'UserId'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Pwd')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'UserInfo', 'column', 'Pwd'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '加密后的密码',
   'user', @CurrentUser, 'table', 'UserInfo', 'column', 'Pwd'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Name')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'UserInfo', 'column', 'Name'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户名',
   'user', @CurrentUser, 'table', 'UserInfo', 'column', 'Name'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Status')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'UserInfo', 'column', 'Status'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户状态：0没有通过   1验证通过',
   'user', @CurrentUser, 'table', 'UserInfo', 'column', 'Status'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'HeadUrl')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'UserInfo', 'column', 'HeadUrl'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '头像地址',
   'user', @CurrentUser, 'table', 'UserInfo', 'column', 'HeadUrl'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('UserInfo')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'DateTime')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'UserInfo', 'column', 'DateTime'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '注册时间',
   'user', @CurrentUser, 'table', 'UserInfo', 'column', 'DateTime'
go

/*==============================================================*/
/* Table: UserRole                                              */
/*==============================================================*/
create table UserRole (
   UserRoleId           int                  identity,
   UserId               varchar(100)         null,
   RoleId               varchar(20)          null,
   DateTime             datetime             not null,
   constraint PK_USERROLE primary key (UserRoleId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('UserRole') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'UserRole' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '用户角色表', 
   'user', @CurrentUser, 'table', 'UserRole'
go

if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('UserRole')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'UserId')
)
begin
   declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_dropextendedproperty 'MS_Description', 
   'user', @CurrentUser, 'table', 'UserRole', 'column', 'UserId'

end


select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主要为邮箱',
   'user', @CurrentUser, 'table', 'UserRole', 'column', 'UserId'
go

alter table Comment
   add constraint FK_COMMENT_REFERENCE_PHOTO foreign key (PhotoId)
      references Photo (PhotoId)
go

alter table Comment
   add constraint FK_COMMENT_REFERENCE_USERINFO foreign key (UserId)
      references UserInfo (UserId)
go

alter table "Like"
   add constraint FK_LIKE_REFERENCE_PHOTO foreign key (PhotoId)
      references Photo (PhotoId)
go

alter table "Like"
   add constraint FK_LIKE_REFERENCE_USERINFO foreign key (UserId)
      references UserInfo (UserId)
go

alter table Photo
   add constraint FK_PHOTO_REFERENCE_USERINFO foreign key (UserId)
      references UserInfo (UserId)
go

alter table Photo
   add constraint FK_PHOTO_REFERENCE_LOCATION foreign key (LocationId)
      references Location (LocationId)
go

alter table Photo
   add constraint FK_PHOTO_REFERENCE_PHOTOCAT foreign key (PhotoCategoryId)
      references PhotoCategory (PhotoCategoryId)
go

alter table Photo
   add constraint FK_PHOTO_REFERENCE_PHOTOGAL foreign key (PhotoGalleryId)
      references PhotoGallery (PhotoGalleryId)
go

alter table PhotoGallery
   add constraint FK_PHOTOGAL_REFERENCE_USERINFO foreign key (UserId)
      references UserInfo (UserId)
go

alter table ScanOrSupport
   add constraint FK_SCANORSU_REFERENCE_PHOTO foreign key (PhotoId)
      references Photo (PhotoId)
go

alter table ScanOrSupport
   add constraint FK_SCANORSU_REFERENCE_USERINFO foreign key (UserId)
      references UserInfo (UserId)
go

alter table UserRole
   add constraint FK_USERROLE_REFERENCE_USERINFO foreign key (UserId)
      references UserInfo (UserId)
go

alter table UserRole
   add constraint FK_USERROLE_REFERENCE_ROLE foreign key (RoleId)
      references Role (RoleId)
go

