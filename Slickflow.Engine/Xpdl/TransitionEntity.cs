﻿/*
The Slickflow project.
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
using Slickflow.Engine.Common;

namespace Slickflow.Engine.Xpdl
{
    /// <summary>
    /// 转移定义
    /// </summary>
    public class TransitionEntity
    {
        /// <summary>
        /// 转移GUID
        /// </summary>
        public String TransitionGUID
        {
            get;
            set;
        }

        /// <summary>
        /// 起始活动GUID
        /// </summary>
        public String FromActivityGUID
        {
            get;
            set;
        }

        /// <summary>
        /// 到达活动GUID
        /// </summary>
        public String ToActivityGUID
        {
            get;
            set;
        }

        /// <summary>
        /// 方向类型
        /// </summary>
        public TransitionDirectionTypeEnum DirectionType
        {
            get;
            set;
        }

        /// <summary>
        /// 接收者类型
        /// </summary>
        public Receiver Receiver
        {
            get;
            set;
        }

        /// <summary>
        /// 条件
        /// </summary>
        public ConditionEntity Condition
        {
            get;
            set;
        }

        /// <summary>
        /// 群体行为类型
        /// </summary>
        public GroupBehaviourEntity GroupBehaviour
        {
            get;
            set;
        }

        /// <summary>
        /// 起始活动
        /// </summary>
        public ActivityEntity FromActivity
        {
            get;
            set;
        }

        /// <summary>
        /// 到达活动
        /// </summary>
        public ActivityEntity ToActivity
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 转移列表类
    /// </summary>
    public class TransitonList : List<TransitionEntity>
    {

    }
}
