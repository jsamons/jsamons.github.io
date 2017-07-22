using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;
using Capstone.Exceptions;

namespace Capstone
{
    class NationalParkCLI
    {
        NationalPark NP;

        public NationalParkCLI(NationalPark tester)
        {
            NP = tester;
        }

        public void Run()
        {
            string[] parks = NP.GetParks();
            bool end = false;
            Console.WriteLine("Welcome to the National Park Campsite Reservation program!");
            Console.WriteLine("");
            while (!end)
            {
                int x;
                int checkedInput;
                Console.WriteLine("Please choose a park:");
                for (x = 0; x < parks.Length; x++)
                {
                    Console.WriteLine("{0}) {1}", x + 1, parks[x]);
                }
                Console.WriteLine("{0}) Quit", x + 1);
                string input = Console.ReadLine();
                checkedInput = InputIntCheck(input, 1, parks.Length + 1);
                if (checkedInput <= x && checkedInput > 0)
                {
                    Console.Clear();
                    DisplayPark(checkedInput);
                    Console.Clear();
                }
                else if (checkedInput == x + 1)
                {
                    end = true;
                    Console.Clear();
                    Console.WriteLine("Thank you for using the Nation Park Campsite Reservation program!");
                    Console.Write("Press enter to exit.");
                    Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Sorry, that was not a valid selection.");
                    Console.WriteLine("");
                }
            }
        }

        public void DisplayPark(int parkInput)
        {
            bool end = false;
            while (!end)
            {
                int checkedInput;
                Park selectedPark;
                selectedPark = NP.GetSelectedPark(parkInput);
                Console.WriteLine("{0} National Park", selectedPark.Name);
                Console.WriteLine("{0,-20} {1}", "Location:", selectedPark.Location);
                Console.WriteLine("{0,-20} {1}", "Established:", selectedPark.Establish_Date.Date.ToShortDateString());
                Console.WriteLine("{0,-20} {1} sq km", "Area:", string.Format("{0:n0}", selectedPark.Area));
                Console.WriteLine("{0,-20} {1}", "Annual Visitors:", string.Format("{0:n0}", selectedPark.Visitors));
                Console.WriteLine();
                Console.WriteLine(selectedPark.Description);
                Console.WriteLine();
                Console.WriteLine("Select a command: ");
                Console.WriteLine("1) View campgrounds");
                Console.WriteLine("2) Return to main menu");
                string input = Console.ReadLine();
                checkedInput = InputIntCheck(input, 1, 2);
                if (checkedInput == 1)
                {
                    Console.Clear();
                    CampgroundsMenu(parkInput);
                    end = true;
                }
                else if (checkedInput == 2)
                {
                    end = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Sorry, that was not a valid selection.");
                    Console.WriteLine("");
                }
            }
        }

        public void CampgroundsMenu(int parkInput)
        {
            bool end = false;
            while (!end)
            {
                int checkedInput;
                Park selectedPark;
                selectedPark = NP.GetSelectedPark(parkInput);
                Console.WriteLine("{0} National Park Campgrounds", selectedPark.Name);
                Console.WriteLine();
                PrintCampgrounds(parkInput);
                Console.WriteLine("Select a command");
                Console.WriteLine("1) Select campground");
                Console.WriteLine("2) Search all campgrounds");
                Console.WriteLine("3) See all reservations for the next 30 days");
                Console.WriteLine("4) Return to main menu");
                string input = Console.ReadLine().ToString();
                checkedInput = InputIntCheck(input, 1, 4);
                if (checkedInput == 1)
                {
                    Console.Clear();
                    end = SelectCampground(parkInput);
                }
                else if (checkedInput == 2)
                {
                    Console.Clear();
                    end = SearchCampground(parkInput, 0);
                }
                else if (checkedInput == 3)
                {
                    Console.Clear();
                    ViewReservation(parkInput);
                }
                else if (checkedInput == 4)
                {
                    end = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Sorry, that was not a valid selection.");
                    Console.WriteLine("");
                }

            }
        }

        public bool SelectCampground(int parkInput)
        {
            bool result = false;
            bool end = false;
            int campgroundInput = 0;

            while (!end)
            {
                int numberOfCampgrounds = PrintCampgrounds(parkInput);
                Console.Write("Which campground? (Enter 0 to cancel) ");
                string input = Console.ReadLine().ToString();
                campgroundInput = InputIntCheck(input, 0, numberOfCampgrounds);
                if (campgroundInput == 0)
                {
                    Console.Clear();
                    return false;
                }
                else if (campgroundInput < 0)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input. Please try again.");
                }
                else
                {
                    Console.Clear();
                    end = true;
                }
            }

