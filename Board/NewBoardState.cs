using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Card;
using TCG.Utility;

namespace TCG.Board
{
    public class NewBoardState : IBoardState
    {
        public NewBoardState() { }

        CardFactory Factory = new CardFactory();

        IBoardState? IBoardState.Execute()
        {
            Logger Logger = Logger.getInstance();
            Logger.Record("Entered New Board State");

            string input = "";

            Logger.Log("Welcome to HR:TG!");
            Logger.Log("Would you like to start a new game? (y/n)");

            input = Console.ReadLine();
            input = input.ToLower();

            if (input == "y" || input == "yes")
            {
                Logger.Break();
                Logger.Log("Enter Player 1 Name: ");

                input = "";

                while (input.Length == 0 || input == null)
                {
                    input = Console.ReadLine();
                }

                Board.CreatePlayerOne(input);

                input = "";

                Logger.Break();
                Logger.Log("Enter Player 2 Name: ");

                while (input.Length == 0)
                {
                    input = Console.ReadLine();
                }

                Board.CreatePlayerTwo(input);

                Logger.Break();
                Logger.Log("Successfully Created 2 players.");
                Logger.Break();
                Logger.Log("Player 1");
                Logger.Log(Board.GetPlayerOne().toString());
                Logger.Break();
                Logger.Log("Player 2");
                Logger.Log(Board.GetPlayerTwo().toString());
                Logger.Break();

                Board.GetPlayerOne().SetEnemy(Board.GetPlayerTwo());
                Board.GetPlayerTwo().SetEnemy(Board.GetPlayerOne());


                bool shuffle = Board.GetPlayerOne().ShuffleDecks();
                if (!shuffle)
                {
                    Logger.Log($"Would you like to generate a default deck for {Board.GetPlayerOne().Name}?");
                    input = Console.ReadLine();

                    if (input == "y" || input == "yes")
                    {
                        Board.GetPlayerOne().AssignDeck(Factory.GenerateDefaultDeck());
                        Board.GetPlayerOne().ShuffleDecks();
                    }
                    else
                    {
                        return new BoardFailureState();
                    }
                }
                    
                shuffle = Board.GetPlayerTwo().ShuffleDecks();
                if (!shuffle)
                {
                    Logger.Log($"Would you like to generate a default deck for {Board.GetPlayerTwo().Name}?");
                    input = Console.ReadLine();

                    if (input == "y" || input == "yes")
                    {
                        Board.GetPlayerTwo().AssignDeck(Factory.GenerateDefaultDeck());
                        Board.GetPlayerTwo().ShuffleDecks();
                    }
                    else
                    {
                        return new BoardFailureState();
                    }
                }

                Logger.Break();
                Logger.Log("Each Player will now draw seven cards");

                for (int i = 0; i < 7; i++)
                {
                    Board.GetPlayerOne().DrawCard();
                    Board.GetPlayerTwo().DrawCard();

                }

                Logger.Break();
                Board.GetPlayerOne().RevealHand();
                Logger.Break();
                Board.GetPlayerTwo().RevealHand();
                Logger.Break();

                Random random = new Random();
                if (random.Next(100) > 50)
                {
                    Board.SetCurrentPlayer(Board.GetPlayerOne());
                    Logger.Log($"{Board.GetPlayerOne().Name} will go first.");
                }
                else
                {
                    Board.SetCurrentPlayer(Board.GetPlayerTwo());
                    Logger.Log($"{Board.GetPlayerTwo().Name} will go first.");
                }

                Logger.Break();
                Logger.Log("Press Enter to Continue.");
                Console.ReadLine();
                return new DrawCardState();
            }
            else
            {
                Logger.Break();
                return new BoardFailureState();
            }
        }
    }
}
