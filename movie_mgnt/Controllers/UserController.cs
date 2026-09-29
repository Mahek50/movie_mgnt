using movie_mgnt.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace movie_mgnt.Controllers
{
    public class UserController : Controller
    {


        public int Get_User_id()
        {
            int id = 0;

            if (Session["Email"] != null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connstr"]
                    .ConnectionString;

                using (SqlConnection connection =
                       new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd =
                           new SqlCommand("Get_User", connection))
                    {
                        cmd.CommandType =
                            CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue(
                            "@Email_id",
                            Session["Email"].ToString());

                        connection.Open();

                        using (SqlDataReader reader =
                               cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                id = Convert.ToInt32(
                                    reader["user_id"]);
                            }
                        }
                    }
                }
            }

            return id;
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
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

        // GET: User/Edit/5
        public ActionResult Edit()
        {
            user use = new user();
            if (Session["Email"] != null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connstr"].ToString();
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("Get_User", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@Email_id", Session["Email"]);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    use.user_id = Convert.ToInt32(reader["user_id"]);
                    use.user_name = reader["user_name"].ToString();
                    use.email_id = reader["email"].ToString();
                    use.user_password = reader["password"].ToString();
                    use.city = reader["city"].ToString();
                    use.phone_number = reader["pno"].ToString();
                }
                connection.Close();
            }
            return View(use);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(user use)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["connstr"].ToString();
                    SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand("Update_User", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@User_id", Get_User_id());
                    cmd.Parameters.AddWithValue("@User_name", use.user_name);
                    cmd.Parameters.AddWithValue("@Email_id", use.email_id);
                    cmd.Parameters.AddWithValue("@User_password", use.user_password);
                    cmd.Parameters.AddWithValue("@City", use.city);
                    cmd.Parameters.AddWithValue("@PhoneNo", use.phone_number);
                    cmd.ExecuteNonQuery();
                    ViewBag.Message = "Update Successfully!";
                    connection.Close();
                }
                return View(use);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex + " Error to Update profile";
                return View(use);
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
