using System;
using System.Collections.Generic;
using System.Text;

namespace Wk.Study.Library.ConfigModel
{
    public class JwtSetting
    {
        public string SecurityKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int ExpireSeconds { get; set; }
    }
}
