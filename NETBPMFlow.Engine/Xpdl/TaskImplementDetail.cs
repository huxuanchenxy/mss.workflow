using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NETBPMFlow.Engine.Common;

namespace NETBPMFlow.Engine.Xpdl
{
    /// <summary>
    /// 任务实现详细类
    /// </summary>
    public class TaskImplementDetail
    {
        /// <summary>
        /// 任务实现类型定义
        /// </summary>
        public ImplementationTypeEnum ImplementationType
        {
            get;
            set;
        }

        public string Assembly
        {
            get;
            set;
        }

        public string Interface
        {
            get;
            set;
        }

        public string Method
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }
    }
}
