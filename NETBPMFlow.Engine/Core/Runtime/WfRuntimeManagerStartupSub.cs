﻿/*
* NETBPMFlow 工作流引擎遵循LGPL协议，也可联系作者商业授权并获取技术支持；
* 除此之外的使用则视为不正当使用，请您务必避免由此带来的商业版权纠纷。
* 
The NETBPMFlow project.
Copyright (C) 2014  .NET Workflow Engine Library

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, you can access the official
web page about lgpl: https://www.gnu.org/licenses/lgpl.html
*/

using System;
using System.Threading;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NETBPMFlow.Data;
using NETBPMFlow.Engine.Common;
using NETBPMFlow.Engine.Utility;
using NETBPMFlow.Engine.Core.Result;
using NETBPMFlow.Engine.Business.Entity;
using NETBPMFlow.Engine.Business.Manager;
using NETBPMFlow.Engine.Xpdl;
using NETBPMFlow.Engine.Core.Event;
using NETBPMFlow.Engine.Core.Pattern;

namespace NETBPMFlow.Engine.Core.Runtime
{
    /// <summary>
    /// 流程启动运行时
    /// </summary>
    internal class WfRuntimeManagerStartupSub : WfRuntimeManager
    {
        /// <summary>
        /// 启动执行方法
        /// </summary>
        /// <param name="session">会话</param>
        internal override void ExecuteInstanceImp(IDbSession session)
        {
            //构造流程实例
            var processInstance = new ProcessInstanceManager()
                .CreateNewProcessInstanceObject(base.AppRunner, base.ProcessModel.ProcessEntity,
                base.ParentProcessInstance,
                base.InvokedSubProcessNode.ActivityInstance);

            //构造活动实例
            //1. 获取开始节点活动
            var startActivity = base.ProcessModel.GetStartActivity();
            var startExecutionContext = ActivityForwardContext.CreateStartupContext(base.ProcessModel,
                processInstance,
                startActivity,
                base.ActivityResource);

            NodeMediator mediator = NodeMediatorFactory.CreateNodeMediator(startExecutionContext, session);
            mediator.Linker.FromActivityInstance = RunningActivityInstance;
            mediator.ExecuteWorkItem();

            //构造回调函数需要的数据
            WfExecutedResult result = base.WfExecutedResult;
            result.ProcessInstanceIDStarted = processInstance.ID;
            result.Status = WfExecutedStatus.Success;
        }
    }
}
