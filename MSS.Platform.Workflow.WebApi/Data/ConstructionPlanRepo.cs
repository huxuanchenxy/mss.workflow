using Dapper;
using MSS.Platform.Workflow.WebApi.Model;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Coded By admin 2019/9/27 11:18:53
namespace MSS.Platform.Workflow.WebApi.Data
{
    public interface IConstructionPlanRepo<T> where T : BaseEntity
    {
        Task<ConstructionPlanPageView> GetPageList(ConstructionPlanParm param);
        Task<ConstructionPlan> Save(ConstructionPlan obj);
        Task<ConstructionPlan> GetByID(long id);
        Task<int> Update(ConstructionPlan obj);
        Task<int> Delete(string[] ids, int userID);
    }

    public class ConstructionPlanRepo : BaseRepo, IConstructionPlanRepo<ConstructionPlan>
    {
        public ConstructionPlanRepo(DapperOptions options) : base(options) { }

        public async Task<ConstructionPlanPageView> GetPageList(ConstructionPlanParm parm)
        {
            return await WithConnection(async c =>
            {

                StringBuilder sql = new StringBuilder();
                sql.Append($@"  SELECT 
                id,
                line_id,
                area_id,
                area_typename,
                plan_name,
                plan_type,
                plan_number,
                important_level,
                plan_level,
                apply_company_org_id,
                construction_company_org_id,
                start_time,
                end_time,
                register_station_id,
                device_num,
                construction_content,
                construction_detail,
                coordination_request,
                coordination_audit,
                stop_electric,
                traction_power,
                traction_train_num,
                traction_start_time,
                traction_time,
                use_laddercar,
                electric_range,
                traction_car_route,
                effect_area,
                effect_explain,
                safe_measure,
                force_unlock_key,
                other_request,
                memo,
                created_time,
                created_by,
                updated_time,
                updated_by,is_del FROM construction_plan
                 ");
                StringBuilder whereSql = new StringBuilder();
                //whereSql.Append(" WHERE ai.ProcessInstanceID = '" + parm.ProcessInstanceID + "'");

                //if (parm.AppName != null)
                //{
                //    whereSql.Append(" and ai.AppName like '%" + parm.AppName.Trim() + "%'");
                //}

                sql.Append(whereSql);
                //验证是否有参与到流程中
                //string sqlcheck = sql.ToString();
                //sqlcheck += ("AND ai.CreatedByUserID = '" + parm.UserID + "'");
                //var checkdata = await c.QueryFirstOrDefaultAsync<TaskViewModel>(sqlcheck);
                //if (checkdata == null)
                //{
                //    return null;
                //}

                var data = await c.QueryAsync<ConstructionPlan>(sql.ToString());
                var total = data.ToList().Count;
                sql.Append(" order by " + parm.sort + " " + parm.order)
                .Append(" limit " + (parm.page - 1) * parm.rows + "," + parm.rows);
                var ets = await c.QueryAsync<ConstructionPlan>(sql.ToString());

                ConstructionPlanPageView ret = new ConstructionPlanPageView();
                ret.rows = ets.ToList();
                ret.total = total;
                return ret;
            });
        }

        public async Task<ConstructionPlan> Save(ConstructionPlan obj)
        {
            return await WithConnection(async c =>
            {
                string sql = $@" INSERT INTO `construction_plan`(
                    
                    line_id,
                    area_id,
                    area_typename,
                    plan_name,
                    plan_type,
                    plan_number,
                    important_level,
                    plan_level,
                    apply_company_org_id,
                    construction_company_org_id,
                    start_time,
                    end_time,
                    register_station_id,
                    device_num,
                    construction_content,
                    construction_detail,
                    coordination_request,
                    coordination_audit,
                    stop_electric,
                    traction_power,
                    traction_train_num,
                    traction_start_time,
                    traction_time,
                    use_laddercar,
                    electric_range,
                    traction_car_route,
                    effect_area,
                    effect_explain,
                    safe_measure,
                    force_unlock_key,
                    other_request,
                    memo,
                    created_time,
                    created_by,
                    updated_time,
                    updated_by,
                    is_del
                ) VALUES 
                (
                    @LineId,
                    @AreaId,
                    @AreaTypename,
                    @PlanName,
                    @PlanType,
                    @PlanNumber,
                    @ImportantLevel,
                    @PlanLevel,
                    @ApplyCompanyOrgId,
                    @ConstructionCompanyOrgId,
                    @StartTime,
                    @EndTime,
                    @RegisterStationId,
                    @DeviceNum,
                    @ConstructionContent,
                    @ConstructionDetail,
                    @CoordinationRequest,
                    @CoordinationAudit,
                    @StopElectric,
                    @TractionPower,
                    @TractionTrainNum,
                    @TractionStartTime,
                    @TractionTime,
                    @UseLaddercar,
                    @ElectricRange,
                    @TractionCarRoute,
                    @EffectArea,
                    @EffectExplain,
                    @SafeMeasure,
                    @ForceUnlockKey,
                    @OtherRequest,
                    @Memo,
                    @CreatedTime,
                    @CreatedBy,
                    @UpdatedTime,
                    @UpdatedBy,
                    @IsDel
                    );
                    ";
                sql += "SELECT LAST_INSERT_ID() ";
                int newid = await c.QueryFirstOrDefaultAsync<int>(sql, obj);
                obj.Id = newid;
                return obj;
            });
        }

