using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Card;
using TCG.Utility;

namespace TCG.Board
{
    public class ActionState : IBoardState
    {
        public ActionState() { }

        public IBoardState? Execute()
        {
         
            Logger Logger = Logger.getInstance();
            Logger.Record($"Entered Draw Card State for {Board.GetCurrentPlayer().Name}. [Turn {(Board.Turn_Number/2 + 1)}]");
            Board.ResetMaxLands();
            Logger.Record("Board reset Max Lands to 1.");

            while (true)
            {
                Logger.Break();
                Logger.Log($"[Turn {Board.Turn_Number/2 + 1}] - Action Phase [Max Lands [{Board.GetMaxLands()}]]");
                Logger.Log("Decide: "
                    + "\n[1] - Play Card"
                    + "\n[2] - Tap Card"
                    + "\n[3] - Check Permenants"
                    + "\n[4] - Check Hand"
                    + "\n[5] - Check Player Information"
                    + "\n[6] - Convert Mana to Colorless"
                    + "\n[7] - End Turn"
                    + "\n[0] - Clear Screen");

                string input = Console.ReadLine();

                if (input == "1")
                {
                    while (true)
                    {
                        Logger.Break();
                        Logger.Log($"Which card would you like to play? [0 - Cancel]");
                        string card_select = Console.ReadLine();
                        int n;
                        if (int.TryParse(card_select, out n))
                        {
                            if (n==0)
                            {
                                Console.Clear();
                                break;
                            }

                            if (n > Board.GetCurrentPlayer().GetHandLength())
                            {
                                Logger.Log("You don't have this card with you!");
                                Logger.Record($"{Board.GetCurrentPlayer().Name} tried to pick an invalid card from their hand [{n}]");
                                continue;
                            }

                            if (n == 0)
                            {
                                Console.Clear();
                                break;
                            }
                            Logger.Log($"{Board.GetCurrentPlayer().Name} picked {Board.GetCurrentPlayer().GetHand()[n - 1].Define()} card from their hand");

                            string type = Board.GetCurrentPlayer().GetHand()[n-1].GetType().ToString();

                            if (type == ("TCG.Card.LandCard"))
                            {
                                if (Board.GetMaxLands() < 1)
                                {
                                    Console.Clear();
                                    Logger.Log("You can only play a maximum of one lands per turn!");
                                    break;
                                }

                                Board.SubtractMaxLands();
                                ICard PlayCard = Board.GetCurrentPlayer().PlayCard(n);
                            }
                            else {
                                ICard PlayCard = Board.GetCurrentPlayer().GetHandCard(n);
                                if (Board.CheckSummonCost(PlayCard))
                                {
                                    _ = Board.GetCurrentPlayer().PlayCard(n);
                                }
                            }
                            break;
                        }
                        else
                        {
                            Logger.Record($"{Board.GetCurrentPlayer().Name} entered invalid input.");
                            Logger.Break();
                        }
                    }


                }else if (input == "2")
                {
                    Logger.Break();
                    Logger.Record($"{Board.GetCurrentPlayer().Name} is trying to tap a card.");

                    while (true)
                    {
                        Logger.Break();
                        Logger.Log("Which card would you like to tap? [0 - Cancel]");
                        string card_select = Console.ReadLine();
                        if (card_select == "0")
                        {
                            Console.Clear();
                            Logger.Record($"{Board.GetCurrentPlayer().Name} did not tap any card.");
                            break;
                        }
                        int n;
                        if (int.TryParse(card_select, out n))
                        {

                            if (n > Board.GetCurrentPlayer().GetPermanentsLength() || n < 1)
                            {
                                Logger.Log("You don't have this card with you!");
                                Logger.Record($"{Board.GetCurrentPlayer().Name} tried to tap an invalid card from their Permnanent [{n}]");
                                continue;
                            }
                            else
                            {
                                Logger.Log($"{Board.GetCurrentPlayer().Name} tried to tap a card from their Permnanents [C# - {n}]");
                                Board.GetCurrentPlayer().TapCard(n-1);
                                Logger.Break();
                            }
                        }
                        else
                        {
                            Logger.Record($"{Board.GetCurrentPlayer().Name} entered invalid input.");
                            Logger.Break();
                        }
                    }

                }
                else if (input == "3")
                {
                    Console.Clear();
                    Board.GetCurrentPlayer().RevealPermanents();
                    Logger.Break();
                }else if (input == "4")
                {
                    Console.Clear();
                    Board.GetCurrentPlayer().RevealHand();
                    Logger.Break();
                }
                else if (input == "5")
                {
                    Console.Clear();
                    Logger.Log(Board.GetCurrentPlayer().toString());
                    Logger.Break();
                }
                else if (input == "6")
                {
                    Logger.Record($"{Board.GetCurrentPlayer().Name} is trying to convert their color mana to colorless");

                    int[] pool = Board.GetCurrentPlayer().GetManaPool();

                    while (true)
                    {
                        Logger.Break();
                        Board.GetCurrentPlayer().toString();
                        Logger.Break();
                        for (int i = 0; i < pool.Length - 1; i++)
                        {
                            Logger.Log($"[{i + 1}] - {StringHelper.ManaTypes[i]} [{pool[i]}]");
                        }
                        Logger.Log($"[COLORLESS] - {StringHelper.ManaTypes[5]} [{pool[5]}]");
                        Logger.Break();
                        Logger.Log("Which type of mana would you like to convert? [0 - Cancel]");
                        string select = Console.ReadLine();
                        int n;
                        if (int.TryParse(select, out n))
                        {
                            if (n == 0)
                            {
                                Console.Clear();
                                break;
                            }
                            if (n > pool.Length-1)
                            {
                                Logger.Log("This mana type does not exist!");
                            }
                            else
                            {
                                Board.GetCurrentPlayer().ConvertManaToColorless(n-1);
                            }

                        }
                        else
                        {
                            Logger.Record("User entered invalid input.");
                        }
                    }
                }
                else if (input == "7")
                {
                    Logger.Log($"{Board.GetCurrentPlayer().Name} Finished with his turn.");
                    //Board.SetCurrentPlayer(Board.GetCurrentPlayer().GetEnemy());
                    Logger.Break();
                    break;
                }
                else if (input == "0")
                {
                    Logger.Break();
                    Console.Clear();
                }
            }

            Board.IncrementTurn();
            if (Board.Turn_Number%2 == 1)
            {
                Board.SwapPlayers();
                return new DrawCardState();
            }
            return new EndTurnState();
        }
    }
}
