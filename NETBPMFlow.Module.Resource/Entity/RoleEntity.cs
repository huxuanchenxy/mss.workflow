﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace NETBPMFlow.Module.Resource.Entity
{
    /// <summary>
    /// 角色对象
    /// </summary>
    [Table("role")]
    public class RoleEntity
    {
        public int ID { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
    }

}
