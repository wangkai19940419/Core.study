using System;
using System.Collections.Generic;

namespace Wk.Study.Model.Models
{
    public partial class SysAction
    {
        public int ActionId { get; set; }
        public string ActionName { get; set; }
        public string ActionTag { get; set; }
        public string ActionDescription { get; set; }
        public int ActionOrder { get; set; }
    }
}
