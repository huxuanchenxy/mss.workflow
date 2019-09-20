using MSS.API.Common;
using MSS.API.Common.Utility;
using MSS.Platform.Workflow.WebApi.Data;
using MSS.Platform.Workflow.WebApi.Model;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MSS.Platform.Workflow.WebApi.Service
{
    public class WorkTaskService : IWorkTaskService
    {
        private readonly IWorkTaskRepo<TaskViewModel> _repo;
        private readonly IAuthHelper _authhelper;
        public WorkTaskService(IWorkTaskRepo<TaskViewModel> repo, IAuthHelper authhelper)
        {
            _repo = repo;
            _authhelper = authhelper;
        }

        /// <summary>
        /// 我的待办
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetReadyTasks(WorkTaskQueryParm parm)
        {
            ApiResult ret = new ApiResult();

            try
            {
                parm.ActivityState = 1;
                parm.AssignedToUserID = _authhelper.GetUserId();
                //parm.AssignedToUserID = 40;
                var data = await _repo.GetPageList(parm);
                ret.code = Code.Success;
                ret.data = data;
            }
            catch (Exception ex)
            {
                ret.code = Code.Failure;
                ret.msg = ex.Message;
            }

            return ret;
        }

        /// <summary>
        /// 我的申请
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetPageMyApply(WorkTaskQueryParm parm)
        {
            ApiResult ret = new ApiResult();

            try
            {
                parm.AssignedToUserID = _authhelper.GetUserId();
                //parm.AssignedToUserID = 40;
                var data = await _repo.GetPageMyApply(parm);
                ret.code = Code.Success;
                ret.data = data;
            }
            catch (Exception ex)
            {
                ret.code = Code.Failure;
                ret.msg = ex.Message;
            }

            return ret;
        }

        /// <summary>
        /// 流转日志
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetPageActivityInstance(WorkQueryParm parm)
        {
            ApiResult ret = new ApiResult();

            try
            {
                parm.UserID = _authhelper.GetUserId();
                //parm.UserID = 40;
                var data = await _repo.GetPageActivityInstance(parm);
                ret.code = Code.Success;
                ret.data = data;
            }
            catch (Exception ex)
            {
                ret.code = Code.Failure;
                ret.msg = ex.Message;
            }

            return ret;
        }





    }

    public interface IWorkTaskService
    {
        Task<ApiResult> GetReadyTasks(WorkTaskQueryParm parm);
        Task<ApiResult> GetPageMyApply(WorkTaskQueryParm parm);
        Task<ApiResult> GetPageActivityInstance(WorkQueryParm parm);
    }


}
