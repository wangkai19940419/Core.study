using System;
using System.Collections.Generic;

namespace Wk.Study.Model.Models
{
    public partial class SysGroup
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int GroupOrder { get; set; }
        public string GroupDescription { get; set; }
        public int GroupType { get; set; }
    }
}
