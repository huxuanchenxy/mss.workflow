﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace NETBPMFlow.Module.Resource.Entity
{
    /// <summary>
    /// 用户对象
    /// </summary>
    [Table("user")]
    public class UserEntity
    {
        public int ID { get; set; }
        public string UserName { get; set; }
    }
}
