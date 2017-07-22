using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models
{
    public class Survey
    {
        public string ParkCode { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public string State { get; set; }

        [Required]
        public string ActivityLevel { get; set; }


        public static List<SelectListItem> States
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem { Text = "Alabama", Value = "AL" },
                    new SelectListItem { Text = "Alaska", Value = "AK" },
                    new SelectListItem { Text = "Arizona", Value = "AZ" },
                    new SelectListItem { Text = "Arkansas", Value = "AR" },
                    new SelectListItem { Text = "California", Value = "CA" },
                    new SelectListItem { Text = "Colorado", Value = "CO" },
                    new SelectListItem { Text = "Connecticut", Value = "CT" },
                    new SelectListItem { Text = "Delaware", Value = "DE" },
                    new SelectListItem { Text = "Florida", Value = "FL" },
                    new SelectListItem { Text = "Georgia", Value = "GA" },
                    new SelectListItem { Text = "Hawaii", Value = "HI" },
                    new SelectListItem { Text = "Idaho", Value = "ID" },
                    new SelectListItem { Text = "Illinois", Value = "IL" },
                    new SelectListItem { Text = "Indiana", Value = "IN" },
                    new SelectListItem { Text = "Iowa", Value = "IA" },
                    new SelectListItem { Text = "Kansas", Value = "KS" },
                    new SelectListItem { Text = "Kentucky", Value = "KY" },
                    new SelectListItem { Text = "Louisiana", Value = "LA" },
                    new SelectListItem { Text = "Maine", Value = "ME" },
                    new SelectListItem { Text = "Maryland", Value = "MD" },
                    new SelectListItem { Text = "Massachusetts", Value = "MA" },
                    new SelectListItem { Text = "Michigan", Value = "MI" },
                    new SelectListItem { Text = "Minnesota", Value = "MN" },
                    new SelectListItem { Text = "Mississippi", Value = "MS" },
                    new SelectListItem { Text = "Missouri", Value = "MO" },
                    new SelectListItem { Text = "Montana", Value = "MT" },
                    new SelectListItem { Text = "Nebraska", Value = "NE" },
                    new SelectListItem { Text = "Nevada", Value = "NV" },
                    new SelectListItem { Text = "New Hampshire", Value = "NH" },
                    new SelectListItem { Text = "New Jersey", Value = "NM" },
                    new SelectListItem { Text = "Nebraska", Value = "NE" },
                    new SelectListItem { Text = "New York", Value = "NY" },
                    new SelectListItem { Text = "North Carolina", Value = "NC" },
                    new SelectListItem { Text = "North Dakota", Value = "ND" },
                    new SelectListItem { Text = "Ohio", Value = "OH" },
                    new SelectListItem { Text = "Oklahoma", Value = "OK" },
                    new SelectListItem { Text = "Oregon", Value = "OR" },
                    new SelectListItem { Text = "Pennsylvania", Value = "PA" },
                    new SelectListItem { Text = "Rhode Island", Value = "RI" },
                    new SelectListItem { Text = "South Carolina", Value = "SC" },
                    new SelectListItem { Text = "South Dakota", Value = "SD" },
                    new SelectListItem { Text = "Tennessee", Value = "TN" },
                    new SelectListItem { Text = "Texas", Value = "TX" },
                    new SelectListItem { Text = "Utah", Value = "UT" },
                    new SelectListItem { Text = "Vermont", Value = "VT" },
                    new SelectListItem { Text = "Virginia", Value = "VA" },
                    new SelectListItem { Text = "Washington", Value = "WA" },
                    new SelectListItem { Text = "West Virginia", Value = "WV" },
                    new SelectListItem { Text = "Wisconsin", Value = "WI" },
                    new SelectListItem { Text = "Wyoming", Value = "WY" },
                };
            }
        }
        public static List<SelectListItem> NationalParks
        {
            get
            {

                NatParksSqlDAL parkDal = new NatParksSqlDAL();
                List<NationalPark> list = parkDal.GetAllParks();
                List<SelectListItem> selectList = new List<SelectListItem>();
                foreach (NationalPark thisPark in list)
                {
                    SelectListItem thisItem = new SelectListItem();
                    thisItem.Text = thisPark.ParkName;
                    thisItem.Value = thisPark.ParkCode;
                    selectList.Add(thisItem);
                }
                return selectList;
            }

        }
    }
}


