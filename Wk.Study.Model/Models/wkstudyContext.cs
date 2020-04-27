using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Wk.Study.Model.Models
{
    public partial class wkstudyContext : DbContext
    {
        public wkstudyContext()
        {
        }

        public wkstudyContext(DbContextOptions<wkstudyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SysAction> SysAction { get; set; }
        public virtual DbSet<SysGroup> SysGroup { get; set; }
        public virtual DbSet<SysMenu> SysMenu { get; set; }
        public virtual DbSet<SysMenuAction> SysMenuAction { get; set; }
        public virtual DbSet<SysMenuType> SysMenuType { get; set; }
        public virtual DbSet<SysRole> SysRole { get; set; }
        public virtual DbSet<SysRoleAction> SysRoleAction { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }
        public virtual DbSet<SysUserGroup> SysUserGroup { get; set; }
        public virtual DbSet<SysUserRole> SysUserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-BA6ABH5\\WANGKAI;Database=wkstudy;User Id=sa;Password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysAction>(entity =>
            {
                entity.HasKey(e => e.ActionId)
                    .HasName("PK_tb_Action_1");

                entity.Property(e => e.ActionId)
                    .HasColumnName("ActionID")
                    .HasComment("权限ID");

                entity.Property(e => e.ActionDescription)
                    .HasMaxLength(50)
                    .HasComment("说明");

                entity.Property(e => e.ActionName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasComment("权限名称");

                entity.Property(e => e.ActionOrder).HasComment("排序");

                entity.Property(e => e.ActionTag)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("权限标识");
            });

            modelBuilder.Entity<SysGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("PK_tb_Group");

                entity.Property(e => e.GroupId)
                    .HasColumnName("GroupID")
                    .HasComment("分组ID");

                entity.Property(e => e.GroupDescription)
                    .HasMaxLength(50)
                    .HasComment("说明");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasComment("组名称");

                entity.Property(e => e.GroupOrder).HasComment("排序");

                entity.Property(e => e.GroupType).HasComment("分组类型 用户组0,角色组1");
            });

            modelBuilder.Entity<SysMenu>(entity =>
            {
                entity.HasKey(e => e.MenuId)
                    .HasName("PK_tb_Menu");

                entity.Property(e => e.MenuId)
                    .HasColumnName("MenuID")
                    .HasComment("模块ID");

                entity.Property(e => e.IsMenu)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("是否显示在导航菜单中");

                entity.Property(e => e.MenuDescription)
                    .HasMaxLength(50)
                    .HasComment("说明");

                entity.Property(e => e.MenuDisabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("是否禁用");

                entity.Property(e => e.MenuName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasComment("模块名称");

                entity.Property(e => e.MenuOrder).HasComment("排序");

                entity.Property(e => e.MenuTag)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("模块标识");

                entity.Property(e => e.MenuTypeId)
                    .HasColumnName("MenuTypeID")
                    .HasComment("模块类型");

                entity.Property(e => e.MenuUrl)
                    .HasColumnName("MenuURL")
                    .HasMaxLength(500)
                    .HasComment("模块地址");
            });

            modelBuilder.Entity<SysMenuAction>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("模块权限ID");

                entity.Property(e => e.ActionTag)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("权限标识");

                entity.Property(e => e.MenuId)
                    .HasColumnName("MenuID")
                    .HasComment("模块ID");
            });

            modelBuilder.Entity<SysMenuType>(entity =>
            {
                entity.HasKey(e => e.MenuTypeId)
                    .HasName("PK_tb_MenuType_1");

                entity.Property(e => e.MenuTypeId)
                    .HasColumnName("MenuTypeID")
                    .HasComment("模块分类ID");

                entity.Property(e => e.MenuTypeCount).HasComment("下级个数");

                entity.Property(e => e.MenuTypeDepth).HasComment("深度");

                entity.Property(e => e.MenuTypeDescription)
                    .HasMaxLength(50)
                    .HasComment("说明");

                entity.Property(e => e.MenuTypeName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasComment("模块类型名称");

                entity.Property(e => e.MenuTypeOrder).HasComment("排序");

                entity.Property(e => e.MenuTypeSuperiorId)
                    .HasColumnName("MenuTypeSuperiorID")
                    .HasComment("上级ID");
            });

            modelBuilder.Entity<SysRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK_tb_Role_1");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasComment("角色ID");

                entity.Property(e => e.RoleDescription)
                    .HasMaxLength(50)
                    .HasComment("说明");

                entity.Property(e => e.RoleGroupId)
                    .HasColumnName("RoleGroupID")
                    .HasComment("分组ID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasComment("角色名称");
            });

            modelBuilder.Entity<SysRoleAction>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasComment("编号");

                entity.Property(e => e.ActionTag)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("权限标识");

                entity.Property(e => e.Flag)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasComment("1为允许，0为不禁止");

                entity.Property(e => e.GroupId)
                    .HasColumnName("GroupID")
                    .HasComment("分组ID");

                entity.Property(e => e.MenuId)
                    .HasColumnName("MenuID")
                    .HasComment("模块ID");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasComment("角色ID");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasComment("用户ID");
            });

            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_User_ID");

                entity.HasComment("用户帐户信息表");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasComment("用户ID");

                entity.Property(e => e.Answer)
                    .HasMaxLength(100)
                    .HasComment("重置密码的答案");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasComment("帐户创建时间");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.IsLimit).HasComment("是否受权限限制，0为受限制");

                entity.Property(e => e.IsOnline).HasComment("是否在线");

                entity.Property(e => e.LastLoginTime)
                    .HasColumnType("datetime")
                    .HasComment("上一次登录的时间");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasComment("密码");

                entity.Property(e => e.Question)
                    .HasMaxLength(100)
                    .HasComment("重置密码的问题");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasComment("角色");

                entity.Property(e => e.Status)
                    .HasDefaultValueSql("((1))")
                    .HasComment("用户状态");

                entity.Property(e => e.UserGroup).HasComment("用户组");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasComment("登录名，用户Email");
            });

            modelBuilder.Entity<SysUserGroup>(entity =>
            {
                entity.HasKey(e => e.UgId)
                    .HasName("PK_tb_UserGroup");

                entity.Property(e => e.UgId)
                    .HasColumnName("UG_ID")
                    .HasComment("用户组ID");

                entity.Property(e => e.UgCount)
                    .HasColumnName("UG_Count")
                    .HasComment("用户分组下级数");

                entity.Property(e => e.UgDepth)
                    .HasColumnName("UG_Depth")
                    .HasComment("用户分组深度");

                entity.Property(e => e.UgDescription)
                    .IsRequired()
                    .HasColumnName("UG_Description")
                    .HasMaxLength(50)
                    .HasComment("用户分组描述");

                entity.Property(e => e.UgName)
                    .IsRequired()
                    .HasColumnName("UG_Name")
                    .HasMaxLength(30)
                    .HasComment("用户分组名称");

                entity.Property(e => e.UgOrder)
                    .HasColumnName("UG_Order")
                    .HasComment("用户分组排序");

                entity.Property(e => e.UgSuperiorId)
                    .HasColumnName("UG_SuperiorID")
                    .HasComment("用户分组上级");
            });

            modelBuilder.Entity<SysUserRole>(entity =>
            {
                entity.HasKey(e => e.UrId)
                    .HasName("PK_tb_UserRole");

                entity.Property(e => e.UrId).HasColumnName("UR_ID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
