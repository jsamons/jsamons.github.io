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
    [TestClass()]
    public class ParkDALTests
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
                cmd = new SqlCommand("select * from park", connection);
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetAllParksTest()
        {
            ParkDAL test = new ParkDAL();

            List<Park> parks = test.GetAllParks();
            bool containsName = false;
            foreach(Park place in parks)
            {
                if(place.Name == "name")
                {
                    containsName = true;
                }
            }
            Assert.IsNotNull(parks);
            Assert.AreEqual(true, containsName);
        }

        [TestMethod]
        public void GetParkTest()
        {
            ParkDAL test = new ParkDAL();

            Park park = test.GetPark(1000);

            Assert.AreEqual("name", park.Name);
        }
    }
}
