using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;

namespace SimpleGame
{
    public class SaveManager
    {
        public static bool SavePlayer(Player player, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(player.Name);
                    writer.WriteLine((int)player.Class);
                    writer.WriteLine(player.Health);
                    writer.WriteLine(player.MaxHealth);
                    writer.WriteLine(player.AttackPower);
                    writer.WriteLine(player.Level);
                    writer.WriteLine(player.Defense);
                    writer.WriteLine(player.pCurrency.Amount);

                    writer.WriteLine(player.Weapons.Count);

                    foreach (Weapon weapon in player.Weapons)
                    {
                        writer.WriteLine(weapon.Name);
                        writer.WriteLine(weapon.AttackPower);
                        writer.WriteLine(weapon.Price);
                        writer.WriteLine(weapon.Description);
                    }

                    writer.WriteLine(player.Items.Count);

                    foreach (Item item in player.Items)
                    {
                        writer.WriteLine(item.Name);
                        writer.WriteLine(item.Effect);
                        writer.WriteLine(item.Price);
                        writer.WriteLine(item.Quantity);
                        writer.WriteLine(item.Description);
                        writer.WriteLine((int)item.Category);
                    }
                }

                Console.WriteLine("Successfully saved the game.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save the game: {ex.Message}");
            }

            return false;
        }
    }
}