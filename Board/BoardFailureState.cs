using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Utility;

namespace TCG.Board
{
    public class BoardFailureState : IBoardState
    {
        public BoardFailureState() { }

        public IBoardState? Execute()
        {
            Logger Logger = Logger.getInstance();
            Logger.Record("Entered Termination State");
            return null;
        }
    }
}
