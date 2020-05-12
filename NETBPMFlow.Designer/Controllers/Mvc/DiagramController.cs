﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace NETBPMFlow.Designer.Controllers.Mvc
{
    /// <summary>
    /// 流程图查看页面
    /// </summary>
    public class DiagramController : Controller
    {
        // GET: Diagram
        public IActionResult Index()
        {
            ViewBag.AppInstanceID = Request.Query["AppInstanceID"];
            ViewBag.ProcessGUID = Request.Query["ProcessGUID"];
            return View();
        }
    }
}