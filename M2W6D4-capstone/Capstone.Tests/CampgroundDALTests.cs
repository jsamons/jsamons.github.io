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
    public class CampgroundDALTests
    {
        private TransactionScope tran;
        private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        
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
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetCampgroundsTest()
        {
            CampgroundDAL test = new CampgroundDAL();

            List<Campground> campgrounds = test.GetCampgrounds(1000);

            bool exists = false;

            foreach(Campground ground in campgrounds)
            {
                if(ground.Name == "name")
                {
                    exists = true;
                }
            }

            Assert.IsNotNull(campgrounds);
            Assert.AreEqual(true, exists);
        }
    }
}
