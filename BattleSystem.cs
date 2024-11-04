
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimpleGame.Logic;

namespace SimpleGame
{
    
    public class BattleSystem
    {
        private BattleStrategy battleStrategy;
        ItemShop itemShop = new ItemShop();

        public void SetBattleStrategy(Character opponent)
        {
            if (opponent is Boss)
            {
                battleStrategy = new BossEnemyStrategy();
            }
            else
            {
                battleStrategy = new RegularEnemyStrategy();
            }
        }

        public void Battle(Player player, Character opponent)
        {
            Console.WriteLine("\n--- Battle Begins ---");
            player.DisplayCharacter();
            opponent.DisplayCharacter();

            SetBattleStrategy(opponent);

            while (player.Health > 0 && opponent.Health > 0)
            {
                Console.WriteLine("\nChoose an action:");
                Console.WriteLine("1. Battle Strategy");
                Console.WriteLine("2. Defend");
                Console.WriteLine("3. Heal");
                Console.WriteLine("4. Boost Attack");
                Console.WriteLine("5. Check Stat");

                Console.Write("Enter your choice: ");
                string battleChoice = Console.ReadLine();

                Console.WriteLine();

                if (battleChoice == "1")
                {
                    battleStrategy.PerformBattle(player, opponent);
                    if (player.Health <= 0)
                    {
                        Console.WriteLine("\n--- Game Over ---");
                        Console.WriteLine("Thank you for playing! Goodbye!");
                        Environment.Exit(0); // Exit the game
                    }
                }
                else if (battleChoice == "2")
                {
                    player.Defend();
                    Console.WriteLine();
                    opponent.Attack(player);
                    Console.WriteLine();
                    player.ResetCharacterBuff();
                    if (player.Health <= 0)
                    {
                        Console.WriteLine("\n--- Game Over ---");
                        Console.WriteLine("Thank you for playing! Goodbye!");
                        Environment.Exit(0); // Exit the game
                    }
                }
                else if (battleChoice == "3")
                {
                    Console.WriteLine("Select a heal potion to use:");

                    if (player.Items.Count > 0) // Check if the player has any items
                    {
                        for (int i = 0; i < player.Items.Count; i++)
                        {
                            Item item = player.Items[i];
                            if (itemShop.IsHealable(item))
                            {
                                Console.WriteLine($"{i + 1}. {item.Name}");
                            }
                        }

                        Console.Write("Enter the potion number: ");
                        string potionChoice = Console.ReadLine();

                        if (int.TryParse(potionChoice, out int potionIndex))
                        {
                            if (potionIndex >= 1 && potionIndex <= player.Items.Count)
                            {
                                Item selectedPotion = player.Items[potionIndex - 1];
                                if (itemShop.IsHealable(selectedPotion))
                                {
                                    player.Heal(potionIndex - 1);
                                    Console.WriteLine($"{player.Name} has been healed.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid potion number.");
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("You don't have any potions.");
                    }
                }
                else if (battleChoice == "4")
                {
                    Console.WriteLine("Select an attack boost potion to use:");

                    bool hasAttackBoostPotion = false; // Flag to track if the player has any attack boost potions

                    for (int i = 0; i < player.Items.Count; i++)
                    {
                        Item item = player.Items[i];

                        if (itemShop.IsAttackBoost(item))
                        {
                            Console.WriteLine($"{i + 1}. {item.Name}");
                            hasAttackBoostPotion = true; // Set the flag to true if an attack boost potion is found
                        }
                    }

                    if (hasAttackBoostPotion)
                    {
                        Console.Write("Enter the attack boost potion number: ");
                        string potionChoice = Console.ReadLine();

                        if (int.TryParse(potionChoice, out int potionIndex))
                        {
                            if (potionIndex >= 1 && potionIndex <= player.Items.Count)
                            {
                                Item selectedPotion = player.Items[potionIndex - 1];
                                if (itemShop.IsAttackBoost(selectedPotion))
                                {
                                    player.AttackBoost(potionIndex - 1);
                                    Console.WriteLine($"{player.Name}'s attack power has increased!");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid potion number.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You don't have any attack boost potions.");
                    }
                }
                else if (battleChoice == "5")
                {
                    Console.WriteLine("\n---------Stats-------");
                    player.DisplayStat();
                    Console.WriteLine();
                    opponent.DisplayCharacter();
                }
            }

            Console.WriteLine("\n--- Battle Ends ---");

            player.passiveHeal();
            player.DisplayCharacter();
            opponent.DisplayCharacter();

            if (player.Health <= 0)
            {
                Console.WriteLine("\n--- Game Over ---");
                Console.WriteLine("Thank you for playing! Goodbye!");
                Environment.Exit(0); // Exit the game
            }
            else
            {
                Console.WriteLine("\nCongratulations! You have defeated the enemy.");
                int experienceReward = opponent.Level * 12;
                int currencyReward = opponent.Level * 10; // Currency reward based on opponent level
                player.GainExperience(experienceReward, player.EquippedWeapon);
                player.pCurrency.AddAmount(currencyReward);
                Console.WriteLine($"You have earned {currencyReward} coins.");
            }
        }
    }
}



