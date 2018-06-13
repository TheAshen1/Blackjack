using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BlackJack.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();

            return View();
        }
    }
}
