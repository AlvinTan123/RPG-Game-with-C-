using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGame
{
    public class RegularEnemyStrategy : BattleStrategy
    {
        int specialAttackCount = 0;
        public override void PerformBattle(Player player, Character opponent)
        {
            // Implement battle logic for regular enemies
            Console.WriteLine("--- Regular Enemy Battle ---");
            Console.WriteLine("Choose an attack:");
            Console.WriteLine("1. Quick Attack");
            Console.WriteLine("2. Blood Demon Art (-10 healths)");
            Console.WriteLine("3. Jesus Power (Only can use 1 time)");
            Console.WriteLine("4. Ransom Attack (Pay to attack)");

            Console.Write("\nEnter your choice: ");
            string attackChoice = Console.ReadLine();

            switch (attackChoice)
            {
                case "1":
                    Console.WriteLine($"\n{player.Name} performs a quick attack!");
                    player.Attack(opponent);
                    opponent.Attack(player);
                    if (player.Health <= 0)
                    {
                        Console.WriteLine("\n--- Game Over ---");
                        Console.WriteLine("Thank you for playing! Goodbye!");
                        Environment.Exit(0); // Exit the game
                    }
                    break;
                case "2":
                    if (player.Health > 10)
                    {
                        Console.WriteLine($"\n{player.Name} sacrifice 10 health and performs a Demon Art Attck!");
                        player.HeavyAttack(opponent);
                        player.Health -= 10; // Subtract 10 from player's health
                        opponent.Attack(player);
                        if (player.Health <= 0)
                        {
                            Console.WriteLine("\n--- Game Over ---");
                            Console.WriteLine("Thank you for playing! Goodbye!");
                            Environment.Exit(0); // Exit the game
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nCan use this skil, you will DIE!!!");
                    }
                    break;
                case "3":
                    if (specialAttackCount < 1) // Check if the special attack has been used less than 2 times
                    {
                        Console.WriteLine($"\n{player.Name} performs a Ultimate power!");
                        player.SpecialAttack(opponent);
                        specialAttackCount++;
                        opponent.Attack(player);
                        if (player.Health <= 0)
                        {
                            Console.WriteLine("\n--- Game Over ---");
                            Console.WriteLine("Thank you for playing! Goodbye!");
                            Environment.Exit(0); // Exit the game
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nYou have already used the special attack 1 time. Choose a different attack.");
                    }
                    break;
                case "4":
                    Console.Write("\nEnter the amount of currency to use for the Ransom attack (Enemy's attack power + amount): ");
                    string ransomAmountInput = Console.ReadLine();
                    if (int.TryParse(ransomAmountInput, out int ransomAmount))
                    {
                        if (player.pCurrency.Amount >= ransomAmount)
                        {
                            Console.WriteLine($"{player.Name} uses Ransom!");
                            player.RansomAttack(opponent, ransomAmount);
                            player.pCurrency.SubtractAmount(ransomAmount);

                            opponent.Attack(player);
                            if (player.Health <= 0)
                            {
                                Console.WriteLine("\n--- Game Over ---");
                                Console.WriteLine("Thank you for playing! Goodbye!");
                                Environment.Exit(0); // Exit the game
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nInsufficient currency. Choose a different attack.");

                        }
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid input. Choose a different attack.");

                    }
                    break;
                default:
                    Console.WriteLine("\nInvalid choice. Perform a default attack.");
                    break;
            }
            if (player.Health <= 0)
            {
                Console.WriteLine("\n--- Game Over ---");
                Console.WriteLine("Thank you for playing! Goodbye!");
                Environment.Exit(0); // Exit the game
            }
            else
            {
                player.ResetCharacterAttackBuff(player.EquippedWeapon);
            }
        }
    }
}
