using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public class VMItem
    {
        private string item;
        private DollarAmount price;
        private int quantity;

        public VMItem(string item, DollarAmount price)
        {
            this.item = item;
            this.price = price;
            quantity = 5;
        }

        public string Item
        {
            get
            {
                return item;
            }
        }

        public DollarAmount Price
        {
            get
            {
                return price;
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }
    }
}
