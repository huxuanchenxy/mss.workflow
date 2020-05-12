using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NETBPMFlow.Engine.Common;
using NETBPMFlow.Engine.Core.Result;

namespace NETBPMFlow.Engine.Core.Event
{
    /// <summary>
    /// 工作流Event
    /// </summary>
    public class WfEventArgs : EventArgs
    {
        /// <summary>
        /// 工作项执行结果
        /// </summary>
        public WfExecutedResult WfExecutedResult { get; set; }

        public WfEventArgs(WfExecutedResult result)
            : base()
        {
            WfExecutedResult = result;
        }
    }
}
