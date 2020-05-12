using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using NETBPMFlow.Data;
using NETBPMFlow.Engine.Common;
using NETBPMFlow.Engine.Utility;
using NETBPMFlow.Engine.Xpdl;
using NETBPMFlow.Engine.Xpdl.Node;
using NETBPMFlow.Engine.Business.Entity;
using NETBPMFlow.Engine.Business.Manager;
using NETBPMFlow.Engine.Core.Result;

namespace NETBPMFlow.Engine.Core.Pattern
{
    /// <summary>
    /// 节点连接线
    /// </summary>
    public class Linker
    {
        /// <summary>
        /// 起始节点定义
        /// </summary>
        public ActivityEntity FromActivity { get; set; }
        
        /// <summary>
        /// 起始节点实例
        /// </summary>
        public ActivityInstanceEntity FromActivityInstance { get; set; }

        /// <summary>
        /// 到达节点定义
        /// </summary>
        public ActivityEntity ToActivity { get; set; }

        /// <summary>
        /// 到达节点实例
        /// </summary>
        public ActivityInstanceEntity ToActivityInstance { get; set; } 
    }
}
