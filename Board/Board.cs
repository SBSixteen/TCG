using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Card;
using TCG.Card.Ability;
using TCG.Utility;

namespace TCG.Board
{
    public static class Board
    {

        static IBoardState BoardState;
        static Player CurrentPlayer;
        static Player OpposingPlayer;

        static Logger Logger = Logger.getInstance();

        static Player P1;
        static Player P2;

        public static int Turn_Number = 0;
        static int max_lands = 1;

        static List<IAbility> ActiveEffects;

        static bool initialized = false;

        public static void Initialize()
        {
            BoardState = new NewBoardState();
            Turn_Number = 0;

            Player.Reset();
            initialized = true;
        }

        public static void Execute()
        {

            if (!initialized)
            {
                Initialize();
            }

            BoardState = BoardState.Execute();
            Console.Clear();
        }
        
        public static void SetCurrentPlayer(Player p)
        {
            CurrentPlayer = p;
        }

        public static Player GetCurrentPlayer()
        {
            return CurrentPlayer;
        }

        public static void SetOpposingPlayer(Player p)
        {
            OpposingPlayer = p;
        }

        public static Player GetOpposingPlayer()
        {
            return OpposingPlayer;
        }

        public static void CreatePlayerOne(string name)
        {
            P1 = new Player(name);  
        }

        public static void CreatePlayerTwo(string name)
        {
            P2 = new Player(name);
        }

        public static void SubtractMaxLands()
        {
            max_lands--;
        }

        public static Player GetPlayerOne()
        {
            return P1;
        }

        public static Player GetPlayerTwo()
        {
            return P2;
        }

        internal static IBoardState GetBoardState()
        {
            return BoardState;
        }

        internal static void ResetMaxLands()
        {
            max_lands = 1;
        }

        internal static int GetMaxLands()
        {
            return max_lands;
        }

        public static bool CheckSummonCost(ICard Card)
        {

            int[] Temp = Board.GetCurrentPlayer().GetManaPool();

            for (int i =0; i<Card.ManaCost.Length; i++)
            {
                if (Temp[i] < Card.ManaCost[i])
                {
                    Logger.Log("You do not have enough mana to summon this creature!");
                    return false;
                }
            }

            Board.GetCurrentPlayer().subtractMana(Card.ManaCost);
            Logger.Log($"You have sufficient Mana to summon ${Card.Name}!");
            Logger.Record("Required Mana has been successfully subtracted");
            return true;
        }

        internal static void IncrementTurn()
        {
            Turn_Number++;
        }

        public static void SwapPlayers()
        {
            SetCurrentPlayer(GetCurrentPlayer().GetEnemy());
            SetOpposingPlayer(GetCurrentPlayer().GetEnemy());

            Logger.Record("Successful player swap");
        }
    }


}
