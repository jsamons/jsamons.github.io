using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class WeatherDAL
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalParkGeek;Integrated Security=True";
        private const string SQL_GetParkWeather = "select * from weather where parkCode = @parkCode";

        public List<Weather> GetAllWeather(string parkCode)
        {
            List<Weather> parkWeather = new List<Weather>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetParkWeather, connection);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Weather w = new Weather();
                        w.ParkCode = Convert.ToString(reader["parkCode"]);
                        w.FiveDayForeCast = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        w.Low = Convert.ToInt32(reader["low"]);
                        w.High = Convert.ToInt32(reader["high"]);
                        w.Forecast = Convert.ToString(reader["forecast"]);

                        parkWeather.Add(w);
                    }

                    return parkWeather;
                }
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}