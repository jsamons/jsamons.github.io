using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class SurveySqlDAL
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalParkGeek;Integrated Security=True";
        private const string SQL_SubmitSurveyResult = "insert into survey_result values(@parkCode, @emailAddress, @state, @activityLevel)";
        private const string SQL_GetTopParks = "select count(parkCode) as park_count, parkCode from survey_result group by parkCode order by park_count desc";


        public bool SubmitSurvey(Survey survey)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(SQL_SubmitSurveyResult, connection);

                    cmd.Parameters.AddWithValue(@"parkCode", survey.ParkCode);
                    cmd.Parameters.AddWithValue(@"emailAddress", survey.EmailAddress);
                    cmd.Parameters.AddWithValue(@"state", survey.State);
                    cmd.Parameters.AddWithValue(@"activityLevel", survey.ActivityLevel);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Dictionary<string, int> TopParks()
        {
            Dictionary<string, int> topParks = new Dictionary<string, int>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetTopParks, connection);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {

                        topParks.Add(Convert.ToString(reader["parkCode"]), Convert.ToInt32(reader["park_count"]));
                    }
                    return (topParks);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        
    }
}