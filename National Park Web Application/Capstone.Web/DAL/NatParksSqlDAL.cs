using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class NatParksSqlDAL
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalParkGeek;Integrated Security=True";
        private const string SQL_GetNationalParks = "select * from park";
        private const string SQL_GetSinglePark = "select * from park where parkCode = @parkCode";

        public List<NationalPark> GetAllParks()
        {
            List<NationalPark> nationalParks = new List<NationalPark>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetNationalParks, connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        NationalPark np = new NationalPark();
                        np.ParkCode = Convert.ToString(reader["parkCode"]);
                        np.ParkName = Convert.ToString(reader["parkName"]);
                        np.ParkDescription = Convert.ToString(reader["parkDescription"]);
                        np.State = Convert.ToString(reader["state"]);
                        np.Acreage = Convert.ToInt32(reader["acreage"]);
                        np.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                        np.MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]);
                        np.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                        np.Climate = Convert.ToString(reader["climate"]);
                        np.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                        np.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                        np.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                        np.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                        np.EntryFee = Convert.ToInt32(reader["entryFee"]);
                        np.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);

                        nationalParks.Add(np);
                    }
                    return nationalParks;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NationalPark GetSinglePark(string parkCode)
        {
            NationalPark park = new NationalPark();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetSinglePark, connection);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        NationalPark np = new NationalPark();
                        np.ParkCode = Convert.ToString(reader["parkCode"]);
                        np.ParkName = Convert.ToString(reader["parkName"]);
                        np.ParkDescription = Convert.ToString(reader["parkDescription"]);
                        np.State = Convert.ToString(reader["state"]);
                        np.Acreage = Convert.ToInt32(reader["acreage"]);
                        np.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                        np.MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]);
                        np.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                        np.Climate = Convert.ToString(reader["climate"]);
                        np.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                        np.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                        np.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                        np.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                        np.EntryFee = Convert.ToInt32(reader["entryFee"]);
                        np.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);

                        park = np;
                    }
                }
                return park;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}