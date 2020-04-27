using System;
using System.Collections.Generic;

namespace Wk.Study.Model.Models
{
    public partial class SysUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int RoleId { get; set; }
        public int UserGroup { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public int Status { get; set; }
        public bool IsOnline { get; set; }
        public bool IsLimit { get; set; }
    }
}
