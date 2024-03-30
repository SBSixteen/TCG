using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Board
{
    public class EndTurnState : IBoardState
    {
        public IBoardState? Execute()
        {
            Board.SwapPlayers();

            foreach (var card in Board.GetCurrentPlayer().GetPermanents())
            {
                card.Sick = false;
                card.Tapped = false;
            }

            foreach (var card in Board.GetOpposingPlayer().GetPermanents())
            {
                card.Sick = false;
                card.Tapped = false;
            }

            Board.GetCurrentPlayer().ResetManaPool();
            Board.GetOpposingPlayer().ResetManaPool();

            return new DrawCardState();
        }
    }
}
