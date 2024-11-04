using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SimpleGame;
using System.Collections.Generic;
using static SimpleGame.Logic;
using System.Numerics;

namespace SimpleGame
{
    public class Player : Character
    {
        public CharacterClass Class { get; set; }
        public int MaxHealth { get; set; }
        private int HealPower { get; set; }
        public int AttackPower { get; set; }

        public List<Item> Items { get; set; }
        public List<Weapon> Weapons { get; set; }

        public int Level { get; set; }
        public int Experience { get; set; }
        private int ExperienceToLevelUp { get; set; }

        public Currency pCurrency { get; set; }

        public Player(string name, CharacterClass characterClass)
        {
            Name = name;
            Class = characterClass;
            MaxHealth = 0;
            pCurrency = new Currency(100);
            Items = new List<Item>();
            Weapons = new List<Weapon>();

            Level = 1;
            Experience = 0;
            ExperienceToLevelUp = 100;

            // Set initial health and heal power based on character class
            switch (Class)
            {
                case CharacterClass.Warrior:
                    MaxHealth = 100;
                    HealPower = 20;
                    Defense = 10;
                    AttackPower = 10;
                    break;
                case CharacterClass.Mage:
                    MaxHealth = 80;
                    HealPower = 30;
                    Defense = 8;
                    AttackPower = 15;
                    break;
                case CharacterClass.Archer:
                    MaxHealth = 90;
                    HealPower = 25;
                    Defense = 5;
                    AttackPower = 9;
                    break;
            }

            Health = MaxHealth;
            Attackpower = AttackPower;
        }

        public void ResetCharacterBuff()
        {
            switch (Class)
            {
                case CharacterClass.Warrior:
                    Defense = 10;
                    break;
                case CharacterClass.Mage:
                    Defense = 8; // Reset the defense to the Mage's default value
                    break;
                case CharacterClass.Archer:
                    Defense = 5;
                    break;
            }
        }

        public void ResetCharacterAttackBuff(Weapon weapon)
        {
            switch (Class)
            {
                case CharacterClass.Warrior:
                    AttackPower = 10;
                    break;
                case CharacterClass.Mage:
                    AttackPower = 15; // Reset the defense to the Mage's default value
                    break;
                case CharacterClass.Archer:
                    AttackPower = 9;
                    break;
            }
            if (weapon != null)
            {
                AttackPower += weapon.AttackPower;
                EquippedWeapon = weapon;

            }
        }

        public override void Attack(Character target)
        {
            Console.WriteLine($"{Name} attacks {target.Name}!");
            // Implement attack logic for the enemy character
            // Use a default attack power for the enemy
            target.Health -= AttackPower;
            Console.WriteLine($"{target.Name} takes {AttackPower} damage.");

            if (target.Health <= 0)
            {
                Console.WriteLine($"{target.Name} has been defeated!");
            }
        }

        public void HeavyAttack(Character target)
        {
            Console.WriteLine($"{Name} use Blood Demon Art 'Blood Drop' on {target.Name}!");
            // Implement attack logic for the enemy character
            // Use a default attack power for the enemy
            target.Health -= AttackPower*2;
            Console.WriteLine($"{target.Name} takes {AttackPower*2} damage.");

            if (target.Health <= 0)
            {
                Console.WriteLine($"{target.Name} has been defeated!");
            }
        }

        public void CounterAttack(Character target)
        {
            Console.WriteLine($"\n{Name} drops his armor to increase speed and slashes 2 times!");

            // Implement attack logic for the enemy character
            // Use a default attack power for the enemy
            target.Health -= AttackPower;
            Console.WriteLine($"\n{target.Name} takes {AttackPower} damage.");

            // Generate a random number between 1 and 10
            Random random = new Random();
            int criticalChance = random.Next(1, 11);

            if (criticalChance <= 4) // 40% chance of critical hit
            {
                int criticalDamage = AttackPower * 2; // Double the attack power
                target.Health -= criticalDamage;
                Console.WriteLine($"Critical Hit! {target.Name} takes {criticalDamage} damage.");
            }
            else
            {
                target.Health -= AttackPower;
                Console.WriteLine($"{target.Name} takes {AttackPower} damage.");
            }

            if (target.Health <= 0)
            {
                Console.WriteLine($"\n{target.Name} has been defeated!");
            }
        }

        public void SpecialAttack(Character target)
        {
            Console.WriteLine($"{Name} use Jesus power repel {target.Name}!");
            // Implement attack logic for the enemy character
            // Use a default attack power for the enemy
            target.Health -= AttackPower*3;
            Console.WriteLine($"The Jesus power is too powerful and {target.Name} takes {AttackPower * 3} damage.");

            if (target.Health <= 0)
            {
                Console.WriteLine($"{target.Name} has been defeated!");
            }
        }

        public void RansomAttack(Character target, int ransom)
        {
            // Implement attack logic for the enemy character
            // Use a default attack power for the enemy
            target.Health -= target.Attackpower+ransom;
            Console.WriteLine($"{target.Name} takes the money and attack itself! Minus: {AttackPower +ransom} damage.");

            if (target.Health <= 0)
            {
                Console.WriteLine($"{target.Name} has been defeated!");
            }
        }

