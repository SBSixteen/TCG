using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TCG.Utility;

namespace TCG.Board
{
    public class DrawCardState : IBoardState
    {
        IBoardState? IBoardState.Execute()
        {
            Logger Logger = Logger.getInstance();
            Logger.Record($"Entered Draw Card State for {Board.GetCurrentPlayer().Name}. [Turn {Board.Turn_Number}]");

            Board.GetCurrentPlayer().DrawCard();
            Logger.Break();
            Logger.Log($"{Board.GetCurrentPlayer().Name} drew a card from their deck. Their deck now has {Board.GetCurrentPlayer().GetDeckLength()} cards left");
            Logger.Break();
            Logger.Log("Press Enter to Continue.");
            Console.ReadLine();

            return new ActionState();
        }
    }
}
