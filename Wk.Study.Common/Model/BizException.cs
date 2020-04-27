using System;
using System.Collections.Generic;
using System.Text;

namespace Wk.Study.Common.Model
{
    public class BizException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bizMsg"></param>
        /// <param name="hiddenMsg"></param>
        public BizException(string bizMsg, string hiddenMsg = "") : this(0, bizMsg, hiddenMsg)
        {
        }

        public BizException(int bizErrorId, string bizMsg) : this(bizErrorId, bizMsg, "")
        {
        }

        public BizException(int bizErrorId, string bizMsg, string hiddenMsg)
        {
            this.BizErrorId = bizErrorId;
            this.BizMsg = bizMsg;
            this.HiddenMsg = hiddenMsg;
        }

        /// <summary>
        /// 
        /// </summary>
        public string BizMsg { get; }

        /// <summary>
        /// 
        /// </summary>
        public int BizErrorId { get; set; }

        public string HiddenMsg { get; set; }
    }
}
