using Microsoft.AspNetCore.Mvc;
using MSS.API.Common;
using MSS.Platform.Workflow.WebApi.Model;
using MSS.Platform.Workflow.WebApi.Service;
using System.Threading.Tasks;

namespace MSS.Platform.Workflow.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WfController : ControllerBase
    {
        private readonly IWorkTaskService _service;
        public WfController(IWorkTaskService service)
        {
            _service = service;
        }

        [HttpGet("QueryReadyTasks")]
        public async Task<ActionResult<ApiResult>> QueryReadyTasks([FromQuery] WorkTaskQueryParm parm)
        {
            ApiResult ret = new ApiResult { code = Code.Failure };
            try
            {
                ret = await _service.GetReadyTasks(parm);
                
            }
            catch (System.Exception ex)
            {
                ret.msg = string.Format(
                    "获取当前用户待办任务数据失败, 异常信息:{0}",
                    ex.Message);
            }
            return ret;
        }



    }
}
