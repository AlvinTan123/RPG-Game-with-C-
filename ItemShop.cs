using System;
using System.Collections.Generic;
using static SimpleGame.Item;
using static SimpleGame.Logic;

namespace SimpleGame
{
    public class ItemShop 
    {
        public List<Item> HealableItems { get; }
        public List<Item> AttackBoostItems { get; }

        public ItemShop()
        {
            HealableItems = new List<Item>
            {
                new Item("Potion", 20, 50, 5, "Heal myself babe! (+20 health)", ItemCategory.Healable),
                new Item("Elixir", 30, 75, 2, "Some healing Thingy (+30 health)", ItemCategory.Healable),
                new Item("Sigma Elixir", 40, 80, 3, "Sigma healing (+40 health)", ItemCategory.Healable),
                new Item("Alpha Elixir", 50, 90, 2, "Alpha healing Thingy (+50 health)", ItemCategory.Healable),
                new Item("Giga Elixir", 70, 100, 2, "GigaChad Healing (+70 health)", ItemCategory.Healable)
            };

            AttackBoostItems = new List<Item>
            {
                new Item("HCG", 15, 30, 3, "Boost 15 Attack for myself! (+15 damages)", ItemCategory.AttackBoost),
                new Item("Insulin", 20, 40, 1, "a “kitchen sink” stack of steroids (+20 damages)", ItemCategory.AttackBoost),
                new Item("Prohormones", 30, 45, 3, "Your body converts it to a steroid in your liver (+30 damages)", ItemCategory.AttackBoost),
                new Item("SARMS", 35, 50, 3, "non-steroidal drugs that grow muscle! (+35 damages)", ItemCategory.AttackBoost),
                new Item("GymBro Motivation", 115, 100, 3, "There is no magic pill (+115 damages)", ItemCategory.AttackBoost)
            };
        }

        public void DisplayItems()
        {
            Console.WriteLine("Healable Items:");
            DisplayItems(HealableItems);

            Console.WriteLine("\nAttack Boost Items:");
            DisplayItems(AttackBoostItems, HealableItems.Count);
        }

        private void DisplayItems(List<Item> items, int startIndex = 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                Console.WriteLine($"{i + startIndex + 1}. {item.Name} (Price: {item.Price} coins)\nStock Left: {item.Quantity}\n{item.Description}\n");
            }
        }

        public int Count()
        {
            return HealableItems.Count + AttackBoostItems.Count;
        }

        public bool IsHealable(Item item)
        {
            return item.Category == ItemCategory.Healable;
        }

        public bool IsAttackBoost(Item item)
        {
            return item.Category == ItemCategory.AttackBoost;
        }
    }
}

