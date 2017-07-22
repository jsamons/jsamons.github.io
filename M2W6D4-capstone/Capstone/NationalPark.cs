using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone
{
    public class NationalPark
    {
        public string[] GetParks()
        {
            ParkDAL dalname = new ParkDAL();
            List<Park> testList = dalname.GetAllParks();
            string[] result = new string[testList.Count()];
            int x = 0;
            foreach (Park parkName in testList)
            {
                result[x] = parkName.Name;
                x++;
            }
            return result;
        }

        public Park GetSelectedPark(int parkID)
        {
            ParkDAL dalname = new ParkDAL();
            return dalname.GetPark(parkID);
        }

        public Campground[] GetCampgroundsInPark(int parkID)
        {
            CampgroundDAL dalname = new CampgroundDAL();
            List<Campground> testList = dalname.GetCampgrounds(parkID);
            return testList.ToArray();
        }

        public Site[] GetOpenSites(int parkID, int campgroundID, DateTime fromdate, DateTime todate)
        {
            ReservationDAL dalname = new ReservationDAL();
            List<Site> testList = dalname.GetOpenings(parkID, campgroundID, fromdate, todate);
            return testList.ToArray();
        }

        public int CreateReservation(int siteID, string reservationName, DateTime fromdate, DateTime todate)
        {
            ReservationDAL dalname = new ReservationDAL();
            Reservation toAdd = new Reservation();
            toAdd.Reservation_Id = 0;
            toAdd.Site_Id = siteID;
            toAdd.Name = reservationName;
            toAdd.From_Date = fromdate;
            toAdd.To_Date = todate;
            return dalname.InsertReservation(toAdd);
        }

        public Reservation[] ViewReservations(int parkID)
        {
            ReservationDAL dalname = new ReservationDAL();
            List<Reservation> testList = dalname.GetNext30DaysReservations(parkID);
            return testList.ToArray();
        } 
    }
}
