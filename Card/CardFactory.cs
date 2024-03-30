using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using TCG.Card.Ability;
using TCG.Utility;

namespace TCG.Card
{
    //Factory
    public class CardFactory
    {
        private Logger Logger = Logger.getInstance(); 

        public LandCard CreateLandCard(string name, int[] manacost, string desc, string rarity, string collection)
        {
            Logger.Record("Card Created");
            return new LandCard(name, manacost, desc, rarity, collection);
        }

        public CreatureCard CreateCreatureCard(string name, int[] manacost, string desc, string rarity, string type, string collection, int attack, int defense, IAbility ability)
        {
            Logger.Record("Card Created");
            return new CreatureCard(name, manacost, desc, rarity, type, collection, attack, defense, ability);
        }

        public void PublishCard(ICard Card)
        {
            if (!Directory.Exists("./Cards"))
            {
                Directory.CreateDirectory("./Cards");
            }
            var serializer = new XmlSerializer(typeof(ICard));
            using (var writer = new FileStream("./Cards/"+ Card.Name + ".card", FileMode.Create))
            {
                serializer.Serialize(writer, Card);
            }
            
            Logger.Record("Card Published");

        }

        public ICard ReadPublishedCard(string filename)
        {
            var serializer = new XmlSerializer(typeof(ICard));
            using (var reader = XmlReader.Create("./Cards/"+filename))
            {
                ICard Card = serializer.Deserialize(reader) as ICard;
                if (Card == null)
                {
                    throw new ArgumentException("Card is corrupt");
                }
                else
                {
                    return Card;
                }
            }
        }

        public ICard ReadPublishedCardWithPath(string filename)
        {
            var serializer = new XmlSerializer(typeof(ICard));
            using (var reader = XmlReader.Create(filename))
            {
                ICard Card = serializer.Deserialize(reader) as ICard;
                if (Card == null)
                {
                    throw new ArgumentException("Card is corrupt");
                }
                else
                {
                    return Card;
                }
            }
        }

        public ICard CreateSpellCard(string name, int[] manacost, string desc, string rarity, string type, string collection, IAbility ability)
        {
            Logger.Record("Card Created");
            return new SpellCard(name, manacost, desc, rarity, type, collection, ability);
        }

        public List<ICard> CreateDeck()
        {
            var paths = Directory.GetFiles("./Cards/");
            List<ICard> Deck = new List<ICard>();   

            foreach (string i in paths)
            {
                Console.WriteLine(i);
                Deck.Add(ReadPublishedCard(i));
            }

            Logger.Record("Deck Created");
            return Deck;
        }

        public List<ICard> GenerateDefaultDeck()
        {
            var paths = Directory.GetFiles("./Cards/");
            List<ICard> Deck = new List<ICard>();

            foreach (string i in paths)
            {
                //Console.WriteLine(i);
                Deck.Add(ReadPublishedCardWithPath(i));
                Deck.Add(ReadPublishedCardWithPath(i));
                Deck.Add(ReadPublishedCardWithPath(i));
            }

            return Deck;
        }
    }
}
