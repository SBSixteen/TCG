using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Card.Ability;
using TCG.Utility;

namespace TCG.Card
{
    public class SpellCard : ICard
    {

        private SpellCard() { }

        public SpellCard(string name, int[] manacost, string desc, string rarity, string type, string collection, IAbility ability) {
            Name = name;
            ManaCost = manacost;
            Description = desc;
            Rarity = rarity;
            Type = "Instant - " + type;
            Collection = collection;
            Ability = ability;
        }

        public override void Play()
        {
            Ability.Execute();
        }

        public override string toString()
        {
            string manacost = StringHelper.ManaCost(ManaCost);
            return string.Format("\nName: {0} [{1}]\nMana Cost: {2}\nDescription: {3}\nAbility: {4}\nRarity: {5}\nCollection: {6}", Name, Type, manacost, Description, Ability.ToString(), Rarity, Collection);
        }

        public override string Define()
        {
            string manacost = StringHelper.ManaCost(ManaCost);
            return string.Format("{0} [{1}] | Cost: {2} | Ability: {3}", Name, Type, manacost,Ability.ToString());
        }
    }
}
