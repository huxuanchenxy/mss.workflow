/*
* Slickflow 工作流引擎遵循LGPL协议，也可联系作者商业授权并获取技术支持；
* 除此之外的使用则视为不正当使用，请您务必避免由此带来的商业版权纠纷。
*  
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
using System.Data;
using Microsoft.AspNetCore.Mvc;
using SlickOne.WebUtility;
using Slickflow.Engine.Common;
using Slickflow.Engine.Core.Result;
using Slickflow.Engine.Business.Data;
using Slickflow.Engine.Business.Entity;
using Slickflow.Engine.Service;

namespace Slickflow.WebApi.Controllers
{
    //[Route("api/v1/[controller]")]
    //[ApiController]
    public class WfProcessController : Controller
    {
        #region Workflow Api访问操作
        /// <summary>
        ///  启动流程测试
        /// </summary>
        /// <param name="runner">运行者</param>
        /// <returns>执行结果</returns>
        [HttpPost]//pass
        public ResponseResult StartProcess([FromBody] WfAppRunner runner)
        {
            using (var session = DbFactory.CreateSession())
            {
                var transaction = session.DbContext.Database.BeginTransaction();
                var wfService = new WorkflowService();
                var result = wfService.StartProcess(runner, session);

                if (result.Status == WfExecutedStatus.Success)
                {
                    transaction.Commit();
                    return ResponseResult.Success();
                }
                else
                {
                    transaction.Rollback();
                    return ResponseResult.Error(result.Message);
                }
            }
        }


        /// <summary>
        ///  运行流程测试
        /// </summary>
        /// <param name="runner">运行者</param>
        /// <returns>执行结果</returns>
        [HttpPost]//pass
        public ResponseResult RunProcessApp([FromBody] WfAppRunner runner)
        {
            using (var session = DbFactory.CreateSession())
            {
                var transaction = session.DbContext.Database.BeginTransaction();
                var wfService = new WorkflowService();
                var result = wfService.RunProcessApp(runner, session);

                if (result.Status == WfExecutedStatus.Success)
                {
                    transaction.Commit();
                    return ResponseResult.Success();
                }
                else
                {
                    transaction.Rollback();
                    return ResponseResult.Error(result.Message);
                }
            }
        }

        /// <summary>
        ///  撤销流程测试
        /// </summary>
        /// <param name="runner">运行者</param>
        /// <returns>执行结果</returns>
        [HttpPost]//pass
        public ResponseResult WithdrawProcess([FromBody] WfAppRunner runner)
        {
            using (var session = DbFactory.CreateSession())
            {
                var transaction = session.DbContext.Database.BeginTransaction();
                var wfService = new WorkflowService();
                var result = wfService.WithdrawProcess(runner, session);

                if (result.Status == WfExecutedStatus.Success)
                {
                    transaction.Commit();
                    return ResponseResult.Success();
                }
                else
                {
                    transaction.Rollback();
                    return ResponseResult.Error(result.Message);
                }
            }
        }

        /// <summary>
        ///  退回流程测试
        /// </summary>
        /// <param name="runner">运行者</param>
        /// <returns>执行结果</returns>
        [HttpPost]//pass(不能退回到开始节点)
        public ResponseResult SendBackProcess([FromBody] WfAppRunner runner)
        {
            using (var session = DbFactory.CreateSession())
            {
                var transaction = session.DbContext.Database.BeginTransaction();
                var wfService = new WorkflowService();
                var result = wfService.SendBackProcess(runner, session);

                if (result.Status == WfExecutedStatus.Success)
                {
                    transaction.Commit();
                    return ResponseResult.Success();
                }
                else
                {
                    transaction.Rollback();
                    return ResponseResult.Error(result.Message);
                }
            }
        }

        /// <summary>
        ///  返签流程测试
        /// </summary>
        /// <param name="runner">运行者</param>
        /// <returns>执行结果</returns>
        [HttpPost]
        public ResponseResult ReverseProcess([FromBody] WfAppRunner runner)
        {
            using (var session = DbFactory.CreateSession())
            {
                var transaction = session.DbContext.Database.BeginTransaction();
                var wfService = new WorkflowService();
                var result = wfService.ReverseProcess(runner, session);

                if (result.Status == WfExecutedStatus.Success)
                {
                    transaction.Commit();
                    return ResponseResult.Success();
                }
                else
                {
                    transaction.Rollback();
                    return ResponseResult.Error(result.Message);
                }
            }
        }
        #endregion

        #region 获取流程数据列表
        /// <summary>
        /// 获取流程记录列表
        /// </summary>
        /// <returns>流程列表</returns>
        [HttpGet]
        public ResponseResult<List<ProcessEntity>> GetProcessListSimple()
        {
            var result = ResponseResult<List<ProcessEntity>>.Default();
            try
            {
                var wfService = new WorkflowService();
                var entity = wfService.GetProcessListSimple().ToList();

                result = ResponseResult<List<ProcessEntity>>.Success(entity);
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<ProcessEntity>>.Error(
                    string.Format("获取流程基本信息失败！{0}", ex.Message)
                );
            }
            return result;
        }
        #endregion



        [HttpPost]//pass
        public ResponseResult CancelProcess([FromBody] WfAppRunner runner)
        {
            using (var session = DbFactory.CreateSession())
            {
                var wfService = new WorkflowService();
                try
                {
                    var result = wfService.CancelProcess(runner.ProcessInstanceID, runner);
                    return ResponseResult.Success();
                }
                catch (Exception ex)
                {
                    return ResponseResult.Error(ex.Message);
                }
                
            }
        }

        /// <summary>
        /// 查询流程下一步信息的节点角色人员树
        /// </summary>
        /// <param name="runner">当前执行人</param>
        /// <returns>流程下一步信息</returns>
        [HttpPost]//pass
        public ResponseResult<List<NodeView>> GetNextStepRoleUserTree([FromBody] WfAppRunner runner)
        {
            var result = ResponseResult<List<NodeView>>.Default();
            try
            {
                var wfservice = new WorkflowService();
                //var nodeViewList = wfservice.GetNextActivityRoleUserTree(runner).ToList<NodeView>();
                var nodeViewList = wfservice.GetNextActivityRoleUserTree(runner, runner.Conditions).ToList<NodeView>();
                result = ResponseResult<List<NodeView>>.Success(nodeViewList, "获取流程下一步信息成功!");
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<NodeView>>.Error(string.Format(
                    " 请确认角色身份是否切换?! {0}",
                    ex.Message));
            }
            return result;
        }
        [HttpPost]//pass
        public ResponseResult<List<TaskViewEntity>> QueryReadyTasks([FromBody] TaskQuery query)
        {
            var result = ResponseResult<List<TaskViewEntity>>.Default();
            try
            {
                var taskList = new List<TaskViewEntity>();
                var wfService = new WorkflowService();
                var itemList = wfService.GetReadyTasks(query);

                if (itemList != null)
                {
                    taskList = itemList.ToList();
                }
                result = ResponseResult<List<TaskViewEntity>>.Success(taskList);
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<TaskViewEntity>>.Error(string.Format(
                    "获取当前用户待办任务数据失败, 异常信息:{0}",
                    ex.Message));
            }
            return result;
        }

        [HttpPost]//pass查询当前到哪里个节点(不包括结束)
        public ResponseResult<List<ActivityInstanceEntity>> QueryReadyActivityInstance([FromBody] TaskQuery query)
        {
            var result = ResponseResult<List<ActivityInstanceEntity>>.Default();
            try
            {
                var wfService = new WorkflowService();
                var itemList = wfService.GetRunningActivityInstance(query).ToList();


                result = ResponseResult<List<ActivityInstanceEntity>>.Success(itemList);
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<ActivityInstanceEntity>>.Error(string.Format(
                    "获取待办任务数据失败, 异常信息:{0}",
                    ex.Message));
            }
            return result;
        }

        [HttpPost]//pass
        public ResponseResult<List<TaskViewEntity>> QueryCompletedTasks([FromBody] TaskQuery query)
        {
            var result = ResponseResult<List<TaskViewEntity>>.Default();
            try
            {
                var taskList = new List<TaskViewEntity>();
                var wfService = new WorkflowService();
                var itemList = wfService.GetCompletedTasks(query);

                if (itemList != null)
                {
                    taskList = itemList.ToList();
                }
                result = ResponseResult<List<TaskViewEntity>>.Success(taskList);
            }
            catch (System.Exception ex)
            {
                result = ResponseResult<List<TaskViewEntity>>.Error(string.Format(
                    "获取已办任务数据失败, 异常信息:{0}",
                    ex.Message));
            }
            return result;
        }
    }
}