        public void UltimateAttack(Character target)
        {
            // Define the minimum and maximum percentage of health sacrifice
            int minPercentage = 10; // 10% minimum sacrifice
            int maxPercentage = 99; // 99% maximum sacrifice

            // Generate a random percentage within the defined range
            int sacrificePercentage = new Random().Next(minPercentage, maxPercentage + 1);

            // Calculate the damage by multiplying the attack power with the sacrifice percentage
            int damage = AttackPower * sacrificePercentage / 100;

            // Subtract the calculated damage from the target's health
            target.Health -= damage;

            Console.WriteLine($"\n{Name} use Dying Light of JAPANESE GOATTO!!!!");

            // Display the attack details
            Console.WriteLine($"\nGOATTO! I SACRIFICE MY LIFE FOR THIS POWER!!!");
            Console.WriteLine($"The attack power is multiplied by {sacrificePercentage}%");
            Console.WriteLine($"{target.Name} takes {damage} damage.");

            if (target.Health <= 0)
            {
                Console.WriteLine($"{target.Name} has been defeated!");
            }
        }

        public void Stun(Character target, Player player)
        {
            Console.WriteLine($"{Name} use flash to stun {target.Name}!");

            Random random = new Random();
            int chance = random.Next(0, 2); // Generate a random number (0 or 1)

            if (chance == 0)
            {

                // Optional: Reduce the boss's attack power temporarily
               int TemporaryAttackPower = target.Attackpower / 2; // Set the TemporaryAttackPower to half of the original AttackPower

                Console.WriteLine($"Stun on {target.Name} is successful!");
                
            }
            else
            {
                Console.WriteLine($"Stun on {target.Name} has failed!");
                target.Attack(player);
            }

        }

        public override void Defend()
        {
            Console.WriteLine($"{Name} defends!");
            // Implement defend logic based on character class
            // For now, let's assume defense increases by 10 for each class
            Defense += 2;
        }

        public void EquipWeapon(Weapon weapon)
        {

            if (weapon != null)
            {
                AttackPower +=weapon.AttackPower;
                EquippedWeapon = weapon;
                Console.WriteLine($"{Name} has equipped {weapon.Name} (Attack Power: {weapon.AttackPower})");
            }
            else
            {
                AttackPower = 0;
                EquippedWeapon = null;
                Console.WriteLine($"{Name} has unequipped the weapon.");
            }
        }

        public void passiveHeal()
        {
            switch (Class)
            {
                case CharacterClass.Warrior:
                    HealPower = 20;
                    break;
                case CharacterClass.Mage:
                    HealPower = 30;
                    break;
                case CharacterClass.Archer:
                    HealPower = 25;
                    break;
            }

            if (Health < MaxHealth)
            {
                int newHealth = Health + HealPower;
                if (newHealth > MaxHealth)
                {
                    Health = MaxHealth;
                    Console.WriteLine("Character's health has reached the maximum.");
                }
                else
                {
                    Health = newHealth;
                    Console.WriteLine($"Character has been healed by {HealPower} health.");
                }
            }
            else
            {
                Console.WriteLine("Character's health is already at maximum.");
            }
        }

        public void Heal(int itemIndex)
        {
            if (itemIndex >= 0 && itemIndex < Items.Count)
            {
                Item selectedHealingItem = Items[itemIndex];
                if (selectedHealingItem is IHealable healableItem)
                {
                    Console.WriteLine($"{Name} uses {selectedHealingItem.Name} to heal!");

                    int healAmount = healableItem.HealAmount;

                    // Calculate the new health after healing
                    int newHealth = Health + healAmount;

                    // Check if the healed health exceeds the maximum health
                    if (newHealth > MaxHealth)
                    {
                        newHealth = MaxHealth;
                    }

                    // Calculate the actual heal amount (in case the maximum health was reached)
                    int actualHealAmount = newHealth - Health;

                    // Update the player's health
                    Health = newHealth;

                    Console.WriteLine($"{Name} has been healed by {actualHealAmount} health.");

                    // Remove the used healing item from the inventory
                    Items.RemoveAt(itemIndex);
                }
            }
            else
            {
                Console.WriteLine("Invalid item index.");
            }
        }

        public void AttackBoost(int itemIndex)
        {
            if (itemIndex >= 0 && itemIndex < Items.Count)
            {
                Item selectedattackboostItem = Items[itemIndex];
                if (selectedattackboostItem is IAttackBoost attackboostItem)
                {
                    Console.WriteLine($"{Name} uses {selectedattackboostItem.Name} to boost the attack!");

                    int boostAmount = attackboostItem.AttackBoostAmount;

                    // Calculate the new health after healing
                    int newAttackPower = AttackPower + boostAmount;

                    // Update the player's newAttackPower
                    AttackPower = newAttackPower;

                    Console.WriteLine($"{Name}'s attack power has been boosted by {boostAmount} attack power.");

                    // Remove the used healing item from the inventory
                    Items.RemoveAt(itemIndex);
                }
            }
            else
            {
                Console.WriteLine("Invalid item index.");
            }
        }


