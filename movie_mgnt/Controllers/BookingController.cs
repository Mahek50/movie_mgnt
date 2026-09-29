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
    public class BookingController : Controller
    {


        public List<SelectListItem> Bind_Movie(int cat_id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connstr"].ToString();
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Bind_Movie", connection);
            cmd.Parameters.AddWithValue("@Cat_id", cat_id);
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new SelectListItem { Value = reader["movie_id"].ToString(), Text = reader["name"].ToString() + "\t\t|\t\tPrice : " + reader["rate"].ToString() });
            }
            ViewBag.MovieList = list;
            return list;
        }



        public int Calculate_Price(int movie_id, int no_tickets)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connstr"].ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Get_Rate", connection);
            cmd.Parameters.AddWithValue("@Movie_id", movie_id);
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            int price = 0;
            if (reader.Read())
            {
                price = Convert.ToInt32(reader["rate"]);
            }
            int total_price = price * no_tickets;
            ViewBag.TotalPrice = total_price;
            return total_price;
        }

        public int Get_User_id()
        {
            int id = 0;
            if (Session["Email"] != null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connstr"].ToString();
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("Get_User", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                cmd.Parameters.AddWithValue("@Email_id", Session["Email"]);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    id = Convert.ToInt32(reader["user_id"]);
                }

            }
            return id;
        }




        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }

        // GET: Booking/Details/5
        public ActionResult Details()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connstr"].ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Get_Booking", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            List<booking> booklist = new List<booking>();
            connection.Open();
            cmd.Parameters.AddWithValue("@user_id", Get_User_id());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                booking booking = new booking();
                booking.booking_id = Convert.ToInt32(reader["booking_id"]);
                booking.cat_name = reader["type"].ToString();
                booking.movie_name = reader["name"].ToString();
                booking.no_tickets = Convert.ToInt32(reader["no_tickets"]);
                booking.amount = Convert.ToInt32(reader["amount"]);
                booklist.Add(booking);
            }
            reader.Close();
            connection.Close();
            return View(booklist);
        }

        // GET: Booking/Create
        public ActionResult Create(int? cat_id, int? movie_id, int? no_tickets)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connstr"].ToString();
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Bind_Category", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new SelectListItem { Value = reader["cat_id"].ToString(), Text = reader["type"].ToString() });
            }
            ViewBag.CategoryList = list;
            ViewBag.MovieList = new List<SelectListItem>();
            // Bind Movie according to selected Category
            if (cat_id != null)
            {
                ViewBag.MovieList = Bind_Movie(cat_id.Value);
            }
            else
            {
                ViewBag.MovieList = new List<SelectListItem>();
            }

            booking book = new booking();

            if (cat_id != null)
            {
                book.cat_id = cat_id.Value;
            }

            if (movie_id != null)
            {
                book.movie_id = movie_id.Value;
            }

            if (no_tickets != null)
            {
                book.no_tickets = no_tickets.Value;
            }

            if (movie_id != null && no_tickets != null)
            {
                book.amount = Calculate_Price(movie_id.Value, no_tickets.Value);
                ViewBag.TotalPrice = book.amount;
            }
            return View(book);
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(booking book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["connstr"].ToString();
                    SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand("Insert_Booking", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@User_Id", Get_User_id());
                    cmd.Parameters.AddWithValue("@Cat_ID", book.cat_id);
                    cmd.Parameters.AddWithValue("@Movie_ID", book.movie_id);
                    cmd.Parameters.AddWithValue("@No_of_Tickets", book.no_tickets);
                    cmd.Parameters.AddWithValue("@Amount", book.amount);
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        ViewBag.Message = "Booking Insert Successfully";
                        return View(book);
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex + " Insertion Failed";
                return View(book);
            }
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int id, int? cat_id, int? movie_id, int? no_tickets)
        {
            booking book = new booking();
            string connectionString = ConfigurationManager.ConnectionStrings["connstr"].ToString();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Get_Booking_ById", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Booking_id", id);

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                book.booking_id = Convert.ToInt32(reader["booking_id"]);
                book.user_id = Convert.ToInt32(reader["user_id"]);
                book.cat_id = Convert.ToInt32(reader["cat_id"]);
                book.movie_id = Convert.ToInt32(reader["movie_id"]);
                book.no_tickets = Convert.ToInt32(reader["no_tickets"]);
                book.amount = Convert.ToInt32(reader["amount"]);
            }

            reader.Close();
            connection.Close();

            List<SelectListItem> list = new List<SelectListItem>();

            connection = new SqlConnection(connectionString);
            cmd = new SqlCommand("Bind_Category", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new SelectListItem
                {
                    Value = reader["cat_id"].ToString(),
                    Text = reader["type"].ToString()
                });
            }

            reader.Close();
            connection.Close();

            ViewBag.CategoryList = list;
            if (cat_id != null)
            {
                book.cat_id = cat_id.Value;
            }

            if (movie_id != null)
            {
                book.movie_id = movie_id.Value;
            }

            if (no_tickets != null)
            {
                book.no_tickets = no_tickets.Value;
            }
            ViewBag.MovieList = new List<SelectListItem>();

            if (book.cat_id != 0)
            {
                ViewBag.MovieList = Bind_Movie(book.cat_id);
            }

            if (movie_id != null && no_tickets != null)
            {
                book.amount = Calculate_Price(movie_id.Value, no_tickets.Value);
                ViewBag.TotalPrice = book.amount;
            }
            else
            {
                ViewBag.TotalPrice = book.amount;
            }

            return View(book);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        public ActionResult Edit(booking book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["connstr"].ToString();
                    SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand("Update_Booking", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Booking_id", book.booking_id);
                    cmd.Parameters.AddWithValue("@User_id", Get_User_id());
                    cmd.Parameters.AddWithValue("@Cat_id", book.cat_id);
                    cmd.Parameters.AddWithValue("@Movie_id", book.movie_id);
                    cmd.Parameters.AddWithValue("@no_of_Tickets", book.no_tickets);
                    cmd.Parameters.AddWithValue("@amount", book.amount);
                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        ViewBag.Message = "Booking Update Successfully";
                        return View(book);
                    }
                }

                return View(book);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex + " Updation Failed";
                return View(book);
            }
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
        {
            booking booking = new booking();
            string connectionString = ConfigurationManager.ConnectionStrings["connstr"].ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Get_Booking_ById", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            connection.Open();
            cmd.Parameters.AddWithValue("@Booking_id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                booking.booking_id = Convert.ToInt32(reader["booking_id"]);
                booking.user_id = Convert.ToInt32(reader["user_id"]);
                booking.cat_name = reader["type"].ToString();
                booking.movie_name = reader["name"].ToString();
                booking.no_tickets = Convert.ToInt32(reader["No_tickets"]);
                booking.amount = Convert.ToInt32(reader["amount"]);

            }
            reader.Close();
            connection.Close();
            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, booking book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["connstr"].ToString();
                    SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand("Delete_Booking", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Booking_id", id);

                    int i = cmd.ExecuteNonQuery();
                    connection.Close();
                    if (i > 0)
                    {
                        ViewBag.Message = "Delete Sucessfully";
                        return View(book);
                    }
                }
                return View(book);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex + " Delete Failed";
                return View(book);
            }
        }
    }
}
    

