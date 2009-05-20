using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace QuartzAdmin.web.Controllers
{
    public class CalendarController : Controller
    {
        Models.CalendarRepository calRepo = new QuartzAdmin.web.Models.CalendarRepository();

        //
        // GET: /Calendar/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string id)
        {
            Quartz.ICalendar cal = calRepo.GetCalendar(id);
            ViewData["calendarName"] = id;

            if (cal == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(cal);
            }
        }

    }
}
