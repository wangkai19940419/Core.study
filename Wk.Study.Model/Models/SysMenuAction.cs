using System;
using System.Collections.Generic;

namespace Wk.Study.Model.Models
{
    public partial class SysMenuAction
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string ActionTag { get; set; }
    }
}
