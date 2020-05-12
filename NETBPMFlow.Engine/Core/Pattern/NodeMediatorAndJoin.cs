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
using NETBPMFlow.Engine.Common;
using NETBPMFlow.Data;
using NETBPMFlow.Engine.Business.Entity;
using NETBPMFlow.Engine.Xpdl;
using NETBPMFlow.Engine.Xpdl.Node;

namespace NETBPMFlow.Engine.Core.Pattern
{
    /// <summary>
    /// AndJoin 节点处理类
    /// </summary>
    internal class NodeMediatorAndJoin : NodeMediatorGateway, ICompleteAutomaticlly
    {
        internal NodeMediatorAndJoin(ActivityEntity activity, 
            IProcessModel processModel, 
            IDbSession session)
            : base(activity, processModel, session)
        {

        }

        /// <summary>
        /// 计算汇合Token数目
        /// </summary>
        /// <returns></returns>
        internal int GetTokensRequired()
        {
            int tokensRequired = this.ProcessModel.GetBackwardTransitionListCount(GatewayActivity.ActivityGUID);
            return tokensRequired;
        }

        #region ICompleteAutomaticlly 成员
        /// <summary>
        /// 自动完成
        /// </summary>
        /// <param name="processInstance">流程实例</param>
        /// <param name="transitionGUID">转移GUID</param>
        /// <param name="fromActivityInstance">起始活动实例</param>
        /// <param name="activityResource">活动资源</param>
        /// <param name="session">会话</param>
        /// <returns>网关执行结果</returns>
        public GatewayExecutedResult CompleteAutomaticlly(ProcessInstanceEntity processInstance,
            string transitionGUID,
            ActivityInstanceEntity fromActivityInstance,
            ActivityResource activityResource,
            IDbSession session)
        {
            //检查是否有运行中的合并节点实例
            ActivityInstanceEntity joinNode = base.ActivityInstanceManager.GetActivityRunning(
                processInstance.ID,
                base.GatewayActivity.ActivityGUID,
                session);

            if (joinNode == null)
            {
                var joinActivityInstance = base.CreateActivityInstanceObject(base.GatewayActivity, 
                    processInstance, activityResource.AppRunner);

                //计算总需要的Token数目
                joinActivityInstance.TokensRequired = GetTokensRequired();
                joinActivityInstance.TokensHad = 1;

                //进入运行状态
                joinActivityInstance.ActivityState = (short)ActivityStateEnum.Running;
                joinActivityInstance.GatewayDirectionTypeID = (short)GatewayDirectionEnum.AndJoin;

                base.InsertActivityInstance(joinActivityInstance,
                    session);
				base.InsertTransitionInstance(processInstance,
                    transitionGUID,
                    fromActivityInstance,
                    joinActivityInstance,
                    TransitionTypeEnum.Forward,
                    TransitionFlyingTypeEnum.NotFlying,
                    activityResource.AppRunner,
                    session);
            }
            else
            {
                //更新节点的活动实例属性
                base.GatewayActivityInstance = joinNode;
                int tokensRequired = base.GatewayActivityInstance.TokensRequired;
                int tokensHad = base.GatewayActivityInstance.TokensHad;

                //更新Token数目
                base.ActivityInstanceManager.IncreaseTokensHad(base.GatewayActivityInstance.ID,
                    activityResource.AppRunner,
                    session);
                base.InsertTransitionInstance(processInstance,
                    transitionGUID,
                    fromActivityInstance,
                    joinNode,
                    TransitionTypeEnum.Forward,
                    TransitionFlyingTypeEnum.NotFlying,
                    activityResource.AppRunner,
                    session);
                if ((tokensHad + 1) == tokensRequired)
                {
                    //如果达到完成节点的Token数，则设置该节点状态为完成
                    base.CompleteActivityInstance(base.GatewayActivityInstance.ID,
                        activityResource,
                        session);
                    base.GatewayActivityInstance.ActivityState = (short)ActivityStateEnum.Completed;
                }
            }

            GatewayExecutedResult result = GatewayExecutedResult.CreateGatewayExecutedResult(
                GatewayExecutedStatus.Successed);
            return result;
        }

        #endregion
    }
}
