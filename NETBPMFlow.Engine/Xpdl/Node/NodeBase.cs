using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NETBPMFlow.Data;
using NETBPMFlow.Engine.Utility;
using NETBPMFlow.Engine.Common;
using NETBPMFlow.Engine.Business.Entity;

namespace NETBPMFlow.Engine.Xpdl.Node
{
    /// <summary>
    /// 节点的基类
    /// </summary>
    public abstract class NodeBase
    {
        #region 属性和构造函数
        /// <summary>
        /// 节点定义属性
        /// </summary>
        public ActivityEntity Activity
        {
            get;
            set;
        }

        /// <summary>
        /// 节点实例
        /// </summary>
        public ActivityInstanceEntity ActivityInstance
        {
            get;
            set;
        }
        

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentActivity"></param>
        public NodeBase(ActivityEntity currentActivity)
        {
            Activity = currentActivity;
        }
        #endregion
    }
}
