using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGame
{
    public interface Logic
    {
        public interface IHealable
        {
            int HealAmount { get; }
            void Heal(Character target);
            bool IsHealable(Item item);
        }

        public interface IAttackBoost
        {
            int AttackBoostAmount { get; }
            void AttackBoost(Character target);
            bool IsAttackBoost(Item item);
        }
    }
}
