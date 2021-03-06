﻿using System.Web.Mvc;

namespace FollowTheTask.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        public ActionResult Index(string message)
        {
            if (message == null) message = "404 - Not found";
            return View((object)message);
        }

        public ActionResult Internal()
        {
            var message = "500 - Internal error :(";
            return RedirectToAction("Index", "Error", new { message = message});
        }

        public ActionResult AccessViolation()
        {
            var message = "403 - Access voilation";
            return RedirectToAction("Index", "Error", new {message = message});
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult InternalError()
        {
            return View();
        }
    }
}