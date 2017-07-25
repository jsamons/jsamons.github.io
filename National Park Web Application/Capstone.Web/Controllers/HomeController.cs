using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        NatParksSqlDAL parkDal = new NatParksSqlDAL();
        WeatherDAL weatherDal = new WeatherDAL();

        // GET: Home
        public ActionResult Index()
        {
            Session["View"] = "Index";
            List<NationalPark> list = parkDal.GetAllParks();
            return View("Index", list);
        }
        public ActionResult TempChange(string temp)
        {
            Session["TempUnit"] = temp;          
            string view = Session["View"] as string;
            if (view == "Detail")
            {
                string parkCode = Session["ParkCode"] as string;
                return RedirectToAction("Detail", "Home", new { id = parkCode });
            }
            else if(view=="SurveyResult")
            {
                Survey survey = Session["Survey"] as Survey;
                return SurveyResult(survey);
            }
            else
            {
                return RedirectToAction(view);
            }
        }

        public ActionResult Detail(string id)
        {
            Session["View"] = "Detail";           
            List<Weather> list = weatherDal.GetAllWeather(id);
            ViewBag.WeatherList = list;
            NationalPark park = parkDal.GetSinglePark(id);
            Session["ParkCode"] = id;
            return View(park);
        }

        public ActionResult Survey()
        {
            Session["View"] = "Survey";
            return View();
        }

        [HttpPost]
        public ActionResult SurveyResult(Survey survey)
        {
            if(!ModelState.IsValid)
            {
                return View("Survey", survey);
            }
            Session["View"] = "SurveyResult";
            Session["Survey"] = survey;
            SurveySqlDAL dal = new SurveySqlDAL();
            dal.SubmitSurvey(survey);
            Dictionary<string, int> topParks = dal.TopParks();
            Session["TopParks"] = topParks;
            List<NationalPark> parks = new List<NationalPark>();
            foreach (KeyValuePair<string,int> kvp in topParks)
            {
                NationalPark park = parkDal.GetSinglePark(kvp.Key);
                parks.Add(park);
            }     
            return View("SurveyResult", parks);
        }
    }
}