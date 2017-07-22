using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.DAL;
using Capstone.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Transactions;
using System.Collections.Generic;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationDALTests
    {
        private TransactionScope tran;
        private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        private DateTime from_date = DateTime.Today.AddDays(5).Date;
        private DateTime to_date = DateTime.Today.AddDays(7).Date;   
        private DateTime current_date = DateTime.Now;


        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                connection.Open();

                cmd = new SqlCommand(@"set identity_insert park on", connection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"insert into park (park_id, name, location, establish_date, area, visitors, description) values (1000, 'name', 'location', '1999-10-29', 3, 5, 'description');", connection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"set identity_insert park off", connection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"set identity_insert campground on", connection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"insert into campground (campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee) values(1000, 1000, 'name', 10, 12, 30.00) ;", connection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"set identity_insert campground off", connection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"set identity_insert site on", connection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"insert into site (site_id, campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) values (1000, 1000, 5, 10, 1, 30, 1) ;", connection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"insert into site (site_id, campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) values (1001, 1000, 6, 10, 1, 30, 1) ;", connection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"set identity_insert site off", connection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(@"insert into reservation (site_id, name, from_date, to_date, create_date) values (1000, 'name', @from_date, @to_date, @current_date);", connection);
                cmd.Parameters.AddWithValue("@from_date", from_date);
                cmd.Parameters.AddWithValue("@to_date", to_date);
                cmd.Parameters.AddWithValue("@current_date", current_date);
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetReservationsTest()
        {
            ReservationDAL test = new ReservationDAL();

            DateTime from_date = DateTime.Today.AddDays(5).Date;
            DateTime to_date = DateTime.Today.AddDays(7).Date;
            List<Site> output = test.GetOpenings(1000, 1000, from_date, to_date);

            bool exists = false;

            foreach (Site open in output)
            {
                if (open.Site_Id == 1001)
                {
                    exists = true;
                }
            }

            Assert.IsNotNull(output);
            Assert.AreEqual(true, exists);

        }

        [TestMethod]
        public void GetNext30DaysReservations()
        {
            ReservationDAL test = new ReservationDAL();

            List<Reservation> output = test.GetNext30DaysReservations(1000);

            bool exists = false;

            foreach (Reservation rez in output)
            {
                if(rez.Name == "name")
                {
                    exists = true;
                }
            }

            Assert.IsNotNull(output);
            Assert.AreEqual(true, exists);
        }

        [TestMethod]
        public void InsertReservation()
        {
            ReservationDAL test = new ReservationDAL();

            Reservation testRes = new Reservation();
            testRes.Site_Id = 1001;
            testRes.Name = "Joe";
            testRes.To_Date = DateTime.Today.AddDays(5);
            testRes.From_Date = DateTime.Today.AddDays(7);

            int reservationId = test.InsertReservation(testRes);

            Assert.IsNotNull(reservationId);
        }
    }
}
