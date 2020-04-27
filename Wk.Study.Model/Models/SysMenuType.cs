using System;
using System.Collections.Generic;

namespace Wk.Study.Model.Models
{
    public partial class SysMenuType
    {
        public int MenuTypeId { get; set; }
        public string MenuTypeName { get; set; }
        public int MenuTypeOrder { get; set; }
        public string MenuTypeDescription { get; set; }
        public int MenuTypeDepth { get; set; }
        public int MenuTypeSuperiorId { get; set; }
        public int MenuTypeCount { get; set; }
    }
}
