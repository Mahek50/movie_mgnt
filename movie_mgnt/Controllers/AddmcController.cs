using movie_mgnt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace movie_mgnt.Controllers
{
    public class AddmcController : Controller
    {
        // GET: Addmc
        public ActionResult Index(movie_cat mc)
        {
            movie_handeler mhandel = new movie_handeler();
            ModelState.Clear();
            return View(mhandel.displaycategry());
        }

        // GET: Addmc/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Addmc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Addmc/Create
        [HttpPost]
        public ActionResult Create(movie_cat mc)
        {
            try
            {
                if(ModelState.IsValid)
                {
                   movie_handeler mh = new movie_handeler();
                    if(mh.Add_movie_category(mc))
                    {
                        ViewBag.Message = "insertedd";
                        ModelState.Clear();
                    }
                }
                return View();
               
            }
            catch
            {
                return View();
            }
        }

        // GET: Addmc/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Addmc/Edit/5
        [HttpPost]
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

        // GET: Addmc/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Addmc/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
