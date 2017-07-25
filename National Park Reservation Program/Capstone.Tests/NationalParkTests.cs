using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;
using Capstone;
using System.Data.SqlClient;
using System.Configuration;
using System.Transactions;

namespace Capstone.Tests
{

   

    [TestClass]
    public class UnitTest1
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
        public void GetParksTest()
        {
            NationalPark park = new NationalPark();
            string[] parks = park.GetParks();

            CollectionAssert.AllItemsAreNotNull(parks);
            CollectionAssert.Contains(parks, "name");
        }

        [TestMethod]
        public void GetSelectedParkTest()
        {
            NationalPark park = new NationalPark();
            Park selectedPark = park.GetSelectedPark(1000);

            Assert.AreEqual("name", selectedPark.Name);
            Assert.AreEqual("location", selectedPark.Location);
        }

        [TestMethod]
        public void GetCampgroundsInParkTest()
        {
            NationalPark park = new NationalPark();

            Campground[] campgrounds = park.GetCampgroundsInPark(1000);
            bool exists = false;
            int count = 0;

            foreach(Campground place in campgrounds)
            {
                if(place.Name=="name")
                {
                    exists = true;
                }
                count += 1;
            }

            CollectionAssert.AllItemsAreNotNull(campgrounds);
            Assert.AreEqual(true, exists);
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void GetOpenSites()
        {
            NationalPark park = new NationalPark();
            Site[] openSites = park.GetOpenSites(1000, 1000, from_date, to_date);
            bool exists = false;
            int count = 0;
            foreach(Site site in openSites)
            {
                if(site.Site_Id==1001)
                {
                    exists = true;
                    count += 1;
                }
            }

            Assert.AreEqual(true, exists);
            Assert.AreEqual(1, count);

            Site[] openSites2 = park.GetOpenSites(1000, 1000, DateTime.Today.AddDays(8).Date, DateTime.Today.AddDays(9).Date);
            int count2 = 0;
            foreach (Site site in openSites2)
            {
                    count2 += 1;
            }

            Assert.AreEqual(2, count2);
        }

        [TestMethod]
        public void CreateReservationTest()
        {
            NationalPark park = new NationalPark();

            int reservationID = park.CreateReservation(1001, "Joe", DateTime.Today.AddDays(8).Date, DateTime.Today.AddDays(9).Date);

            Assert.IsNotNull(reservationID);

            int lastReservationID = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                connection.Open();

                cmd = new SqlCommand(@"select reservation_id from reservation order by reservation_id desc;", connection);
                lastReservationID = Convert.ToInt32(cmd.ExecuteScalar());
            }

            Assert.AreEqual(lastReservationID, reservationID);
        }

        [TestMethod]
        public void ViewReservationsTest()
        {
            NationalPark park = new NationalPark();

            Reservation[] next30DayRes = park.ViewReservations(1000);
            bool exists = false;
            foreach(Reservation resi in next30DayRes)
            {
                if(resi.Name=="name")
                {
                    exists = true;
                }
            }

            Assert.AreEqual(1, next30DayRes.Length);
            Assert.AreEqual(true, exists);
        }
    }
}
