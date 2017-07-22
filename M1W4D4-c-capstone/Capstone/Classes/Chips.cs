﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public class Chips:VMItem
    {
        public Chips(string item, DollarAmount price):base(item, price)
        {
        }

        public override string ToString()
        {
            return "Salty goodness.";
        }
    }
}
