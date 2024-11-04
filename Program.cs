using System;
using System.Collections.Generic;
using System.Numerics;
using SimpleGame;
using static SimpleGame.Logic;

class Program
{
    static void Main()
    {

        Console.WriteLine("Welcome to the Adventure RPG!");

        Console.WriteLine("1. Start a new game");
        Console.WriteLine("2. Load a saved game");

        string gameChoice = "";

        while (gameChoice != "1" && gameChoice != "2")
        {
            Console.Write("Enter your choice: ");
            gameChoice = Console.ReadLine();

            int choiceNumber;
            if (!int.TryParse(gameChoice, out choiceNumber))
            {
                Console.WriteLine("Invalid input. Please enter a valid choice as either '1' or '2'.");
                continue;
            }

            if (choiceNumber != 1 && choiceNumber != 2)
            {
                Console.WriteLine("Invalid choice. Please enter either '1' or '2'.");
            }
            if (gameChoice == "1")
            {
                // Start a new game
                Console.Write("Please enter your character's name: ");
                string playerName = Console.ReadLine();

                Console.WriteLine("Choose a character class:");
                Console.WriteLine("1. Warrior");
                Console.WriteLine("2. Mage");
                Console.WriteLine("3. Archer");

                int classChoice;
                CharacterClass chosenClass;
                while (true)
                {
                    Console.Write("Enter the number corresponding to your class: ");
                    string classInput = Console.ReadLine();

                    if (int.TryParse(classInput, out classChoice))
                    {
                        if (classChoice >= 1 && classChoice <= 3)
                        {
                            chosenClass = (CharacterClass)(classChoice - 1);
                            break;
                        }
                    }

                    Console.WriteLine("\nInvalid choice. Please enter a valid class number.");
                }

                Player player = new Player(playerName, chosenClass);

                Console.WriteLine("\n--- Character Created ---");
                player.DisplayCharacter();

                Console.WriteLine($"\nYou have {player.pCurrency.Amount} coins.");

                WeaponShop weaponShop = new WeaponShop();
                ItemShop itemShop = new ItemShop();
                BattleSystem battleSystem = new BattleSystem();

                // Game loop
                while (true)
                {
                    Console.WriteLine("\nYou find yourself in the town of Rivendell. What would you like to do?");
                    Console.WriteLine("1. Explore the nearby forest");
                    Console.WriteLine("2. Visit the weapon shop");
                    Console.WriteLine("3. Visit the item shop");
                    Console.WriteLine("4. Check your character stats");
                    Console.WriteLine("5. Save and quit the game");

                    Console.Write("\nEnter your choice: ");
                    string choice = Console.ReadLine();

                    Console.WriteLine();

                    if (choice == "1")
                    {
                        // Forest exploration logic
                        Console.WriteLine("You enter the forest and encounter an enemy!");

                        Random random = new Random();
                        int encounterChance = random.Next(1, 11); // Random number between 1 and 10

                        if (encounterChance <= 9)
                        {
                            // Enemy encounter
                            Enemy enemy = Enemy.GenerateRandomEnemy(player.Level);
                            Console.WriteLine("--- Enemy Encountered ---");
                            battleSystem.Battle(player, enemy);
                        }
                        else
                        {
                            // Boss encounter (10% chance)
                            Boss boss = Boss.GenerateRandomBoss(player.Level);
                            Console.WriteLine("--- Boss Encountered ---");
                            battleSystem.Battle(player, boss);
                        }
                    }
                    else if (choice == "2")
                    {
                        Console.WriteLine("Welcome to the Weapon Shop!");
                        weaponShop.DisplayWeapons();

                        Console.Write("Enter the number of the weapon you want to purchase (or 0 to exit): ");
                        int weaponChoice = int.Parse(Console.ReadLine());

                        if (weaponChoice >= 1 && weaponChoice <= weaponShop.Weapons.Count)
                        {
                            Weapon selectedWeapon = weaponShop.Weapons[weaponChoice - 1];

                            if (player.pCurrency.Amount >= selectedWeapon.Price)
                            {
                                player.pCurrency.SubtractAmount(selectedWeapon.Price);
                                player.EquipWeapon(selectedWeapon);
                                player.Weapons.Add(selectedWeapon);
                                Console.WriteLine($"\nYou have purchased the {selectedWeapon.Name}!");

                                Console.WriteLine("\n--- Updated Character Stats ---");
                                player.Attackpower = selectedWeapon.AttackPower;
                                player.DisplayCharacter();
                            }
                            else
                            {
                                Console.WriteLine("Insufficient funds. You still have " + player.pCurrency.Amount + " coins.");
                            }
                        }
                        else if (weaponChoice == 0)
                        {
                            Console.WriteLine("\nYou have exited the weapon shop.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Please try again.");
                        }
                    }
                    else if (choice == "3")
                    {
                        Console.WriteLine("Welcome to the Item Shop!");
                        itemShop.DisplayItems();

                        Console.Write("Enter the number of the item you want to purchase (or 0 to exit): ");
                        int itemChoice = int.Parse(Console.ReadLine());

                        if (itemChoice >= 1 && itemChoice <= itemShop.Count())
                        {
                            Item selectedItem = null;
                            if (itemChoice <= itemShop.HealableItems.Count)
                            {
                                selectedItem = itemShop.HealableItems[itemChoice - 1];
                            }
                            else if (itemChoice <= itemShop.HealableItems.Count + itemShop.AttackBoostItems.Count)
                            {
                                int adjustedIndex = itemChoice - itemShop.HealableItems.Count - 1;
                                selectedItem = itemShop.AttackBoostItems[adjustedIndex];
                            }

                            if (player.pCurrency.Amount >= selectedItem.Price)
                            {
                                player.pCurrency.SubtractAmount(selectedItem.Price);
                                player.Items.Add(selectedItem);
                                Console.WriteLine($"\nYou have purchased the {selectedItem.Name}!");

                                Console.WriteLine("\n--- Updated Character Stats ---");
                                player.DisplayCharacter();
                            }
                            else
                            {
                                Console.WriteLine("Insufficient funds. You still have " + player.pCurrency.Amount + " coins.");
                            }
                        }
                        else if (itemChoice == 0)
                        {
                            Console.WriteLine("\nYou have exited the item shop.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Please try again.");
                        }
                    }
                    else if (choice == "4")
                    {
                        Console.WriteLine("--- Character Stats ---");
                        player.DisplayCharacter();
                        Console.WriteLine($"\nYou have {player.pCurrency.Amount} coins remaining.");
                    }
                    else if (choice == "5")
                    {
                        // Save the game and quit
                        if (SaveManager.SavePlayer(player, "C:\\SUT\\SEM1\\OOP\\save.txt"))
                        {
                            Console.WriteLine("Game saved. Thank you for playing! Goodbye!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Failed to save the game. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please try again.");
                    }
                }
            }
            else if (gameChoice == "2")
            {
                // Load a saved game
                Player player = LoadManager.LoadPlayer("C:\\SUT\\SEM1\\OOP\\save.txt");
                if (player != null)
                {
                    Console.WriteLine("Successfully loaded the game.");

                    // Display loaded player data
                    Console.WriteLine("\n--- Loaded Character Stats ---");
                    player.DisplayCharacter();
                    Console.WriteLine($"\nYou have {player.pCurrency.Amount} coins remaining.");

                    WeaponShop weaponShop = new WeaponShop();
                    ItemShop itemShop = new ItemShop();
                    BattleSystem battleSystem = new BattleSystem();

                    // Game loop
                    while (true)
                    {
                        Console.WriteLine("\nYou find yourself in the town of Rivendell. What would you like to do?");
                        Console.WriteLine("1. Explore the nearby forest");
                        Console.WriteLine("2. Visit the weapon shop");
                        Console.WriteLine("3. Visit the item shop");
                        Console.WriteLine("4. Check your character stats");
                        Console.WriteLine("5. Save and quit the game");

                        Console.Write("\nEnter your choice: ");
                        string choice = Console.ReadLine();

                        Console.WriteLine();

                        if (choice == "1")
                        {
                            // Forest exploration logic
                            Console.WriteLine("You enter the forest and encounter an enemy!");

                            Random random = new Random();
                            int encounterChance = random.Next(1, 11); // Random number between 1 and 10

                            if (encounterChance <= 9)
                            {
                                // Enemy encounter
                                Enemy enemy = Enemy.GenerateRandomEnemy(player.Level);
                                Console.WriteLine("--- Enemy Encountered ---");
                                battleSystem.Battle(player, enemy);
                            }
                            else
                            {
                                // Boss encounter (10% chance)
                                Boss boss = Boss.GenerateRandomBoss(player.Level);
                                Console.WriteLine("--- Boss Encountered ---");
                                battleSystem.Battle(player, boss);
                            }
                        }
                        else if (choice == "2")
                        {
                            // Weapon shop logic
                            Console.WriteLine("Welcome to the Weapon Shop!");
                            weaponShop.DisplayWeapons();

                            Console.Write("Enter the number of the weapon you want to purchase (or 0 to exit): ");
                            int weaponChoice = int.Parse(Console.ReadLine());

                            if (weaponChoice >= 1 && weaponChoice <= weaponShop.Weapons.Count)
                            {
                                Weapon selectedWeapon = weaponShop.Weapons[weaponChoice - 1];

                                if (player.pCurrency.Amount >= selectedWeapon.Price)
                                {
                                    player.pCurrency.SubtractAmount(selectedWeapon.Price);
                                    player.EquipWeapon(selectedWeapon);
                                    player.Weapons.Add(selectedWeapon);
                                    Console.WriteLine($"\nYou have purchased the {selectedWeapon.Name}!");

                                    Console.WriteLine("\n--- Updated Character Stats ---");
                                    player.Attackpower = selectedWeapon.AttackPower;
                                    player.DisplayCharacter();
                                }
                                else
                                {
                                    Console.WriteLine("Insufficient funds. You still have " + player.pCurrency.Amount + " coins.");
                                }
                            }
                            else if (weaponChoice == 0)
                            {
                                Console.WriteLine("\nYou have exited the weapon shop.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice. Please try again.");
                            }
                        }
                        else if (choice == "3")
                        {
                            Console.WriteLine("Welcome to the Item Shop!");
                            itemShop.DisplayItems();

                            Console.Write("Enter the number of the item you want to purchase (or 0 to exit): ");
                            int itemChoice = int.Parse(Console.ReadLine());

                            if (itemChoice >= 1 && itemChoice <= itemShop.Count())
                            {
                                Item selectedItem = null;
                                if (itemChoice <= itemShop.HealableItems.Count)
                                {
                                    selectedItem = itemShop.HealableItems[itemChoice - 1];
                                }
                                else if (itemChoice <= itemShop.HealableItems.Count + itemShop.AttackBoostItems.Count)
                                {
                                    int adjustedIndex = itemChoice - itemShop.HealableItems.Count - 1;
                                    selectedItem = itemShop.AttackBoostItems[adjustedIndex];
                                }

                                if (player.pCurrency.Amount >= selectedItem.Price)
                                {
                                    player.pCurrency.SubtractAmount(selectedItem.Price);
                                    player.Items.Add(selectedItem);
                                    Console.WriteLine($"\nYou have purchased the {selectedItem.Name}!");

                                    Console.WriteLine("\n--- Updated Character Stats ---");
                                    player.DisplayCharacter();
                                }
                                else
                                {
                                    Console.WriteLine("Insufficient funds. You still have " + player.pCurrency.Amount + " coins.");
                                }
                            }
                            else if (itemChoice == 0)
                            {
                                Console.WriteLine("\nYou have exited the item shop.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice. Please try again.");
                            }
                        }
                        else if (choice == "4")
                        {
                            Console.WriteLine("--- Character Stats ---");
                            player.DisplayCharacter();
                            Console.WriteLine($"\nYou have {player.pCurrency.Amount} coins remaining.");
                        }
                        else if (choice == "5")
                        {
                            // Save the game and quit
                            if (SaveManager.SavePlayer(player, "C:\\SUT\\SEM1\\OOP\\save.txt"))
                            {
                                Console.WriteLine("Game saved. Thank you for playing! Goodbye!");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Failed to save the game. Please try again.");
                            }
                        }
                    }
                }

                else
                {
                    Console.WriteLine("File corrupted, relaunch and start new game!");

                }
            }
        }
    }
}
