using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGame
{
    public class Currency
    {
        public double Amount { get; set; }

        public Currency(double initialAmount)
        {
            Amount = initialAmount;
        }

        public void AddAmount(double value)
        {
            Amount += value;
        }

        public void SubtractAmount(double value)
        {
            Amount -= value;
        }
    }
}
