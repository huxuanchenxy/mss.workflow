using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NETBPMFlow.Data;
using NETBPMFlow.Engine.Common;
using NETBPMFlow.Engine.Business.Entity;

namespace NETBPMFlow.Engine.Xpdl.Node
{
    /// <summary>
    /// 多实例节点类型
    /// </summary>
    internal class MultipleInstanceNode : NodeBase
    {
        internal MultipleInstanceNode(ActivityEntity activity) :
            base(activity)
        {

        }
    }
}
