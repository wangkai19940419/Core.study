using System;
using System.Collections.Generic;

namespace Wk.Study.Model.Models
{
    public partial class SysRole
    {
        public int RoleId { get; set; }
        public int RoleGroupId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public int RoleOrder { get; set; }
    }
}
