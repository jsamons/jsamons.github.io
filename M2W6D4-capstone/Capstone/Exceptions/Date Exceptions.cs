using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone.Exceptions
{
    class DateExceptions
    {
        private DateTime fromDateTime;

        public string InvalidFromDate(string fromDate, int campgroundID)
        {
            string message="";
            try
            {
                    fromDateTime = Convert.ToDateTime(fromDate);
                    if (fromDateTime < DateTime.Now.Date)
                    {
                        Console.Clear();
                        message = "Cannot choose a past date. Please try again.";
                    }
                    else if (fromDateTime > DateTime.Now.AddMonths(6).Date)
                    {
                        Console.Clear();
                        message = "Cannot book a reservation more than 6 months in advance. Please try again.";
                    }
            }

            catch(Exception)
            {
                Console.Clear();
                message = "Incorrect format. Please try again.";
            }

            return message;
        }

        public string InvalidToDate(string toDate, int campgroundID)
        {
            string message = "";
            DateTime toDateTime;
            try
            {
                toDateTime = Convert.ToDateTime(toDate);
                if(toDateTime<fromDateTime)
                {
                    message = "Cannot choose a departure date that comes before the arrival date.";
                }               
                else if (toDateTime > DateTime.Now.AddMonths(6).Date)
                {
                    Console.Clear();
                    message = "Cannot book a reservation more than 6 months in advance. Please try again.";
                }
            }

            catch (Exception)
            {
                Console.Clear();
                message = "Incorrect format. Please try again.";
            }

            return message;
        }
    }
}
