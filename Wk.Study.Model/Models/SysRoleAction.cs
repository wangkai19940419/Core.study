using System;
using System.Collections.Generic;

namespace Wk.Study.Model.Models
{
    public partial class SysRoleAction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int GroupId { get; set; }
        public int MenuId { get; set; }
        public string ActionTag { get; set; }
        public bool? Flag { get; set; }
    }
}
