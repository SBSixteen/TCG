﻿using System;
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
            if (Targets.Count == 0)
            {
                //Board.GetOpposingPlayer().addDamage(Attack);
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
