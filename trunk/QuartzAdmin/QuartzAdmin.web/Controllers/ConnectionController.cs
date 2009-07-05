using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using QuartzAdmin.web.Models;
using QuartzAdmin.web.Helpers;

namespace QuartzAdmin.web.Controllers
{
    public class ConnectionController : Controller
    {
        IConnectionRepository _connectionRepository = new ConnectionRepository();

        public ConnectionController(IConnectionRepository repository)
        {
            _connectionRepository = repository;
        }

        //
        // GET: /Connection/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Connection/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Connection/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Connection/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                ConnectionModel connection = new ConnectionModel();
                this.UpdateModel(connection);
                connection.ConnectionParameters.AddRange(ConnectionParameterModel.FromFormCollection(collection));

                if (connection.IsValid)
                {
                    this._connectionRepository.AddConnection(connection);
                    this._connectionRepository.Save();
                }
                else
                {
                    ModelState.AddRuleViolations(connection.GetRuleViolations());
                    return View(connection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Connection/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Connection/Edit/5

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
