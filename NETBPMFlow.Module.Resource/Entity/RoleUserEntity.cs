using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace NETBPMFlow.Module.Resource.Entity
{
    [Table("SysRoleUser")]
    public class RoleUserEntity
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
    }
}
