using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleGame
{
    public class Enemy : Character
    {
        private Random random;

        public Enemy(string name, int health, int attack, int level)
        {
            Name = name;
            Health = health;
            Attackpower = attack;
            Level = level;
            random = new Random();
        }

        public override void Attack(Character target)
        {
            Console.WriteLine($"{Name} attacks {target.Name}!");

            // Implement attack logic for the enemy character
            // Use a default attack power for the enemy
            int baseDamage = Attackpower - target.Defense;
            int attackType = random.Next(1, 4); // Randomly select a number between 1 and 3

            if (attackType == 1)
            {
                int spellChance = random.Next(1, 4); // Randomly select a number between 1 and 3
                if (spellChance == 1)
                {
                    int spellDamage = baseDamage * 2; // Double the base damage
                    target.Health -= spellDamage;
                    Console.WriteLine($"{target.Name} is hit by a powerful spell and takes {spellDamage} damage!");
                }
                else
                {
                    target.Health -= baseDamage;
                    Console.WriteLine($"{target.Name} is hit by a regular attack and takes {baseDamage} damage!");
                }
            }
            else if (attackType == 2)
            {
                int criticalChance = random.Next(1, 6); // Randomly select a number between 1 and 5
                if (criticalChance == 1)
                {
                    int criticalDamage = baseDamage * 3; // Triple the base damage
                    target.Health -= criticalDamage;
                    Console.WriteLine($"{target.Name} is hit by a critical attack and takes {criticalDamage} damage!");
                }
                else
                {
                    target.Health -= baseDamage;
                    Console.WriteLine($"{target.Name} is hit by a regular attack and takes {baseDamage} damage!");
                }
            }
            else if (attackType == 3)
            {
                int poisonChance = random.Next(1, 4); // Randomly select a number between 1 and 3
                if (poisonChance == 1)
                {
                    int poisonDamage = baseDamage / 2; // Half the base damage
                    target.Health -= poisonDamage;
                    Console.WriteLine($"{target.Name} is poisoned and takes {poisonDamage} damage!");
                }
                else
                {
                    target.Health -= baseDamage;
                    Console.WriteLine($"{target.Name} is hit by a regular attack and takes {baseDamage} damage!");
                }
            }

            if (target.Health <= 0)
            {
                Console.WriteLine($"{target.Name} has been defeated!");
            }
        }

        public void AdjustStats()
        {
            // Increase enemy's stats based on the level
            Health += Level * 10;
            Attackpower += Level * 5;
            Defense += Level * 2;
        }

        public override void Defend()
        {
            Console.WriteLine($"{Name} defends!");
            // Implement defend logic for the enemy character
        }

        public override void DisplayCharacter()
        {
            Console.WriteLine("Enemy Information:");
            Console.WriteLine("--------------------");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Health: {Health}");
            Console.WriteLine($"Attack Power: {Attackpower}");
            Console.WriteLine($"Level: {Level}");
        }

        // Method to generate a random enemy
        public static Enemy GenerateRandomEnemy(int lvl)
        {
            if (lvl == 1)
            {
                Random random = new Random();
                string[] enemyNames = { "Goblin", "Skeleton", "Orc", "Slime" };
                int index = random.Next(enemyNames.Length);
                string name = enemyNames[index];
                int health = random.Next(30, 61); // Random health between 30 and 60
                int attack = random.Next(10, 21); // Random attack between 10 and 20
                int level = random.Next(1, 6); // Random level between 1 and 5

                return new Enemy(name, health, attack, level);
            }
            else if (lvl == 2) 
            {
                Random random = new Random();
                string[] enemyNames = { "Shrek", "Dark Knight", "Demon", "Dragon" };
                int index = random.Next(enemyNames.Length);
                string name = enemyNames[index];
                int health = random.Next(50, 101); // Random health between 50 and 100
                int attack = random.Next(20, 31); // Random attack between 20 and 30
                int level = random.Next(4, 9); // Random level between 4 and 8

                return new Enemy(name, health, attack, level);

            }
            else if (lvl == 3)
            {
                Random random = new Random();
                string[] enemyNames = { "Orc Warrior", "Shadow Assassin", "Hellhound", "Wyvern" };
                int index = random.Next(enemyNames.Length);
                string name = enemyNames[index];
                int health = random.Next(100, 151); // Random health between 100 and 150
                int attack = random.Next(25, 36); // Random attack between 25 and 35
                int level = random.Next(9, 14); // Random level between 9 and 13

                return new Enemy(name, health, attack, level);
            }
            else if (lvl == 4)
            {
                Random random = new Random();
                string[] enemyNames = { "Troll Berserker", "Spectral Mage", "Demonic Knight", "Fire Drake" };
                int index = random.Next(enemyNames.Length);
                string name = enemyNames[index];
                int health = random.Next(125, 176); // Random health between 125 and 175
                int attack = random.Next(30, 41); // Random attack between 30 and 40
                int level = random.Next(14, 19); // Random level between 14 and 18

                return new Enemy(name, health, attack, level);
            }
            else if (lvl >= 5)
            {
                Random random = new Random();
                string[] enemyNames = { "Giant Warlord", "Shadowblade Assassin", "Infernal Demon", "Ancient Dragon" };
                int index = random.Next(enemyNames.Length);
                string name = enemyNames[index];
                int health = random.Next(150, 201); // Random health between 150 and 200
                int attack = random.Next(35, 46); // Random attack between 35 and 45
                int level = random.Next(19, 24); // Random level between 19 and 23

                return new Enemy(name, health, attack, level);
            }

            return null;
        }
    }
}
