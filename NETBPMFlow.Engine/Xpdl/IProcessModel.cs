﻿using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Diagnostics;
using NETBPMFlow.Module.Resource;
using NETBPMFlow.Engine.Utility;
using NETBPMFlow.Engine.Common;
using NETBPMFlow.Engine.Business.Entity;
using NETBPMFlow.Engine.Xpdl.Schedule;

namespace NETBPMFlow.Engine.Xpdl
{
    /// <summary>
    /// 流程模型解析
    /// </summary>
    internal interface IProcessModel
    {
        ProcessEntity ProcessEntity { get; set; }
        XmlDocument XmlProcessDefinition { get; }
        ActivityEntity GetFirstActivity();
        ActivityEntity GetStartActivity();
        ActivityEntity GetEndActivity();
        ActivityEntity GetActivity(string activityGUID);
        ActivityEntity GetNextActivity(string activityGUID);
        IList<NodeView> GetNextActivityTree(string currentActivityGUID,
            IDictionary<string, string> condition = null);
        IList<NodeView> GetNextActivityTree(int processInstanceID,
            string currentActivityGUID,
            IDictionary<string, string> condition);

        ActivityEntity GetBackwardGatewayActivity(ActivityEntity gatewayActivity, 
            ref int joinCount, ref int splitCount);
        Int32 GetBackwardTransitionListCount(string activityGUID);
        IList<TransitionEntity> GetForwardTransitionList(string activityGUID);
        Boolean CheckAndSplitOccurrenceCondition(IList<TransitionEntity> transitionList, 
            IDictionary<string, string> conditionValuePair);
        Boolean IsValidTransition(TransitionEntity transition, IDictionary<string, string> conditionValuePair);
        NextActivityMatchedResult GetNextActivityList(string currentActivityGUID, 
            IDictionary<string, string> conditionKeyValuePair = null);
        NextActivityMatchedResult GetNextActivityList(string currentActivityGUID,
            IDictionary<string, string> conditionKeyValuePair,
            ActivityResource activityResource,
            Expression<Func<ActivityResource, ActivityEntity, bool>> expression);
        IList<ActivityEntity> GetTaskActivityList();
        IList<ActivityEntity> GetAllTaskActivityList();

        //资源
        IList<Role> GetRoles();
        IList<Role> GetActivityRoles(string activityGUID);
    }
}
