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
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        // GET: Search/Details/5
        public ActionResult Details(int?cat_id)
        {
            List<movie> movlist = new List<movie>();

            string connectionString = ConfigurationManager.ConnectionStrings["connstr"].ToString();

            List<SelectListItem> list = new List<SelectListItem>();

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            // Bind Category
            SqlCommand cmd1 = new SqlCommand("Bind_Category", connection);
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new SelectListItem
                {
                    Value = reader["Cat_id"].ToString(),
                    Text = reader["type"].ToString()
                });
            }

            reader.Close();
            connection.Close();

            ViewBag.CategoryList = list;
            // Bind Movie
            if (cat_id != null)
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("Bind_Movie", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Cat_id", cat_id);

                SqlDataReader reader1 = cmd.ExecuteReader();

                while (reader1.Read())
                {
                    movie mov = new movie();

                    mov.movie_id = Convert.ToInt32(reader1["movie_id"]);
                    mov.name = reader1["name"].ToString();
                    mov.cat_id = Convert.ToInt32(reader1["cat_id"]);
                    mov.r_date = Convert.ToDateTime(reader1["r_date"]);
                    mov.rate = Convert.ToInt32(reader1["rate"]);

                    movlist.Add(mov);
                }
                ViewBag.SelectedCatId = cat_id;
                reader1.Close();
                connection.Close();
            }

            return View(movlist);
        }

        // GET: Search/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
        [HttpPost]
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

        // GET: Search/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Search/Edit/5
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

        // GET: Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Search/Delete/5
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
