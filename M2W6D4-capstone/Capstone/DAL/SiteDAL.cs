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
    public class SiteDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        private const string SQL_GetSites = @"select * from site where campground_id = @campground_id;";

        public List<Site> GetSites(int campground_Id)
        {
            List<Site> output = new List<Site>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetSites, connection);
                    cmd.Parameters.AddWithValue("@campground_id", campground_Id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Site s = new Site();

                        s.Campground_Id = Convert.ToInt32(reader["campground_id"]);
                        s.Site_Id = Convert.ToInt32(reader["site_id"]);
                        int hasUtilities = Convert.ToInt32(reader["utilities"]);
                        if(hasUtilities==1)
                        {
                            s.HasUtilities = true;
                        }
                        s.Max_Occupancy = Convert.ToInt32(reader["max_occupancy"]);
                        s.Max_Rv_Length = Convert.ToInt32(reader["max_rv_length"]);
                        int accessible = Convert.ToInt32(reader["accessible"]);
                        if(accessible == 1)
                        {
                            s.WheelchairAccessible = true;
                        }

                        output.Add(s);
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