            result = SearchCampground(parkInput, campgroundInput);
            return result;
        }

        public bool SearchCampground(int parkInput, int campgroundInput)
        {
            int confirmationID = 0;
            int siteInput = 0;

            Site[] selectedSites;
            Campground selectedCampground;
            Campground[] allCampgrounds = NP.GetCampgroundsInPark(parkInput);

            int campgroundID = 0;

            if (campgroundInput != 0)
            {
                selectedCampground = allCampgrounds[campgroundInput - 1];
                campgroundID = selectedCampground.Campground_Id;
            }

            bool end = false;
            string fromDateString = "";
            DateExceptions exception = new DateExceptions();

            while (!end)
            {
                Console.WriteLine("What is the arrival date? (yyyy/mm/dd) ");
                fromDateString = Console.ReadLine();
                string checkDate = exception.InvalidFromDate(fromDateString, campgroundID);
                if (checkDate == "")
                {
                    end = true;
                }
                else
                {
                    Console.WriteLine(checkDate);
                }
            }

            DateTime fromDate = DateTime.Parse(fromDateString);

            string toDateString = "";
            end = false;

            while (!end)
            {
                Console.WriteLine("What is the departure date? (yyyy/mm/dd) ");
                toDateString = Console.ReadLine();
                string checkDate = exception.InvalidToDate(toDateString, campgroundID);
                if (checkDate == "")
                {
                    end = true;
                }
                else
                {
                    Console.WriteLine(checkDate);
                }
            }

            DateTime toDate = DateTime.Parse(toDateString);

            //Console.Write("Would you like to add advanced search criteria? (y/n) ");

            //if (Console.ReadLine().ToString().ToLower() == "y")
            //{
            //    AdvancedSearch();
            //}

            Console.Clear();

            selectedSites = NP.GetOpenSites(parkInput, campgroundID, fromDate, toDate);

            if (selectedSites.Length == 0)
            {
                Console.WriteLine("No campsites currently available for this date range. Please select a different date range or location.");
                Console.WriteLine();
                return false;
            }
            else
            {
                Console.WriteLine("Results matching your search criteria: ");
                Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}", "Site No.", "Max Occup.", "Accessible?", "Max RV Length", "Utility", "Cost");

                CampgroundDAL temp = new CampgroundDAL();
                int count = 0;
                List<Site> topFiveSites= new List<Site>();

                foreach (Site location in selectedSites)
                {
                    Campground campground = temp.GetSingleCampground(location.Campground_Id);
                    decimal cost = campground.Daily_Fee;
                    int numOfDays = toDate.Subtract(fromDate).Days;
                    decimal totalCost = cost * numOfDays;
                    if (!campground.IsOffseason(fromDate) && !campground.IsOffseason(toDate))
                    {
                        Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}${5,-15:0.00}", location.Site_Id, location.Max_Occupancy, location.WheelchairAccessible, location.Max_Rv_Length, location.HasUtilities, totalCost);
                        count += 1;
                        topFiveSites.Add(location);
                        if(count==5)
                        {
                            break;
                        }
                    }
                }

