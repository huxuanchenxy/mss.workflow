using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using NETBPMFlow.Data;
using NETBPMFlow.Module.Resource.Data;
using NETBPMFlow.Module.Resource.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NETBPMFlow.Module.Resource.Manager
{
    /// <summary>
    /// 角色用户管理类
    /// </summary>
    internal class TaskManager : BaseRepo
    {
        internal WorkTaskPageView GetPageByParm(WorkTaskQueryParm parm)
        {
            return WithConnection(c =>
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($@" SELECT 
                                t.ID,
                                t.AppName,
                                t.AppInstanceID,
                                ai.ProcessGUID,
                                pi.Version,
                                t.ProcessInstanceID,
                                ai.ActivityGUID,
                                t.ActivityInstanceID,
                                ai.ActivityName,
                                ai.ActivityType,
                                ai.WorkItemType,
                                ai.CreatedByUserID,
                                ai.CreatedByUserName,
                                ai.CreatedDateTime,
                                t.TaskType,
                                t.EntrustedTaskID,
                                t.AssignedToUserID,
                                t.AssignedToUserName,
                                t.CreatedDateTime,
                                t.LastUpdatedDateTime,
                                t.EndedDateTime,
                                t.EndedByUserID,
                                t.EndedByUserName,
                                t.TaskState,
                                ai.ActivityState,
                                t.RecordStatusInvalid,
                                pi.ProcessState,
                                ai.ComplexType,
                                ai.MIHostActivityInstanceID,
                                pi.AppInstanceCode,
                                pi.ProcessName,
                                pi.CreatedByUserID,
                                pi.CreatedByUserName,
                                pi.CreatedDateTime,
                                CASE WHEN ai.MIHostActivityInstanceID is null THEN ai.ActivityState ELSE ai1.ActivityState END MiHostState
                            FROM
                                WfActivityInstance ai
                                    INNER JOIN
                                WfTasks t ON ai.ID = t.ActivityInstanceID
                                    INNER JOIN
                                WfProcessInstance pi ON ai.ProcessInstanceID = pi.ID
                                    LEFT JOIN
                                WfActivityInstance ai1 ON ai.MIHostActivityInstanceID = ai1.ID ");
                StringBuilder whereSql = new StringBuilder();
                whereSql.Append(" WHERE pi.ProcessState = '2' AND ( ai.ActivityType = '4' OR ai.WorkItemType = '1' ) AND t.TaskState <> '32' ");
                if (parm.ActivityState != null)
                {
                    whereSql.Append(" and ai.ActivityState =" + parm.ActivityState);
                }
                if (parm.AssignedToUserID != null)
                {
                    whereSql.Append(" and t.AssignedToUserID =" + parm.AssignedToUserID);
                }

                sql.Append(whereSql);
                string sqltmp = $@" SELECT 
                                t.ID,
                                t.AppName FROM WfTasks t ; ";
                try
                {
                    var data = c.Query<TaskViewModel>(sqltmp);
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }

                int total = 0;
                sql.Append(" order by t." + parm.sort + " " + parm.order)
                .Append(" limit " + (parm.page - 1) * parm.rows + "," + parm.rows);
                //List<TaskViewEntity> ets = c.Query<TaskViewEntity>(sql.ToString()).ToList();

                WorkTaskPageView ret = new WorkTaskPageView();
                //ret.rows = ets;
                ret.total = total;
                return ret;
            });
        }
    }
}
