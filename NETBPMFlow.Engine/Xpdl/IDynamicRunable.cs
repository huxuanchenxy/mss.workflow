﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using NETBPMFlow.Engine.Common;
using NETBPMFlow.Engine.Utility;

namespace NETBPMFlow.Engine.Xpdl
{
    /// <summary>
    /// 动态运行接口
    /// </summary>
    internal interface IDynamicRunable
    {
        /// <summary>
        /// 调用外部方法
        /// </summary>
        /// <param name="implementation"></param>
        /// <param name="userParameters"></param>
        /// <returns></returns>
        object InvokeMethod(TaskImplementDetail implementation, object[] userParameters);
    }
}