                if (count == 0)
                {
                    Console.Clear();
                    Console.WriteLine("There are no available sites in this date range. Returning to campground menu.");
                    Console.WriteLine();
                    return false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Which site should be reserved? (Enter 0 to cancel)");

                    string input = Console.ReadLine().ToString();
                    Site [] selectableSites = topFiveSites.ToArray();
                    siteInput = SiteInputCheck(input, selectableSites);

                    if (siteInput == 0)
                    {
                        Console.Clear();
                        return false;
                    }
                    else
                    {

                        Console.Write("What name should the reservation be under? ");
                        string reservationName = Console.ReadLine();
                        Console.WriteLine();
                        confirmationID = NP.CreateReservation(siteInput, reservationName, fromDate, toDate);
                        Console.WriteLine("The reservation has been made and the confirmation ID is {0}.", confirmationID);
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                        return true;
                    }
                }
            }
        }

        public void ViewReservation(int parkID)
        {
            Reservation[] booked = NP.ViewReservations(parkID);
            Console.WriteLine("{0, -20}{1, -20}{2, -20}", "Site ID", "From Date", "To Date");
            for (int x = 0; x < booked.Length; x++)
            {
                Console.WriteLine("{0, -20}{1, -20}{2, -20}", booked[x].Site_Id, booked[x].From_Date.Date.ToShortDateString(), booked[x].To_Date.Date.ToShortDateString());
            }
            Console.WriteLine();
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }
        
        //public void AdvancedSearch()
        //{
        //    int checkedInput = 1;
        //    int maxOcc;
        //    bool acc = false;
        //    int maxRV;
        //    bool util = false;
        //    int maxCost;
        //    Console.Write("Do you have a requirement for a maximum occupancy? (y/n) ");
        //    if (Console.ReadLine().ToString().ToLower() == "y")
        //    {
        //        Console.WriteLine();
        //        Console.Write("What is the maximum occupancy required? ");
        //        string maxOccStr = Console.ReadLine();
        //        checkedInput = InputIntCheck(maxOccStr, 0, 1000);
        //        if (checkedInput == 0)
        //        {
        //            //is problem try again
        //        }
        //        else
        //        {
        //            maxOcc = checkedInput;
        //        }
        //    }
        //    Console.Write("Do you have a requirement for accessibility? (y/n) ");
        //    if (Console.ReadLine().ToString().ToLower() == "y")
        //    {
        //        acc = true;
        //    }
        //    Console.Write("Do you have a requirement for a maximum RV length? (y/n) ");
        //    if (Console.ReadLine().ToString().ToLower() == "y")
        //    {
        //        Console.WriteLine();
        //        Console.Write("What is the maximum RV length required? ");
        //        string maxRVStr = Console.ReadLine();
        //        checkedInput = InputIntCheck(maxRVStr, 0, 1000);
        //        if (checkedInput == 0)
        //        {
        //            //is problem try again
        //        }
        //        else
        //        {
        //            maxRV = checkedInput;
        //        }
        //    }
        //    Console.Write("Do you have a requirement for utilities? (y/n) ");
        //    if (Console.ReadLine().ToString().ToLower() == "y")
        //    {
        //        util = true;
        //    }
        //    Console.Write("Do you have a requirement for a maximum cost? (y/n) ");
        //    if (Console.ReadLine().ToString().ToLower() == "y")
        //    {
        //        Console.WriteLine();
        //        Console.Write("What is the maximum cost allowed? ");
        //        string maxCostStr = Console.ReadLine();
        //        checkedInput = InputIntCheck(maxCostStr, 0, 1000);
        //        if (checkedInput == 0)
        //        {
        //            //is problem try again
        //        }
        //        else
        //        {
        //            maxCost = checkedInput;
        //        }
        //    }
        //    Console.WriteLine("/n/n/nPass those values somehow, hit enter.");
        //    Console.ReadLine();
        //}

        public int PrintCampgrounds(int parkInput)
        {
            Campground[] campgrounds;
            campgrounds = NP.GetCampgroundsInPark(parkInput);
            Console.WriteLine("{0, 10}{1, 35}{2, 21}{3, 24}", "Name", "Open", "Close", "Daily Fee");
            for (int x = 0; x < campgrounds.Length; x++)
            {
                Console.WriteLine("#{0, -5}{1, -35}{2, -20}{3, -20}${4, -20:0.00}", x + 1, campgrounds[x].Name, NumberToMonth(campgrounds[x].Opening_Month), NumberToMonth(campgrounds[x].Closing_Month), campgrounds[x].Daily_Fee);
            }
            Console.WriteLine();
            return campgrounds.Length;
        }

        public int InputIntCheck(string input, int low, int high)
        {
            int result;
            try
            {
                result = int.Parse(input);
                if (result < low || result > high)
                {
                    result = -1;
                }
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        public int SiteInputCheck(string input, Site[] selectedSites)
        {
            bool validChoice = false;
            int result = 0;
            string inputedChoice = input;

            while (!validChoice)
            {
                try
                {
                    result = int.Parse(inputedChoice);

                    foreach (Site site in selectedSites)
                    {
                        if (result == 0)
                        {
                            validChoice = true;
                            break;
                        }
                        else if (site.Site_Id == result)
                        {
                            validChoice = true;
                            break;
                        }
                    }

                    if (!validChoice)
                    {
                        Console.WriteLine("Invalid site. Please try again or enter 0 to cancel.");
                        inputedChoice = Console.ReadLine();
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid site. Please try again or enter 0 to cancel.");
                    inputedChoice = Console.ReadLine();
                }
            }
            return result;

        }

        public string NumberToMonth(int monthNumber)
        {
            switch(monthNumber)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                default:
                    return "December";
            }
        }
        
        //    public bool Question(string question, string typeToCheck, string value1, string value2)
        //    {
        //        return false;
        //    }
        //
    }
}
