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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETBPMFlow.Engine.Core.Result
{
    /// <summary>
    /// 活动节点执行结果
    /// </summary>
    public class WfNodeMediatedResult : WfExecutedResult
    {
        /// <summary>
        /// 异常类型
        /// </summary>
        public WfNodeMediatedFeedback Feedback { get; set; }

        /// <summary>
        /// 创建NodeMediatedResult 对象
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns></returns>
        public static WfNodeMediatedResult CreateNodeMediatedResult(WfNodeMediatedFeedback feedback)
        {
            var mediatedResult = new WfNodeMediatedResult();
            mediatedResult.Feedback = feedback;

            return mediatedResult;
        }

    }

    /// <summary>
    /// 执行反馈枚举
    /// </summary>
    public enum WfNodeMediatedFeedback
    {
        /// <summary>
        /// 串行会(加)签，设置下一个执行节点的任务进入运行状态
        /// </summary>
        ForwardToNextSequenceTask = 1,

        /// <summary>
        /// 并行会(加)签，等待节点到达足够多的完成比例
        /// </summary>
        WaitingForCompletedMore = 2,

        /// <summary>
        /// 并行汇合需要其它分支
        /// </summary>
        NeedOtherGatewayBranchesToJoin = 3,

        /// <summary>
        /// XOR场景，第一个满足条件的分支完成，其后的被阻止
        /// </summary>
        XOrFirstHasBeenFinishedOthersBlocked = 4,

        /// <summary>
        /// 其它未知因素需调试
        /// </summary>
        OtherUnknownReasonToDebug = 5,

        /// <summary>
        /// 未知类型的节点需监视
        /// </summary>
        UnknownNodeTypeToWatch = 6
    }
}
