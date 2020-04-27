using System;
using System.Collections.Generic;

namespace Wk.Study.Model.Models
{
    public partial class SysMenu
    {
        public int MenuId { get; set; }
        public int MenuTypeId { get; set; }
        public string MenuName { get; set; }
        public string MenuTag { get; set; }
        public string MenuUrl { get; set; }
        public bool? MenuDisabled { get; set; }
        public int MenuOrder { get; set; }
        public string MenuDescription { get; set; }
        public bool? IsMenu { get; set; }
    }
}
