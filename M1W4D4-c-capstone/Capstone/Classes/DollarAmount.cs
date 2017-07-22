using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class DollarAmount
    {
        private int totalAmountInCents;

        public int Cents
        {
            get
            {
                int remainder = totalAmountInCents % 100;
                return remainder;
            }
        }

        public int Dollars
        {
            get
            {
                int quotient = totalAmountInCents / 100;
                return quotient;
            }
        }

        public bool IsNegative
        {
            get
            {
                return totalAmountInCents < 0;
            }
        }

        public DollarAmount(int totalCents)
        {
            totalAmountInCents = totalCents;
        }

        public DollarAmount(int dollars, int cents)
        {
            totalAmountInCents = (dollars * 100) + cents;
        }

        public DollarAmount Minus(DollarAmount amountToSubtract)
        {
            int difference = this.totalAmountInCents - amountToSubtract.totalAmountInCents;

            return new DollarAmount(difference);
        }

        public DollarAmount Plus(DollarAmount amountToAdd)
        {
            int newTotal = this.totalAmountInCents + amountToAdd.totalAmountInCents;

            return new DollarAmount(newTotal);
        }

        public override string ToString()
        {
            if (Cents < 10)
            {
                return $"${Dollars}.0{Cents}";
            }
            return $"${Dollars}.{Cents}";
        }

        public int TotalAmountInCents
        {
            get
            {
                return totalAmountInCents;
            }
        }
    }
}
