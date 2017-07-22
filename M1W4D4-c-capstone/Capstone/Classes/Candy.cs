using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public class Candy:VMItem
    {
        public Candy(string item, DollarAmount price):base(item, price)
        {
        }

        public override string ToString()
        {
            return "Enjoy that sugar rush.....before you crash.";
        }
    }
}
