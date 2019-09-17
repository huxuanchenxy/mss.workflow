﻿/*
* Slickflow 软件遵循自有项目开源协议，也可联系作者获取企业版商业授权和技术支持；
* 除此之外的使用则视为不正当使用，请您务必避免由此带来的一切商业版权纠纷和损失。
* 
The Slickflow Open License (SfPL 1.0)
Copyright (C) 2014  .NET Workflow Engine Library

1. Slickflow software must be legally used, and should not be used in violation of law, 
   morality and other acts that endanger social interests;
2. Non-transferable, non-transferable and indivisible authorization of this software;
3. The source code can be modified to apply Slickflow components in their own projects 
   or products, but Slickflow source code can not be separately encapsulated for sale or 
   distributed to third-party users;
4. The intellectual property rights of Slickflow software shall be protected by law, and
   no documents such as technical data shall be made public or sold.
5. The enterprise, ultimate and universe version can be provided with commercial license, 
   technical support and upgrade service.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Slickflow.Data;
using Slickflow.Engine.Common;

namespace Slickflow.Engine.Delegate
{
    /// <summary>
    /// 委托接口
    /// </summary>
    public interface IDelegateService
    {
        int ID { get; set; }
        int GetID();
        T GetInstance<T>(int id) where T : class;
    }

    /// <summary>
    /// 委托事件列表
    /// </summary>
    public class DelegateEventList
        : List<KeyValuePair<EventFireTypeEnum, Func<int, string, IDelegateService, Boolean>>>
    {

    }
}
