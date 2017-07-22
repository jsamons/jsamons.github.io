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
    public class WeatherDALTest
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalParkGeek;Integrated Security=True";

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd;
            }
        }
        [TestMethod]
        public void GetAllWeatherTest()
        {
            WeatherDAL test = new WeatherDAL();
            List<Weather> testList = test.GetAllWeather("CVNP");
            Assert.IsNotNull(testList);
        }
    }
}
