using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.DAL;
using System.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.Tests.DALTests
{
    [TestClass]
    public class NatParksDALTest
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalParkGeek;Integrated Security=True";
        private TransactionScope tran;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd;
                cmd = new SqlCommand(@"insert into park (parkCode, parkName, state, acreage, elevationInFeet, milesOfTrail, numberOfCampsites, climate, yearFounded, annualVisitorCount, inspirationalQuote, inspirationalQuoteSource, ParkDescription, entryFee, numberOfAnimalSpecies) values ('ABCD', 'string', 'string', 1, 2, 3, 4, 'string', 5, 6, 'string', 'string', 'string', 7, 8);", connection);
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
            NatParksSqlDAL test = new NatParksSqlDAL();
            List<NationalPark> testList = test.GetAllParks();
            Assert.IsNotNull(testList);
        }
        [TestMethod]
        public void GetSingleParkTest()
        {
            NatParksSqlDAL test2 = new NatParksSqlDAL();
            NationalPark testPark = test2.GetSinglePark("ABCD");
            Assert.IsNotNull(testPark);
        }
    }
}
