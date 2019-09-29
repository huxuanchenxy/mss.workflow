using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NETBPMFlow.Data;
using NETBPMFlow.Engine.Common;
using NETBPMFlow.Engine.Business.Entity;

namespace NETBPMFlow.Engine.Xpdl.Node
{
    /// <summary>
    /// 子流程节点
    /// </summary>
    internal class SubProcessNode : NodeBase
    {
        public string SubProcessGUID { get; set; }

        internal SubProcessNode(ActivityEntity activity) :
            base(activity)
        {

        }
    }
}
