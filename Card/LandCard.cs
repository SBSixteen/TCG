using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TCG.Utility;

namespace TCG.Card
{
    public class LandCard : ICard
    {
        private LandCard() { }
        public LandCard(string name, int[] manacost, string desc, string rarity, string collection)
        {

            Name = name;
            ManaCost = manacost;
            Type = "Land";
            Description = desc;
            Rarity = rarity;
            Collection = collection;

        }

        public override void Play()
        {
            Logger Logger = Logger.getInstance();

            Board.Board.GetCurrentPlayer().addMana(ManaCost);
            Logger.Log($"{Board.Board.GetCurrentPlayer().Name} has now been given {StringHelper.ManaCost(ManaCost)}.");
            Tapped = true;
        }

        public override string toString()
        {
            string costs = StringHelper.ManaCost(ManaCost);
            return string.Format("\nName: {0} [{1}]\nMana Provision: {2}\nDescription: {3}\nRarity: {4}\nCollection: {5}", Name, Type, costs, Description, Rarity, Collection);
        }

        public override string Define()
        {
            string costs = StringHelper.ManaCost(ManaCost);
            return string.Format("{0} [{1}] | Gives {2} Mana", Name, Type, costs);
        }

        public void Reset()
        {
            Tapped = false;
        }
    }
}
