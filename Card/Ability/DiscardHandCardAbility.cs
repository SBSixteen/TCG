using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Utility;

namespace TCG.Card.Ability
{
    public class DiscardHandCardAbility : IAbility
    {
        public DiscardHandCardAbility() { }

        public override void Execute()
        {
            Random random = new Random();
            int select = random.Next(Board.Board.GetOpposingPlayer().GetHandLength());
            Board.Board.GetOpposingPlayer().DiscardCard(select);
        }

        public override string ToString()
        {
            return "Discards 1 random card from the opposing player's hand";
        }
    }
}
