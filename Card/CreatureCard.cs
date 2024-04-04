using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Card.Ability;
using TCG.Utility;

namespace TCG.Card
{
    public class CreatureCard : ICard
    {
        private CreatureCard() { }

        public CreatureCard(string name, int[] manacost, string desc, string rarity, string type, string collection, int attack, int defense, IAbility ability, bool sick = true)
        {
            Name = name;
            ManaCost = manacost;
            Description = desc;
            Rarity = rarity;
            Type = "Creature - " + type;
            Collection = collection;
            Attack = attack;
            Toughness = defense;
            Sick = sick;
        }
        public override void Play()
        {
            if (Sick)
            {

            }
            if (Target == null)
            {
                Board.Board.GetCurrentPlayer().GetEnemy().addDamage(Attack);
                Tapped = true;
            }
            else
            {
                Toughness -= Target.Attack;
                Target.Toughness -= Attack;

                if (Target.Toughness < 1) {
                    Board.Board.Logger.Log($"{Target.Name} succumbed to their wounds!");
                }

                if (Toughness < 1)
                {
                    Board.Board.Logger.Log($"{Name} succumbed to their wounds!");
                }

                Tapped = true;
            }
        }

        public override string toString()
        {
            string manacost = StringHelper.ManaCost(ManaCost);
            return string.Format("\nName: {0} [{1}]  +{2}/+{3}\nSummon Cost: {4}\nDescription: {5}\nRarity: {6}\nCollection: {7}", Name, Type, Attack, Toughness, manacost, Description, Rarity, Collection);
        }

        public override string Define()
        {
            string manacost = StringHelper.ManaCost(ManaCost);
            return string.Format("Name: {0} [{1}]  +{2}/+{3} | Summon Cost: {4}", Name, Type, Attack, Toughness, manacost);
        }
    }
}
