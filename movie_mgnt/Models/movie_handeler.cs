using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace movie_mgnt.Models
{
    public class movie_handeler
    {
        SqlConnection con;
        public SqlConnection connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
            return new SqlConnection(constr);
        }

        public bool Add_movie_category(movie_cat mc)
        {

            con = connection();
            SqlCommand cmd = new SqlCommand("Add_movie_c", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@type", mc.type);
            con.Open();
            int res= cmd.ExecuteNonQuery();
            con.Close();
            if (res >= 1)
            {
             return true;
            }
            return false;

        }
        public bool Add_movie(movie m)
        {

            con = connection();
            SqlCommand cmd = new SqlCommand("Add_movie", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", m.name);
            cmd.Parameters.AddWithValue("@realse_date", m.r_date);
            cmd.Parameters.AddWithValue("@cat_id", m.cat_id);
            cmd.Parameters.AddWithValue("@rate", m.rate);

            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            if (res >= 1)
            {
                return true;
            }
            return false;

        }


        public List<movie_cat> displaycategry()
        {
            con = connection();
            List<movie_cat> movcat = new List<movie_cat>();
            SqlCommand cmd = new SqlCommand("display_category",con);
            cmd.CommandType= CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            adp.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                movcat.Add(
                    new movie_cat
                    {
                        cat_id = Convert.ToInt32(dr["cat_id"]),
                        type = Convert.ToString(dr["type"])
                    });


            }

            return movcat ;
        }
    }
}