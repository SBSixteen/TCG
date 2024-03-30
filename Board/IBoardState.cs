using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Board
{
    //State
    public interface IBoardState
    {
        public IBoardState? Execute();
    }
}
