using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Campground
    {

        public int Campground_Id { get; set; }
        public int Park_Id { get; set; }
        public string Name { get; set; }
        public int Opening_Month { get; set; }
        public int Closing_Month { get; set; }
        public decimal Daily_Fee { get; set; }

        public bool IsOffseason(DateTime date)
        {
            try
            {
                if(date.Month>= Closing_Month || date.Month<Opening_Month)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception)
            {
                throw;
            }

        }
    }
}
