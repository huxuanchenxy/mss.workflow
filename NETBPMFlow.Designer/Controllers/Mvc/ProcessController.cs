﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace NETBPMFlow.Designer.Controllers.Mvc
{
    /// <summary>
    /// 流程定义控制器
    /// </summary>
    public class ProcessController : Controller
    {
        // GET: Process
        public IActionResult List()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Import()
        {
            return View();
        }
    }
}