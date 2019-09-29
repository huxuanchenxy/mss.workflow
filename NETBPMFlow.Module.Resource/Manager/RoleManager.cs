using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NETBPMFlow.Module.Resource.Data;
using NETBPMFlow.Module.Resource.Entity;

namespace NETBPMFlow.Module.Resource.Manager
{
    /// <summary>
    /// 角色管理类
    /// </summary>
    internal class RoleManager
    {
        /// <summary>
        /// 获取所有角色数据
        /// </summary>
        /// <returns>角色列表</returns>
        //internal List<RoleEntity> GetAll()
        //{
        //    using (var session = DbFactory.CreateSession())
        //    {
        //        var list = session.GetRepository<RoleEntity>().GetDbSet()
        //            .OrderBy(e => e.RoleName)
        //            .ToList();
        //        return list;
        //    }
        //}

        internal List<RoleEntity> GetAll()
        {
            using (var session = DbFactory.CreateSession())
            {
                string sql = $@" SELECT ID,role_name AS RoleName,CAST(id as CHAR(50)) AS RoleCode FROM role ";
                var list = session.GetRepository<RoleEntity>().Query(sql)
                    .OrderBy(e => e.RoleName)
                    .ToList();
                return list;
            }
        }
    }
}