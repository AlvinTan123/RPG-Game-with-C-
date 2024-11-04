using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGame
{
    public class Boss : Character
    {
        public int HeavyDamage { get; private set; }
        public int HealAmount { get; private set; }
        public int SpellDamage { get; private set; }

        public Boss(string name, int health, int level, int attack, int heavyDamage, int healAmount, int spellDamage)
        {
            Name = name;
            Health = health;
            Level = level;
            Attackpower = attack;
            HeavyDamage = heavyDamage;
            HealAmount = healAmount;
            SpellDamage = spellDamage;
        }

        public override void Attack(Character target)
        {
            Console.WriteLine($"{Name} attacks {target.Name}!");

            // Implement attack logic for the boss character
            int randomNumber = new Random().Next(1, 11);

            // Use a default attack power for the boss
            int baseDamage = Attackpower - target.Defense;

            if (randomNumber <= 6)
            {
                // 60% chance of regular attack
                target.Health -= baseDamage;
                Console.WriteLine($"* {Name} hits {target.Name} for {baseDamage} damage!");
            }
            else if (randomNumber <= 9)
            {
                // 30% chance of heavy damage
                int heavyDamage = baseDamage * 3; // triple the base damage
                target.Health -= heavyDamage;
                Console.WriteLine($"* {Name} uses Heavy Damage! {target.Name} takes {heavyDamage} damage!");
            }
            else
            {
                // 10% chance of spell attack
                int spellDamage = baseDamage * 2; // Double the base damage
                target.Health -= spellDamage;
                Console.WriteLine($"* {Name} uses Spell Attack! {target.Name} takes {spellDamage} damage!");
            }

            if (target.Health <= 0)
            {
                Console.WriteLine($"* {target.Name} has been defeated!");
            }
        }

        public void Heal()
        {
            Console.WriteLine($"* {Name} uses Heal!");
            Health += HealAmount;
            Console.WriteLine($"* {Name} heals for {HealAmount} points.");
        }

        public override void Defend()
        {
            Console.WriteLine($"* {Name} defends!");
            // Implement defend logic for the boss character
        }

        public override void DisplayCharacter()
        {
            Console.WriteLine("Boss Information:");
            Console.WriteLine("------------------");
            Console.WriteLine($"* Name: {Name}");
            Console.WriteLine($"* Health: {Health}");
            Console.WriteLine($"* Level: {Level}");
            Console.WriteLine($"* Attack Power: {Attackpower}");
            Console.WriteLine($"* Heavy Damage: {HeavyDamage}");
            Console.WriteLine($"* Heal Amount: {HealAmount}");
            Console.WriteLine($"* Spell Damage: {SpellDamage}");
        }

        // Method to generate a random boss
        public static Boss GenerateRandomBoss(int lvl)
        {
            if (lvl == 1)
            {
                Random random = new Random();
                string[] bossNames = { "Dragon", "Demon", "Warlord", "Behemoth" };
                int index = random.Next(bossNames.Length);
                string name = bossNames[index];
                int health = random.Next(85, 121); 
                int attack = random.Next(20, 27);
                int level = random.Next(6, 11); 
                int heavyDamage = random.Next(30,41); 
                int healAmount = random.Next(15, 25); 
                int spellDamage = random.Next(35, 50); 

                return new Boss(name, health, level, attack, heavyDamage, healAmount, spellDamage);
            }
            else if (lvl == 2)
            {
                Random random = new Random();
                string[] bossNames = { "Ancient Dragon", "Archdemon", "Warlord King", "Colossal Behemoth" };
                int index = random.Next(bossNames.Length);
                string name = bossNames[index];
                int health = random.Next(150, 201); 
                int attack = random.Next(30, 38);
                int level = random.Next(8, 13); 
                int heavyDamage = random.Next(35, 46);
                int healAmount = random.Next(30, 35); 
                int spellDamage = random.Next(40, 55); 

                return new Boss(name, health, level, attack, heavyDamage, healAmount, spellDamage);
            }
            else if (lvl == 3)
            {
                Random random = new Random();
                string[] bossNames = { "Serpent King", "Shadowlord", "Dreadnought", "Ancient Wyrm" };
                int index = random.Next(bossNames.Length);
                string name = bossNames[index];
                int health = random.Next(200, 251);
                int attack = random.Next(35, 43);
                int level = random.Next(14, 19);
                int heavyDamage = random.Next(45, 56);
                int healAmount = random.Next(40, 46);
                int spellDamage = random.Next(50, 65);

                return new Boss(name, health, level, attack, heavyDamage, healAmount, spellDamage);
            }
            else if (lvl == 4)
            {
                Random random = new Random();
                string[] bossNames = { "Lord of Shadows", "Doombringer", "Titanus", "Ethereal Specter" };
                int index = random.Next(bossNames.Length);
                string name = bossNames[index];
                int health = random.Next(250, 301);
                int attack = random.Next(40, 48);
                int level = random.Next(20, 25);
                int heavyDamage = random.Next(55, 66);
                int healAmount = random.Next(50, 56);
                int spellDamage = random.Next(60, 75);

                return new Boss(name, health, level, attack, heavyDamage, healAmount, spellDamage);
            }
            else if (lvl >= 5)
            {
                Random random = new Random();
                string[] bossNames = { "Apocalypse", "Behemoth King", "Nightmare Lord", "Soul Devourer" };
                int index = random.Next(bossNames.Length);
                string name = bossNames[index];
                int health = random.Next(300, 351);
                int attack = random.Next(45, 53);
                int level = random.Next(26, 30);
                int heavyDamage = random.Next(65, 76);
                int healAmount = random.Next(60, 66);
                int spellDamage = random.Next(70, 85);

                return new Boss(name, health, level, attack, heavyDamage, healAmount, spellDamage);
            }

            return null;
        }
    }
}