        public Currency Currency { get; }
        public override void DisplayCharacter()
        {
            Console.WriteLine("Player Information:");
            Console.WriteLine("--------------------");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Class: {Class}");
            Console.WriteLine($"Health: {Health}/{MaxHealth}");
            Console.WriteLine($"Heal Power: {HealPower}");
            Console.WriteLine($"Attack Power: {AttackPower}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Defense: {Defense}");
            Console.WriteLine($"Equipped Weapon: {EquippedWeapon?.Name ?? "None"}");

            Console.WriteLine("\nItems in Inventory:");

            // Separate lists for different categories
            List<Item> healableItems = new List<Item>();
            List<Item> attackBoostItems = new List<Item>();

            // Categorize the items
            foreach (Item item in Items)
            {
                if (item.IsHealable(item))
                {
                    healableItems.Add(item);
                }
                else if (item.IsAttackBoost(item))
                {
                    attackBoostItems.Add(item);
                }
            }

            // Display the categorized items
            if (healableItems.Count == 0 && attackBoostItems.Count == 0)
            {
                Console.WriteLine("No items.");
            }
            else
            {
                if (healableItems.Count > 0)
                {
                    Console.WriteLine("Healable Items:");
                    DisplayItems(healableItems);
                }

                if (attackBoostItems.Count > 0)
                {
                    Console.WriteLine("\nAttack Boost Items:");
                    DisplayItems(attackBoostItems);
                }
            }

 

            Console.WriteLine("\nWeapons in Inventory:");
            if (Weapons.Count == 0)
            {
                Console.WriteLine("No weapons.");
            }
            else
            {
                for (int i = 0; i < Weapons.Count; i++)
                {
                    Weapon weapon = Weapons[i];
                    Console.WriteLine($"{i + 1}. {weapon.Name}");
                }
            }
        }

        public void DisplayStat()
        {
            Console.WriteLine("Player Information:");
            Console.WriteLine("--------------------");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Class: {Class}");
            Console.WriteLine($"Health: {Health}/{MaxHealth}");
            Console.WriteLine($"Heal Power: {HealPower}");
            Console.WriteLine($"Attack Power: {AttackPower}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Defense: {Defense}");

        }

        public void GainExperience(int experiencePoints,Weapon weapon)
        {
            Experience += experiencePoints;
            Console.WriteLine($"{Name} gained {experiencePoints} experience points!");

            // Check if the player has enough experience to level up
            if (Experience >= ExperienceToLevelUp)
            {
                LevelUp(weapon);
            }
        }

        private void LevelUp(Weapon weapon)
        {
            Level++;
            Console.WriteLine($"{Name} leveled up to Level {Level}!");

            // Increase player's stats based on level
            int levelMultiplier = Level - 1; // Start with level 1 multiplier (0 increase for level 1)

            switch (Class)
            {
                case CharacterClass.Warrior:
                    MaxHealth = 100 + levelMultiplier * 40; // Increase MaxHealth by 40 for each level
                    HealPower = 20 + levelMultiplier * 10; // Increase HealPower by 10 for each level
                    Defense = 10 + levelMultiplier * 2; // Increase Defense by 2 for each level
                    AttackPower = 10 + levelMultiplier * 5; // Increase AttackPower by 5 for each level
                    break;
                case CharacterClass.Mage:
                    MaxHealth = 80 + levelMultiplier * 30; // Increase MaxHealth by 30 for each level
                    HealPower = 30 + levelMultiplier * 20; // Increase HealPower by 20 for each level
                    Defense = 8 + levelMultiplier * 2; // Increase Defense by 2 for each level
                    AttackPower = 15 + levelMultiplier * 10; // Increase AttackPower by 10 for each level
                    break;
                case CharacterClass.Archer:
                    MaxHealth = 90 + levelMultiplier * 35; // Increase MaxHealth by 35 for each level
                    HealPower = 25 + levelMultiplier * 2; // Increase HealPower by 15 for each level
                    Defense = 5 + levelMultiplier * 2; // Increase Defense by 2 for each level
                    AttackPower = 9 + levelMultiplier * 6; // Increase AttackPower by 6 for each level
                    break;
            }

            if (weapon != null)
            {
                AttackPower += weapon.AttackPower;
                EquippedWeapon = weapon;
            }

            // Adjust the required experience to level up for the next level
            ExperienceToLevelUp = CalculateExperienceToLevelUp();
            Console.WriteLine($"Experience required to level up: {ExperienceToLevelUp}");
        }

        private int CalculateExperienceToLevelUp()
        {
            // Define your own logic to calculate the required experience to level up for each level
            // You can use a formula or a predefined table based on your game's design

            // For simplicity, let's assume a linear progression where each level requires 100 more experience points
            return 100 + (Level - 1) * 100;
        }

        // Display items in a category
        public void DisplayItems(List<Item> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                Console.WriteLine($"{i + 1}. {item.Name}\n{item.Description}\n");
            }
        }
    }
}
