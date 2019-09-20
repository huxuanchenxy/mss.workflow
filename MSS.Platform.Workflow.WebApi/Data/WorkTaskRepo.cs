﻿using Dapper;
using MSS.Platform.Workflow.WebApi.Data;
using MSS.Platform.Workflow.WebApi.Model;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace MSS.Platform.Workflow.WebApi.Data
{

    public interface IWorkTaskRepo<T> where T : BaseEntity
    {
        Task<WorkTaskPageView> GetPageList(WorkTaskQueryParm param);
        Task<WorkTaskPageView> GetPageMyApply(WorkTaskQueryParm parm);
        Task<WorkTaskPageView> GetPageActivityInstance(WorkQueryParm parm);
    }

    public class WorkTaskRepo : BaseRepo, IWorkTaskRepo<TaskViewModel>
    {
        public WorkTaskRepo(DapperOptions options) : base(options) { }

        public async Task<WorkTaskPageView> GetPageList(WorkTaskQueryParm parm)
        {
            return await WithConnection(async c =>
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
                if (parm.AppName != null)
                {
                    whereSql.Append(" and t.AppName like '%" + parm.AppName.Trim() + "%'");
                }

                sql.Append(whereSql);


                var data = await c.QueryAsync<TaskViewModel>(sql.ToString());
                var total = data.ToList().Count;
                sql.Append(" order by " + parm.sort + " " + parm.order)
                .Append(" limit " + (parm.page - 1) * parm.rows + "," + parm.rows);
                var ets = await c.QueryAsync<TaskViewModel>(sql.ToString());

                WorkTaskPageView ret = new WorkTaskPageView();
                ret.rows = ets.ToList();
                ret.total = total;
                return ret;
            });
        }

        /// <summary>
        /// 我的申请
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<WorkTaskPageView> GetPageMyApply(WorkTaskQueryParm parm)
        {
            return await WithConnection(async c =>
            {

                StringBuilder sql = new StringBuilder();
                sql.Append($@"  SELECT pi.ID,
		                                ai.AppName,
		                                ai.ProcessGUID,
		                                pi.Version,
		                                ai.ActivityGUID,
		                                ai.ActivityName,
		                                ai.ActivityType,
		                                ai.WorkItemType,
		                                ai.ActivityState,
		                                pi.ProcessState,
		                                ai.ComplexType,
		                                ai.MIHostActivityInstanceID,
		                                pi.AppInstanceCode,
		                                pi.ProcessName,
		                                pi.CreatedByUserID,
		                                pi.CreatedByUserName,
		                                pi.CreatedDateTime
                                FROM
		                                WfActivityInstance ai
                                INNER JOIN
		                                WfProcessInstance pi ON ai.ProcessInstanceID = pi.ID ");
                StringBuilder whereSql = new StringBuilder();
                whereSql.Append(" WHERE ai.ActivityType = '1' AND pi.CreatedByUserID = '" + parm.AssignedToUserID + "' ");

                if (parm.AppName != null)
                {
                    whereSql.Append(" and ai.AppName like '%" + parm.AppName.Trim() + "%'");
                }

                sql.Append(whereSql);


                var data = await c.QueryAsync<TaskViewModel>(sql.ToString());
                var total = data.ToList().Count;
                sql.Append(" order by " + parm.sort + " " + parm.order)
                .Append(" limit " + (parm.page - 1) * parm.rows + "," + parm.rows);
                var ets = await c.QueryAsync<TaskViewModel>(sql.ToString());

                WorkTaskPageView ret = new WorkTaskPageView();
                ret.rows = ets.ToList();
                ret.total = total;
                return ret;
            });
        }

        public async Task<WorkTaskPageView> GetPageActivityInstance(WorkQueryParm parm)
        {
            return await WithConnection(async c =>
            {

                StringBuilder sql = new StringBuilder();
                sql.Append($@"  SELECT pi.ID,
		                                ai.AppName,
		                                ai.ProcessGUID,
		                                pi.Version,
		                                ai.ActivityGUID,
		                                ai.ActivityName,
		                                ai.ActivityType,
		                                ai.WorkItemType,
		                                ai.ActivityState,
		                                pi.ProcessState,
		                                ai.ComplexType,
		                                ai.MIHostActivityInstanceID,
		                                pi.AppInstanceCode,
		                                pi.ProcessName,
		                                pi.CreatedByUserID,
		                                pi.CreatedByUserName,
		                                pi.CreatedDateTime
                                FROM
		                                WfActivityInstance ai
                                INNER JOIN
		                                WfProcessInstance pi ON ai.ProcessInstanceID = pi.ID ");
                StringBuilder whereSql = new StringBuilder();
                whereSql.Append(" WHERE ai.ProcessInstanceID = '" + parm.ProcessInstanceID + "'");

                //if (parm.AppName != null)
                //{
                //    whereSql.Append(" and ai.AppName like '%" + parm.AppName.Trim() + "%'");
                //}
                //if (parm.ProcessInstanceID != null)
                //{
                //    whereSql.Append(" and ai.ProcessInstanceID = '" + parm.ProcessInstanceID + "'");
                //}

                sql.Append(whereSql);

                //验证是否有参与到流程中
                string sqlcheck = sql.ToString();
                sqlcheck += ("AND ai.CreatedByUserID = '" + parm.UserID + "'");
                var checkdata = await c.QueryFirstOrDefaultAsync<TaskViewModel>(sqlcheck);
                if (checkdata == null)
                {
                    return null;
                }

                var data = await c.QueryAsync<TaskViewModel>(sql.ToString());
                var total = data.ToList().Count;
                sql.Append(" order by " + parm.sort + " " + parm.order)
                .Append(" limit " + (parm.page - 1) * parm.rows + "," + parm.rows);
                var ets = await c.QueryAsync<TaskViewModel>(sql.ToString());

                WorkTaskPageView ret = new WorkTaskPageView();
                ret.rows = ets.ToList();
                ret.total = total;
                return ret;
            });
        }
    }

}
