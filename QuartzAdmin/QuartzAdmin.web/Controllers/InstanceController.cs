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
        public Models.IInstanceRepository Repository { get; set; }

        public InstanceController()
        {
            Repository = new QuartzAdmin.web.Models.InstanceRepository();
        }
        public InstanceController(Models.IInstanceRepository repository)
        {
            Repository = repository;
        }

        //
        // GET: /Instance/

        public ActionResult Index()
        {
            return View(Repository.GetAll());
        }

        //
        // GET: /Instance/Details/5

        public ActionResult Details(string id)
        {
            Models.InstanceModel instance = Repository.GetByName(id);
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
                Models.InstanceModel instance = new QuartzAdmin.web.Models.InstanceModel();
                instance.InstanceName = collection["InstanceName"];

                foreach (string key in collection.Keys)
                {
                    if (key.Contains("InstancePropertyKey"))
                    {
                        string propIdx = key.Replace("InstancePropertyKey", ""); 
                        instance.InstanceProperties.Add(new QuartzAdmin.web.Models.InstancePropertyModel() { PropertyName = collection[key], PropertyValue = collection["InstancePropertyValue" + propIdx.ToString()] });
                    }
                }
                this.Repository.Save(instance);

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
