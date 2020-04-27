using System;
using System.Collections.Generic;

namespace Wk.Study.Model.Models
{
    public partial class SysUserGroup
    {
        public int UgId { get; set; }
        public string UgName { get; set; }
        public int UgOrder { get; set; }
        public string UgDescription { get; set; }
        public int UgDepth { get; set; }
        public int UgSuperiorId { get; set; }
        public int UgCount { get; set; }
    }
}
