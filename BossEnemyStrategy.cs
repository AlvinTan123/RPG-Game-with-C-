using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGame
{
    public class BossEnemyStrategy : BattleStrategy
    {
        int specialAttackCount = 0;
        public override void PerformBattle(Player player, Character opponent)
        {
            // Implement battle logic for bosses
            Console.WriteLine("--- Boss Enemy Battle ---");
            Console.WriteLine("Choose an attack:");
            Console.WriteLine("1. Quick Attack");
            Console.WriteLine("2. Blood Demon Art(-10 healths)");
            Console.WriteLine("3. Jesus Power(Only can use 1 time)");
            Console.WriteLine("4. Ransom Attack(Pay to attack)");
            Console.WriteLine("5. Stun");
            Console.WriteLine("6. Counter Attack (2 times attack, - defense)");
            Console.WriteLine("7. Ultimate Attack (Sacrifice random percent of health to attack)");

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
                    Console.WriteLine($"\n{player.Name} performs a heavy attack!");
                    player.HeavyAttack(opponent);
                    player.Health -= 10; // Subtract 10 from player's health
                    opponent.Attack(player);
                    if (player.Health <= 0)
                    {
                        Console.WriteLine("\n--- Game Over ---");
                        Console.WriteLine("Thank you for playing! Goodbye!");
                        Environment.Exit(0); // Exit the game
                    }
                    break;
                case "3":
                    if (specialAttackCount < 1) // Check if the special attack has been used less than 2 times
                    {
                        Console.WriteLine($"\n{player.Name} performs a Ultimate power!");
                        player.SpecialAttack(opponent);
                        specialAttackCount++;
                        if (player.Health <= 0)
                        {
                            Console.WriteLine("\n--- Game Over ---");
                            Console.WriteLine("Thank you for playing! Goodbye!");
                            Environment.Exit(0); // Exit the game
                        }
                    }
                    {
                        Console.WriteLine("\nYou have already used the special attack 1 time. Choose a different attack.");
                    }
                    break;
                case "4":
                    Console.Write("\nEnter the amount of currency to use for the Ransom attack: ");
                    string ransomAmountInput = Console.ReadLine();
                    if (int.TryParse(ransomAmountInput, out int ransomAmount))
                    {
                        if (player.Currency.Amount >= ransomAmount)
                        {
                            Console.WriteLine($"{player.Name} uses Ransom!");
                            player.RansomAttack(opponent,ransomAmount);
                            player.Currency.SubtractAmount(ransomAmount);
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
                case "5":
                    Console.WriteLine($"\n{player.Name} use SUSSY Stuns!");
                    player.Stun(opponent,player);
                    if (player.Health <= 0)
                    {
                        Console.WriteLine("\n--- Game Over ---");
                        Console.WriteLine("Thank you for playing! Goodbye!");
                        Environment.Exit(0); // Exit the game
                    }
                    break;
                case "6":
                    Console.WriteLine($"\n{player.Name} performs a counter attack!");
                    player.Defense -= player.Defense - player.Defense;
                    player.CounterAttack(opponent);
                    opponent.Attack(player);
                    player.ResetCharacterBuff();
                    if (player.Health <= 0)
                    {
                        Console.WriteLine("\n--- Game Over ---");
                        Console.WriteLine("Thank you for playing! Goodbye!");
                        Environment.Exit(0); // Exit the game
                    }
                    break;
                case "7":
                    Console.WriteLine($"\n{player.Name} unleashes the ultimate attack!");

                    if (player.Health < player.MaxHealth * 0.4)
                    {
                        player.UltimateAttack(opponent);
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
                        Console.WriteLine("\nYou don't have enough health to unleash the ultimate attack!");
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
