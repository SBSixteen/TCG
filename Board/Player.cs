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
            Logger.Record("This player was damaged!");
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
                if (Permanents[i].GetType().ToString() == "TCG.Card.LandCard")
                {
                    Permanents[i].Play();
                }
                else if(Permanents[i].GetType().ToString() == "TCG.Card.CreatureCard")
                {
                    Logger.Log($"{Name} decided to attack with creature ${Permanents[i].Name}");

                    Logger.Break();
                    Logger.Log("Which creature would you like to block with?");

                    List<int> valid = [];
                    int idx = 0;
                    foreach (var card in GetEnemy().GetPermanents())
                    {
                        idx++;
                        if (card.GetType().ToString() == "TCG.Card.CreatureCard" && !card.Tapped)
                        {
                            Logger.Log($"[{idx}] - ${card.Define()}");
                            valid.Add(idx);
                        }
                    }

                    Logger.Log("[0] - Do not block!");
                    Logger.Break();

                    Logger.Log($"{GetEnemy().Name} will now pick a blocking creature from their permanents:" );

                    while (true)
                    {
                        string card_select = Console.ReadLine();
                        int n;
                        if (int.TryParse(card_select, out n))
                        {
                            if (n == 0)
                            {
                                Logger.Break();

                                Logger.Log($"{GetEnemy().Name} chose not to block with any creature!");
                                Logger.Log($"{GetEnemy().Name} was attacked by {Permanents[i].Name} for {Permanents[i].Attack} Damage!");
                                Permanents[i].Play();
                                break;
                            }

                            if (valid.Contains(n))
                            {

                                int mem_tgh_enem = GetEnemy().GetPermanents()[n - 1].Toughness;
                                int mem_tgh_plyr = Permanents[i].Toughness;

                                Permanents[i].AddTarget(GetEnemy().GetPermanents()[n-1]);
                                Permanents[i].Play();

                                if (Permanents[i].Toughness < 1)
                                {
                                    if (Permanents[i].Target?.Toughness < 1)
                                    {
                                        GetEnemy().DiscardPermenantCard(n - 1);
                                    }
                                    DiscardPermenantCard(i);
                                }
                                else
                                {
                                    Permanents[i].Toughness = mem_tgh_plyr;
                                    if (Permanents[i].Target?.Toughness < 1)
                                    {
                                        GetEnemy().DiscardPermenantCard(n - 1);
                                    }
                                    else
                                    {
                                        GetEnemy().GetPermanents()[n - 1].Toughness = mem_tgh_enem;
                                        GetEnemy().GetPermanents()[n - 1].Tapped = true;
                                    }
                                }

                                Logger.Break();
                                break;
                            }
                            else
                            {
                                Logger.Log("Invalid input! You cannot play this card!");
                            }
                        }
                        else
                        {
                            Logger.Log("Invalid input! Enter a number!");
                        }

                    }

                }
                Logger.Log($"{Name} successfully tapped {Permanents[i].Define()}");

            }
        }

        public void RevealPermanents()
        {
            Logger.Log($"[Permenants] - {Name} | {Hand.Count} in Hand, {Permanents.Count} in Play");
            Logger.Log(StringHelper.CardListView(Permanents));
        }

        public void RevealHand()
        {
            Logger.Log($"[Hand] - {Name} | {Hand.Count} in Hand, {Permanents.Count} in Play");
            Logger.Log(StringHelper.CardListView(Hand));

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

        public void DiscardHandCard(int i)
        {

            if (Hand.Count > 1)
            {
                Discard.Add(Hand[i]);
                Hand.RemoveAt(i);
            }
            else
            {
                Logger.Log("Invalid Operation!");
                return;
            }
            
            Logger.Log("Card was successfully discarded!");
        }

        public void DiscardPermenantCard(int i)
        {

            if (Permanents.Count > 1)
            {
                Discard.Add(Permanents[i]);
                Permanents.RemoveAt(i);
            }
            else
            {
                Logger.Log("Invalid Operation!");
                return;
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
