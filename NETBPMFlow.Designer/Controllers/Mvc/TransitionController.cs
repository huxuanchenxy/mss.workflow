﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace NETBPMFlow.Designer.Controllers.Mvc
{
    /// <summary>
    /// 转移属性控制器
    /// </summary>
    public class TransitionController : Controller
    {
        // GET: Transition
        public IActionResult Edit()
        {
            return View();
        }
    }
}