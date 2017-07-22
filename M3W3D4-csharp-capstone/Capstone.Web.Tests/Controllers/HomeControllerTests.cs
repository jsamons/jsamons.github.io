using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using System.Transactions;
using System.Data.SqlClient;
using System.Web.Mvc;


namespace Capstone.Web.Controllers.Tests
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
        //Not sure how to test this, returns a list and in
        [TestMethod()]
        public void IndexTest()
        {
            NatParksSqlDAL parkDal = new NatParksSqlDAL();
            HomeController controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult;
            List<NationalPark> list = parkDal.GetAllParks();
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}