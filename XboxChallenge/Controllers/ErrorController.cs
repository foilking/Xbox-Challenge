using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XboxChallenge.Controllers
{
    /*
     * Incredibly light controller for handling errors. Normally, you would have these go out to 
     * views that would display more detailed and interesting error pages, but because of time
     * constraints, I've gone with just simple messages.
     */
    public class ErrorController : Controller
    {
        /*
         * Generic error page, a catch all.
         */
        public ActionResult General(Exception exception)
        {
            return Content("General failure", "text/plain");
        }

        /*
         * 404 Page
         */
        public ActionResult Http404()
        {
            return Content("Not found", "text/plain");
        }

        /*
         * 403 Page
         */
        public ActionResult Http403()
        {
            return Content("Forbidden", "text/plain");
        }

    }
}
