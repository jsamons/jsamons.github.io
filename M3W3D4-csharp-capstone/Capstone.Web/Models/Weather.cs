using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }
        public int FiveDayForeCast { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string Forecast { get; set; }

        public Dictionary<string, string> forecastMessage = new Dictionary<string, string>()
        {
            { "sunny", "Don't forget to pack sunblock." },
            {"rain", "Make sure you pack rain gear and wear waterproof shoes." },
            {"thunderstorms",  "Seek shelter and avoid hiking on exposed ridges." },
            {"snow", "Pack snowshoes." }
        };

    }
}