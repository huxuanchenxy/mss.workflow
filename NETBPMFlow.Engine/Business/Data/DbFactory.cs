﻿using System;
using System.Collections.Generic;
using System.Text;
using NETBPMFlow.Data;
using Microsoft.EntityFrameworkCore;

namespace NETBPMFlow.Engine.Business.Data
{
    /// <summary>
    /// 上下文创建工厂类
    /// </summary>
    public class DbFactory
    {
        /// <summary>
        /// 创建数据会话
        /// </summary>
        /// <returns>会话</returns>
        public static IDbSession CreateSession()
        {
            var session = SessionFactory.CreateSession(new WfDbContext());
            return session;
        }
    }
}
