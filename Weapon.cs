using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGame
{
    public class Weapon
    {
        public string Name { get; private set; }
        public int AttackPower { get; private set; }
        public int Price { get; private set; }

        public string Description { get; private set; }

        public Weapon(string name, int attackPower, int price, string description)
        {
            Name = name;
            AttackPower = attackPower;
            Price = price;
            Description = description;
        }
    }
}
