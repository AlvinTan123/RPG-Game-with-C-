
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGame
{
    public class Item : Logic.IHealable, Logic.IAttackBoost
    {
        public string Name { get; private set; }
        public int Effect { get; private set; }
        public double Price { get; private set; }
        public string Description { get; private set; }
        public int Quantity { get; private set; }
        public ItemCategory Category { get; }

        public Item(string name, int effect, double price, int quantity, string description, ItemCategory category)
        {
            Name = name;
            Effect = effect;
            Price = price;
            Quantity = quantity;
            Description = description;
            Category = category;
        }

        public int HealAmount => Effect;
        public void Heal(Character target)
        {
            // Implement the healing logic here
        }

        public bool IsHealable(Item item)
        {
            return item.Category == ItemCategory.Healable;
        }

        public int AttackBoostAmount => Effect;
        public void AttackBoost(Character target)
        {
            // Implement the attack boost logic here
        }

        public bool IsAttackBoost(Item item)
        {
            return item.Category == ItemCategory.AttackBoost;
        }

        public enum ItemCategory
        {
            Healable,
            AttackBoost
        }
    }
}
