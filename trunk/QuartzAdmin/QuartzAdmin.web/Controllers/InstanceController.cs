using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace QuartzAdmin.web.Controllers
{
    public class InstanceController : Controller
    {
        Models.InstanceRepository repo = new QuartzAdmin.web.Models.InstanceRepository();
        //
        // GET: /Instance/

        public ActionResult Index()
        {
            return View(repo.GetAllInstances());
        }

        //
        // GET: /Instance/Details/5

        public ActionResult Details(string id)
        {
            Models.InstanceModel instance = repo.GetInstance(id);
            if (instance == null)
            {
                return View("NotFound");
            }
            else
            {
                instance.IsValid();
                return View(instance);
            }
        }

        //
        // GET: /Instance/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Instance/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Instance/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Instance/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
