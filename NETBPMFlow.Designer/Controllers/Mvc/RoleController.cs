﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace NETBPMFlow.Designer.Controllers.Mvc
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    public class RoleController : Controller
    {
        // GET: Role
        public IActionResult List()
        {
            return View();
        }
    }
}