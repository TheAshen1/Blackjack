﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlackJack.Presentation.Controllers
{
    public class RoundsController : Controller
    {
        // GET: Rounds
        public ActionResult Index()
        {
            return View();
        }
    }
}