        public async Task<ConstructionPlan> GetByID(long id)
        {
            return await WithConnection(async c =>
            {
                var result = await c.QueryFirstOrDefaultAsync<ConstructionPlan>(
                    "SELECT * FROM construction_plan WHERE id = @id", new { id = id });
                return result;
            });
        }

        public async Task<int> Update(ConstructionPlan obj)
        {
            return await WithConnection(async c =>
            {
                var result = await c.ExecuteAsync($@" UPDATE construction_plan set 
                    
                    line_id=@LineId,
                    area_id=@AreaId,
                    area_typename=@AreaTypename,
                    plan_name=@PlanName,
                    plan_type=@PlanType,
                    plan_number=@PlanNumber,
                    important_level=@ImportantLevel,
                    plan_level=@PlanLevel,
                    apply_company_org_id=@ApplyCompanyOrgId,
                    construction_company_org_id=@ConstructionCompanyOrgId,
                    start_time=@StartTime,
                    end_time=@EndTime,
                    register_station_id=@RegisterStationId,
                    device_num=@DeviceNum,
                    construction_content=@ConstructionContent,
                    construction_detail=@ConstructionDetail,
                    coordination_request=@CoordinationRequest,
                    coordination_audit=@CoordinationAudit,
                    stop_electric=@StopElectric,
                    traction_power=@TractionPower,
                    traction_train_num=@TractionTrainNum,
                    traction_start_time=@TractionStartTime,
                    traction_time=@TractionTime,
                    use_laddercar=@UseLaddercar,
                    electric_range=@ElectricRange,
                    traction_car_route=@TractionCarRoute,
                    effect_area=@EffectArea,
                    effect_explain=@EffectExplain,
                    safe_measure=@SafeMeasure,
                    force_unlock_key=@ForceUnlockKey,
                    other_request=@OtherRequest,
                    memo=@Memo,
                    created_time=@CreatedTime,
                    created_by=@CreatedBy,
                    updated_time=@UpdatedTime,
                    updated_by=@UpdatedBy,
                    is_del=@IsDel
                 where id=@Id", obj);
                return result;
            });
        }

        public async Task<int> Delete(string[] ids, int userID)
        {
            return await WithConnection(async c =>
            {
                var result = await c.ExecuteAsync(" Update construction_plan set is_del=1" +
                ",updated_time=@updated_time,updated_by=@updated_by" +
                " WHERE id in @ids ", new { ids = ids, updated_time = DateTime.Now, updated_by = userID });
                return result;
            });
        }
    }
}



