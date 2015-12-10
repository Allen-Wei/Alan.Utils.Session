using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alan.Utils.Session.Demo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //set
            var guid = Guid.NewGuid().ToString();
            Response.Headers.Add("X-Token", guid);
            Alan.Utils.Session.Singleton.Container.Set(guid, new { FirstName = "Alan", LastName = "Wei" }, DateTime.Now.AddSeconds(30));
            return View();
        }
    }
}