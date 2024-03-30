using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TCG.Card.Ability
{
    [XmlInclude(typeof(DamageAbility))]
    [XmlInclude(typeof(HealAbility))]
    [XmlInclude(typeof(DiscardHandCardAbility))]
    [Serializable]
    public abstract class IAbility
    {
        public int Damage = 0;
        public int Heal = 0;
        public abstract void Execute();
        public abstract override string ToString();
    }
}
