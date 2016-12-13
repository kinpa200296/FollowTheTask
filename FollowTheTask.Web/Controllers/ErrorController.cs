using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FollowTheTask.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        public ActionResult Index(string message)
        {
            if (message == null) message = "404 - Not found";
            return View(message);
        }

        public ActionResult Internal()
        {
            var message = "500 - Internal error :(";
            return View("Index", message );
        }

        public ActionResult AccessViolation()
        {
            var message = "403 - Access voilation";
            return View("Index", message );
        }
    }
}