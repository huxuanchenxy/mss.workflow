﻿using System;
using System.Collections.Generic;

namespace NETBPMFlow.Module.Resource.Entity
{
    public class WorkTaskQueryParm : BaseQueryParm
    {
        public int? ActivityState { get; set; }
        public int? AssignedToUserID { get; set; }
    }

    public class WorkTaskPageView
    {
        public List<TaskViewModel> rows { get; set; }
        public int total { get; set; }
    }
    public class TaskViewModel
    {
        public int ID { get; set; }

        public string AppName { get; set; }

        public string AppInstanceID { get; set; }

        public string AppInstanceCode { get; set; }

        public string ProcessName { get; set; }

        public string ProcessGUID { get; set; }

        public string Version { get; set; }

        public int ProcessInstanceID { get; set; }

        public string ActivityGUID { get; set; }

        public int ActivityInstanceID { get; set; }

        public string ActivityName { get; set; }

        public short ActivityType { get; set; }

        public short WorkItemType { get; set; }

        public string PreviousUserID { get; set; }          //上一步审核人ID

        public string PreviousUserName { get; set; }

        public System.DateTime PreviousDateTime { get; set; }

        public short TaskType { get; set; }

        public int EntrustedTaskID { get; set; }        //被委托任务ID

        public string AssignedToUserID { get; set; }

        public string AssignedToUserName { get; set; }

        public System.DateTime CreatedDateTime { get; set; }

        public DateTime LastUpdatedDateTime { get; set; }

        public DateTime EndedDateTime { get; set; }

        public string EndedByUserID { get; set; }

        public string EndedByUserName { get; set; }

        public short TaskState { get; set; }

        public short ActivityState { get; set; }

        public byte RecordStatusInvalid { get; set; }

        public short ProcessState { get; set; }

        public short ComplexType { get; set; }

        public int MIHostActivityInstanceID { get; set; }

        public string PCreatedByUserID { get; set; }

        public string PCreatedByUserName { get; set; }

        public DateTime PCreatdDateTime { get; set; }

        public short MiHostState { get; set; }


    }


}
