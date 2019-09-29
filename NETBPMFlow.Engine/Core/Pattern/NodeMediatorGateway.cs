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
using NETBPMFlow.Data;
using NETBPMFlow.Engine.Common;
using NETBPMFlow.Engine.Business.Entity;
using NETBPMFlow.Engine.Business.Manager;
using NETBPMFlow.Engine.Xpdl;
using NETBPMFlow.Engine.Xpdl.Node;

namespace NETBPMFlow.Engine.Core.Pattern
{
    /// <summary>
    /// 逻辑控制节点执行器
    /// </summary>
    internal class NodeMediatorGateway
    {
        private ActivityEntity _gatewayActivity;
        internal ActivityEntity GatewayActivity
        {
            get { return _gatewayActivity; }
        }

        private IProcessModel _processModel;
        internal IProcessModel ProcessModel
        {
            get { return _processModel; }
        }

        private IDbSession _session;
        internal IDbSession Session
        {
            get { return _session; }
        }

        internal ActivityInstanceEntity GatewayActivityInstance
        {
            get;
            set;
        }

        private ActivityInstanceManager activityInstanceManager;
        internal ActivityInstanceManager ActivityInstanceManager
        {
            get
            {
                if (activityInstanceManager == null)
                {
                    activityInstanceManager = new ActivityInstanceManager();
                }
                return activityInstanceManager;
            }
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="gActivity">网关节点</param>
        /// <param name="processModel">流程模型</param>
        /// <param name="session">数据上下文</param>
        internal NodeMediatorGateway(ActivityEntity gActivity, 
            IProcessModel processModel, 
            IDbSession session)
        {
            _gatewayActivity = gActivity;
            _processModel = processModel;
            _session = session;
        }

        /// <summary>
        /// 创建节点对象
        /// </summary>
        /// <param name="activity">活动</param>
        /// <param name="processInstance">流程实例</param>
        /// <param name="runner">运行者</param>
        protected ActivityInstanceEntity CreateActivityInstanceObject(ActivityEntity activity,
            ProcessInstanceEntity processInstance,
            WfAppRunner runner)
        {
            ActivityInstanceManager aim = new ActivityInstanceManager();
            this.GatewayActivityInstance = aim.CreateActivityInstanceObject(processInstance.AppName,
                processInstance.AppInstanceID,
                processInstance.ID,
                activity,
                runner);

            return this.GatewayActivityInstance;
        }

        /// <summary>
        /// 插入实例数据
        /// </summary>
        /// <param name="activityInstance">活动资源</param>
        /// <param name="session">会话</param>
        internal virtual void InsertActivityInstance(ActivityInstanceEntity activityInstance,
            IDbSession session)
        {
            ActivityInstanceManager.Insert(activityInstance, session);
        }

        /// <summary>
        /// 插入连线实例的方法
        /// </summary>
        /// <param name="processInstance">流程实例</param>
        /// <param name="transitionGUID">转移GUID</param>
        /// <param name="fromActivityInstance">来源活动实例</param>
        /// <param name="toActivityInstance">目的活动实例</param>
        /// <param name="transitionType">转移类型</param>
        /// <param name="flyingType">飞跃类型</param>
        /// <param name="runner">运行者</param>
        /// <param name="session">会话</param>
        /// <returns></returns>
        internal virtual void InsertTransitionInstance(ProcessInstanceEntity processInstance,
            string transitionGUID,
            ActivityInstanceEntity fromActivityInstance,
            ActivityInstanceEntity toActivityInstance,
            TransitionTypeEnum transitionType,
            TransitionFlyingTypeEnum flyingType,
            WfAppRunner runner,
            IDbSession session)
        {
            var tim = new TransitionInstanceManager();
            var transitionInstanceObject = tim.CreateTransitionInstanceObject(processInstance,
                transitionGUID,
                fromActivityInstance,
                toActivityInstance,
                transitionType,
                flyingType,
                runner,
                (byte)ConditionParseResultEnum.Passed);

            tim.Insert( transitionInstanceObject, session);
        }

        /// <summary>
        /// 节点对象的完成方法
        /// </summary>
        /// <param name="ActivityInstanceID">活动实例ID</param>
        /// <param name="activityResource">活动资源</param>
        /// <param name="session">会话</param>
        internal virtual void CompleteActivityInstance(int ActivityInstanceID,
            ActivityResource activityResource,
            IDbSession session)
        {
            //设置完成状态
            ActivityInstanceManager.Complete(ActivityInstanceID,
                activityResource.AppRunner,
                session);
        }
		/// <summary>
        /// 获取分支实例的个数
        /// </summary>
        /// <param name="splitActivityGUID">活动GUID</param>
        /// <param name="processInstanceID">流程实例ID</param>
        /// <returns>实例数目</returns>
        protected int GetInstanceGatewayCount(string splitActivityGUID,
            int processInstanceID)
        {
            ActivityInstanceManager aim = new ActivityInstanceManager();
            return aim.GetInstanceGatewayCount(splitActivityGUID, processInstanceID);
        }
    }
}
