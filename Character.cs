using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleGame
{
    public enum CharacterClass
    {
        Warrior,
        Mage,
        Archer
    }
    public abstract class Character
    {
        private string name;
        private int health;
        private int attackpower;
        private int level;
        private int defense;
        private Weapon equippedWeapon;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int Attackpower
        {
            get { return attackpower; }
            set { attackpower = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public int Defense
        {
            get { return defense; }
            set { defense = value; }
        }

        public Weapon EquippedWeapon
        {
            get { return equippedWeapon; }
            set { equippedWeapon = value; }
        }

        public abstract void Attack(Character target);

        public abstract void Defend();

        public virtual void DisplayCharacter()
        {
            Console.WriteLine("Character Information:");
            Console.WriteLine("----------------------");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Health: {health}");
            Console.WriteLine($"Attack Power: {attackpower}");
            Console.WriteLine($"Level: {level}");
            Console.WriteLine($"Equipped Weapon: {equippedWeapon?.Name ?? "None"}");
        }
    }
}