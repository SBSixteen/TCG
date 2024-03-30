using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Utility;
using TCG.Card;
using System.Reflection;

namespace TCG.Board
{
    public class Player
    {
        Logger Logger = Logger.getInstance();
        private static int playerCounter = 0;
        public string? Name;
        private int Life = 10;
        private int designate = 0;
        private int[] Mana = [0, 0, 0, 0, 0, 0];

        List<ICard> Permanents;
        List<ICard> Deck;
        List<ICard> Hand;
        List<ICard> Discard;

        Player Enemy;

        public Player(string name)
        {
            Name = name;
            playerCounter++;
            designate = playerCounter;
            Deck = new List<ICard>();
            Hand = new List<ICard>();
            Discard = new List<ICard>();
            Permanents = new List<ICard>();
        }

        public void addMana(int[] manapool)
        {
            for (int i = 0; i < manapool.Length; i++)
            {
                Mana[i] += manapool[i];
            }
        }

        public void subtractMana(int[] manapool)
        {
            for (int i = 0; i < manapool.Length; i++)
            {
                Mana[i] -= manapool[i];
            }
        }

        public void addDamage(int attack)
        {
            Life -= attack;
        }

        public string toString()
        {
            string costs = StringHelper.PlayerManaPool(Mana);
            return string.Format("Name: {0} [P{1}]\nLife: {2}\nMana: {3}", Name, designate, Life, costs);
        }

        public static void Reset()
        {
            playerCounter = 0;
        }

        public void SetEnemy(Player E)
        {
            Enemy = E;
        }

        public Player GetEnemy()
        {
            return Enemy;
        }

        public int[] GetManaPool()
        {
            return Mana;
        }

        public void CardToDeck(ICard card)
        {
            Deck.Add(card);
        }

        public void AssignDeck(List<ICard> T)
        {
            Deck = T;
        }

        public void DrawCard()
        {
            Hand.Add(Deck[Deck.Count - 1]);
            Deck.Remove(Deck[Deck.Count-1]);
            Logger.Record("Successfully drew card from deck and moved it to hand");
        }

        public ICard PlayCard(int i)
        {
            i = i - 1;
            Permanents.Add(Hand[i]);
            Hand.RemoveAt(i);
            if(Permanents[Permanents.Count - 1].Sick.GetType().ToString() == "TCG.Card.CreatureCard")
            {
                Permanents[Permanents.Count - 1].Sick = true;
            }
            return Permanents[Permanents.Count-1];
        }

        public ICard GetHandCard(int i)
        {
            i = i - 1;
            return Hand[i];
        }

        public void TapCard(int i)
        {
            if (Permanents[i].Tapped)
            {
                Logger.Log("You cannot tapped a tapped card. You can tap this card in the next turn.");
                return;
            }else if (Permanents[i].Sick) {
                Logger.Log("You cannot tap a card suffering from Summoning Sickness.");
                return;
            }
            else
            {
                Permanents[i].Play();
                Logger.Log($"{Name} tapped {Permanents[i].Define()}");

            }
        }

        public void RevealPermanents()
        {
            Logger.Log(StringHelper.CardListView(Permanents));
        }

        public void RevealHand()
        {
            int k = 0;
            foreach (var i in Hand)
            {
                k++;
                Logger.Log($"[{k}] - {i.Define()}");
            }
        }

        public bool ShuffleDecks()
        {

            Random rng = new Random();
            int size = Deck.Count;

            if (size > 1)
            {
                Logger.Log($"Shuffling {Name}'s deck!");
            }
            else
            {
                Logger.Log("Deck unshufflable due to 0 or 1 cards in the deck!");
                return false;
            }

            while (size > 1)
            {
                size--;
                int k = rng.Next(size + 1);
                ICard tmp = Deck[k];
                Deck[k] = Deck[size];
                Deck[size] = tmp;
            }

            Logger.Break();
            Logger.Log(string.Format("Successfully Shuffled Cards for {0}", this.Name));
            return true;

        }

        public void ConvertManaToColorless(int i)
        {
            if (Mana[i] < 1)
            {
                Logger.Log("You have 0 Mana of this type of mana!");
            }
            else
            {
                Mana[5]++;
                Mana[i]--;
                Logger.Log("Successfully converted to colorless mana");
            }
        }

        public void DiscardCard(int i)
        {

            if (Hand.Count > 1)
            {
                Discard.Add(Hand[i]);
                Hand.RemoveAt(i);
            }
            else
            {
                Discard.Add(Hand[0]);
                Hand.RemoveAt(0);
            }
            
            Logger.Log("Card was successfully discarded!");
        }

        public List<ICard> GetHand()
        {
            return Hand;
        }

        public int GetDeckLength()
        {
            return Deck.Count;
        }

        public int GetHandLength()
        {
            return Hand.Count;
        }

        public int GetPermanentsLength()
        {
            return Permanents.Count;
        }

        internal List<ICard> GetPermanents()
        {
            return Permanents;
        }

        public void ResetManaPool()
        {
            for (int i =0; i< Mana.Length; i++)
            {
                Mana[i] = 0;
            }    
        }
    }

}
