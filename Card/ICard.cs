using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TCG.Card.Ability;

namespace TCG.Card
{
    //Composite
    [XmlInclude(typeof(LandCard))]
    [XmlInclude(typeof(CreatureCard))]
    [XmlInclude(typeof(SpellCard))]
    [XmlInclude(typeof(IAbility))]
    [Serializable]
    public abstract class ICard
    {
        public string? Name;  //Name of the Card
        public int[]? ManaCost; // Cost to invoke this card
        public string? Type; // Creature/Land/Spell/Enchantment?
        public string? Description; // Description of the card
        public string? Rarity;  //Rarity of the card
        public string? Collection; // The DLC/Collection to which the card belongs to
        public int? CN; // Card Number of the DLC/Collection 
        public ICard? Target; // Other cards on which this card may act on during play phase
        public IAbility? Ability;
        public bool Tapped = false;
        public bool Sick = false;

        public int Attack;
        public int Toughness;


        public abstract void Play();
        public abstract string toString();
        public abstract string Define();
        public void AddTarget(ICard target)
        {
            Target = target;
        }

        public ICard GetTarget()
        {
            return Target;
        }
        public void MakeSick()
        {
            Sick = true;  
        }
    }
}
