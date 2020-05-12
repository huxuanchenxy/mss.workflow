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
using NETBPMFlow.Engine.Xpdl;
using NETBPMFlow.Engine.Business.Entity;
using NETBPMFlow.Engine.Business.Manager;
using NETBPMFlow.Engine.Core.Result;
using NETBPMFlow.Engine.Core.Pattern;

namespace NETBPMFlow.Engine.Core.Runtime
{
    /// <summary>
    /// 任务撤销运行时
    /// </summary>
    internal class WfRuntimeManagerWithdraw : WfRuntimeManager
    {
        /// <summary>
        /// 撤销处理具体功能实现
        /// </summary>
        /// <param name="session">会话</param>
        internal override void ExecuteInstanceImp(IDbSession session)
        {
            //创建新任务节点
            var backMostPreviouslyActivityInstanceID = GetBackwardMostPreviouslyActivityInstanceID();
            var nodeMediatorBackward = new NodeMediatorBackward(base.BackwardContext, session);

            nodeMediatorBackward.CreateBackwardActivityTaskTransitionInstance(base.BackwardContext.ProcessInstance,
                base.BackwardContext.BackwardFromActivityInstance,
                BackwardTypeEnum.Withdrawed,
                backMostPreviouslyActivityInstanceID,
                base.BackwardContext.BackwardToTargetTransitionGUID,
                TransitionTypeEnum.Withdrawed,
                TransitionFlyingTypeEnum.NotFlying,
                base.ActivityResource,
                session);

            //普通节点情况下：更新撤销节点的状态（从准备状态更新为撤销状态）
            var aim = new ActivityInstanceManager();
            aim.Withdraw(base.BackwardContext.BackwardFromActivityInstance.ID,
                base.ActivityResource.AppRunner, session);

            //构造回调函数需要的数据
            WfExecutedResult result = base.WfExecutedResult;
            result.BackwardTaskReceiver = base.BackwardContext.BackwardTaskReceiver;
            result.Status = WfExecutedStatus.Success;
        }
    }
}
