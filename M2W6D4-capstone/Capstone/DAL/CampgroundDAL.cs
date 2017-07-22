using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace Capstone.DAL
{
    public class CampgroundDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        private const string SQL_GetCampgrounds = @"select * from campground where park_id = @park_id";
        private const string SQL_GetSingleCampground = @"select * from campground where campground_id=@campground_id;";


        public List<Campground> GetCampgrounds(int park_ID)
        {
            List<Campground> output = new List<Campground>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetCampgrounds, connection);
                    cmd.Parameters.AddWithValue( "@park_id", park_ID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Campground c = new Campground();
                        c.Campground_Id = Convert.ToInt32(reader["campground_Id"]);
                        c.Park_Id = Convert.ToInt32(reader["park_id"]);
                        c.Name = Convert.ToString(reader["name"]);
                        c.Opening_Month = Convert.ToInt32(reader["open_from_mm"]);
                        c.Closing_Month = Convert.ToInt32(reader["open_to_mm"]);
                        c.Daily_Fee = Convert.ToDecimal(reader["daily_fee"]);

                        output.Add(c);
                    }

                }
            }
            catch(Exception)
            {
                throw;
            }

            return output;
        }

        public Campground GetSingleCampground(int campgroundID)
        {
            Campground output=new Campground();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetSingleCampground, connection);
                    cmd.Parameters.AddWithValue("@campground_id", campgroundID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground c = new Campground();
                        c.Campground_Id = Convert.ToInt32(reader["campground_Id"]);
                        c.Park_Id = Convert.ToInt32(reader["park_id"]);
                        c.Name = Convert.ToString(reader["name"]);
                        c.Opening_Month = Convert.ToInt32(reader["open_from_mm"]);
                        c.Closing_Month = Convert.ToInt32(reader["open_to_mm"]);
                        c.Daily_Fee = Convert.ToDecimal(reader["daily_fee"]);

                        output= c;
                    }
                }
            }

            catch(Exception)
            {
                throw;
            }

            return output;
        }
    }
}
