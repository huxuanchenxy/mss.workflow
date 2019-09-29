using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NETBPMFlow.Data;
using NETBPMFlow.Engine.Business.Entity;

namespace NETBPMFlow.Engine.Business.Data
{
    /// <summary>
    /// 流程数据上下文
    /// </summary>
    public class WfDbContext : DbContext
    {
        public DbSet<ProcessEntity> Processes { get; set; }
        public DbSet<ProcessInstanceEntity> ProcessInstances { get; set; }
        public DbSet<ActivityInstanceEntity> ActivityInstances { get; set; }
        public DbSet<TransitionInstanceEntity> TransitionInstances { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<LogEntity> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (ConnectionString.DbType == DatabaseTypeEnum.SQLSERVER.ToString())
                optionsBuilder.UseSqlServer(ConnectionString.Value, b=>b.UseRowNumberForPaging());
            else if (ConnectionString.DbType == DatabaseTypeEnum.MYSQL.ToString())
                optionsBuilder.UseMySql(ConnectionString.Value);
            else if (ConnectionString.DbType == DatabaseTypeEnum.ORACLE.ToString())
                optionsBuilder.UseOracle(ConnectionString.Value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}