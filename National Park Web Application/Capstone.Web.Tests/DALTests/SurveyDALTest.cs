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
    public class SurveyDALTest
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
                cmd = new SqlCommand("insert into park values('ABCD', 'string', 'string', 1, 2, 3, 4, 'string', 5, 6, 'string', 'string', 'string', 7, 8);", connection);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into survey_result values('ABCD', 'test@test.com', 'Liquid', 'Vigorous');", connection);
                cmd.ExecuteNonQuery();
            }

        }
        [TestMethod]
        public void SubmitSurveyTest()
        {
            SurveySqlDAL newSurveyDal = new SurveySqlDAL();
            Survey testSurvey = new Survey();
            testSurvey.EmailAddress = "test@test.com";
            testSurvey.ActivityLevel = "Inactive";
            testSurvey.ParkCode = "CVNP";
            testSurvey.State = "TEST";
            bool testSurveySubmission = newSurveyDal.SubmitSurvey(testSurvey);

            Assert.AreEqual(true, testSurveySubmission);
        }
        [TestMethod]
        public void GetTopParksTest()
        {
            SurveySqlDAL newSurveyDal = new SurveySqlDAL();
            Dictionary<string, int> testDict = newSurveyDal.TopParks();

            Assert.AreEqual(true, testDict.ContainsKey("ABCD"));
        }
    }
}
