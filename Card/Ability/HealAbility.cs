using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Card.Ability
{
    [Serializable]

    public class HealAbility : IAbility
    {
        private HealAbility() { }

        public HealAbility(int heal)
        {
            Heal = heal;
        }

        public override void Execute()
        {

        }

        public override string ToString()
        {
            return String.Format($"Heals 1 Player Creature or themselves for {Heal} health");
        }
    }
}
