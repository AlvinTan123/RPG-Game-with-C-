using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGame
{
    public class WeaponShop
    {
        public List<Weapon> Weapons { get; }

        public WeaponShop()
        {
            Weapons = new List<Weapon>
        {
            new Weapon("Sword", 3, 50,"Some Rusty sowrd... (+3 Damages)"),
            new Weapon("Axe", 4, 75,"It an Axe to chop wood? (+4 Damages)"),
            new Weapon("Bow", 2, 40,"Bow is for the weak, but I will take it haha! (+2 Damages)"),
            new Weapon("Thumb", 15, 100,"I kill a men with a thumb- Unknown chef (+15 Damages)"),
            new Weapon("Pencil", 20, 120,"Kill 3 men with a pencil - John Wick (+20 Damages)"),
            new Weapon("Wen Hau's cheat sword", 999, 2500,"One shot every enemy and boss yeah!!! (+999 Damages)")
        };
        }

        public void DisplayWeapons()
        {
            Console.WriteLine("Available Weapons:");
            for (int i = 0; i < Weapons.Count; i++)
            {
                Weapon weapon = Weapons[i];
                Console.WriteLine($"{i + 1}. {weapon.Name} (Price: {weapon.Price} coins)\n{weapon.Description}\n");
            }
        }
    }
}
