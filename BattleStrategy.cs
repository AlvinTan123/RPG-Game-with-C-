using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGame
{
    public abstract class BattleStrategy
    {
        public abstract void PerformBattle(Player player, Character opponent);
    }
}
