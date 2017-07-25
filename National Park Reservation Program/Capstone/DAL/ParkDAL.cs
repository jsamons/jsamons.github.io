using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ParkDAL
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        private const string SQL_GetAllParks = @"select * from park;";
        private const string SQL_GetPark = @"select * from park where park_id= @park_id;";


        public List<Park> GetAllParks()
        {
            List<Park> output = new List<Park>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllParks, connection);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park p = new Park();
                        p.Park_Id = Convert.ToInt32(reader["park_id"]);
                        p.Name = Convert.ToString(reader["name"]);
                        p.Location = Convert.ToString(reader["location"]);
                        p.Establish_Date = Convert.ToDateTime(reader["establish_date"]);
                        p.Description = Convert.ToString(reader["description"]);
                        p.Area = Convert.ToInt32(reader["area"]);
                        p.Visitors = Convert.ToInt32(reader["visitors"]);

                        output.Add(p);
                    }

                }
            }
            catch(Exception)
            {
                throw;
            }

            return output;
        }

        public Park GetPark(int park_Id)
        {

            Park output = new Park();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetPark, connection);
                    cmd.Parameters.AddWithValue("@park_id", park_Id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park p = new Park();

                        p.Park_Id = Convert.ToInt32(reader["park_id"]);
                        p.Name = Convert.ToString(reader["name"]);
                        p.Location = Convert.ToString(reader["location"]);
                        p.Establish_Date = Convert.ToDateTime(reader["establish_date"]);
                        p.Description = Convert.ToString(reader["description"]);
                        p.Area = Convert.ToInt32(reader["area"]);
                        p.Visitors = Convert.ToInt32(reader["visitors"]);

                        output = p;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return output;
        }

    }
}
