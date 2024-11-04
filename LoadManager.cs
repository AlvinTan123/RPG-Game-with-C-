using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleGame
{
    public class LoadManager
    {
        public static Player LoadPlayer(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                {
                    string playerName = reader.ReadLine();
                    int classChoice = int.Parse(reader.ReadLine());

                    if (Enum.IsDefined(typeof(CharacterClass), classChoice))
                    {
                        CharacterClass characterClass = (CharacterClass)classChoice;

                        int health = int.Parse(reader.ReadLine());
                        int maxhealth = int.Parse(reader.ReadLine());
                        int attackPower = int.Parse(reader.ReadLine());
                        int level = int.Parse(reader.ReadLine());
                        int defense = int.Parse(reader.ReadLine());
                        double currencyAmount = double.Parse(reader.ReadLine());

              


                        int weaponCount = int.Parse(reader.ReadLine());
             
                        List<Weapon> weapons = new List<Weapon>();
                        for (int i = 0; i < weaponCount; i++)
                        {
                            string weaponName = reader.ReadLine();
                            int weaponAttackPower = int.Parse(reader.ReadLine());
                            int weaponPrice = int.Parse(reader.ReadLine());
                            string weaponDescription = reader.ReadLine();

                            Weapon equippedWeapon = new Weapon(weaponName, weaponAttackPower, weaponPrice, weaponDescription);
                            weapons.Add(equippedWeapon);
 
                        }

                        int itemCount = int.Parse(reader.ReadLine());
                        List<Item> items = new List<Item>();
                        for (int i = 0; i < itemCount; i++)
                        {
                            string itemName = reader.ReadLine();
                            int itemEffect = int.Parse(reader.ReadLine());
                            double itemPrice = double.Parse(reader.ReadLine());
                            int itemQuantity = int.Parse(reader.ReadLine());
                            string itemDescription = reader.ReadLine();
                            string itemCategoryStr = reader.ReadLine();

                            if (Enum.TryParse(itemCategoryStr, out Item.ItemCategory itemCategory))
                            {
                                Item item = new Item(itemName, itemEffect, itemPrice, itemQuantity, itemDescription, itemCategory);
                                items.Add(item);
                            }
                            else
                            {
                                Console.WriteLine("Invalid item category in the saved game.");
                                return null;
                            }
                        }

                        Player player = new Player(playerName, characterClass)
                        {
                            Health = health,
                            MaxHealth = maxhealth,
                            AttackPower = attackPower,
                            Level = level,
                            Defense = defense,
                            pCurrency = new Currency(currencyAmount),
                            Weapons = weapons,
                            Items = items
                        };

                        Console.WriteLine("Successfully loaded the saved game.");
                        return player;
                    }
                    else
                    {
                        Console.WriteLine("Invalid character class in the saved game.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load the saved game!");
            }

            return null;
        }
    }
 }

