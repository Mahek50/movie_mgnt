using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using movie_mgnt.Models;

namespace movie_mgnt.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(Login log )
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connstr"].ToString();
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("Login_User", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@Email_id", log.email_id);
                cmd.Parameters.AddWithValue("@User_password", log.user_password);
                object resultObj =
                   cmd.ExecuteScalar();

                int result = 0;

                if (resultObj != null &&
                    resultObj != DBNull.Value)
                {
                    result = Convert.ToInt32(resultObj);
                }

                if (result > 0)
                {
                    Session["Email"] = log.email_id;
                    if (Session["Email"].ToString() == log.email_id)
                    {
                        return RedirectToAction("Index", "Addmc");
                    }
                    else
                    {
                        return RedirectToAction("Details", "Booking");
                    }

                }
                else
                {
                    ViewBag.Error = "Email or Password Invalid.";
                    return View(log);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
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

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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
