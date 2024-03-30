using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Card.Ability
{
    [Serializable]

    public class DamageAbility : IAbility
    {
        private DamageAbility() { }

        public DamageAbility(int damage)
        {
            Damage = damage;
        }

        public override void Execute()
        {

        }

        public override string ToString()
        {
            return String.Format($"Damages 1 Enemy Player or Creature for {Damage} damage");
        }
    }
}
