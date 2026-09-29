using movie_mgnt.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace movie_mgnt.Controllers
{
    public class AddmovieController : Controller
    {
        // GET: Addmovie
        public ActionResult Index()
        {
            return View();
        }

        // GET: Addmovie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Addmovie/Create
        public ActionResult Create()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["connstr"].ToString();
            SqlConnection con = new SqlConnection(connectionstring);
            List<SelectListItem> list = new List<SelectListItem>();
            SqlCommand cmd = new SqlCommand("Bind_Category", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new SelectListItem
                {
                    Value = reader["cat_id"].ToString(),
                    Text = reader["type"].ToString(),
                });
            }
            con.Close();
            ViewBag.CategoryList = list;
            return View(new movie());
        }

        // POST: Addmovie/Create
        [HttpPost]
        public ActionResult Create(movie m)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    movie_handeler mh = new movie_handeler();
                    if (mh.Add_movie(m))
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

        // GET: Addmovie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Addmovie/Edit/5
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

        // GET: Addmovie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Addmovie/Delete/5
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
