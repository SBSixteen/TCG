using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Card;

namespace TCG.Utility
{
    public static class StringHelper
    {
        public static string[] ManaTypes = { "White", "Blue", "Black", "Red", "Green", "Colorless", };

        public static string ManaCost(int[] manacost)
        {
            if (manacost.Length != 6)
            {
                throw new ArgumentException("Illegal Array of Manacost");
            }

            string manacosts = "";
            int iter = 0;

            foreach (int i in manacost)
            {
                if (i > 0)
                {
                    manacosts += ManaTypes[iter] + ": " + i + ", ";
                }
                iter++;
            }

            return manacosts.Substring(0, manacosts.Length - 2);
        }

        public static string PlayerManaPool(int[] manacost)
        {
            if (manacost.Length != 6)
            {
                throw new ArgumentException("Illegal Array of Manacost");
            }

            string manacosts = "";
            int iter = 0;

            foreach (int i in manacost)
            {
                manacosts += ManaTypes[iter] + ": " + i + ", ";
                iter++;
            }

            return manacosts.Substring(0, manacosts.Length - 2);
        }

        public static string CardListView(List<ICard> Cards)
        {
            string view = "";
            int k = 0;

            if (Cards.Count == 0)
            {
                return "There are no cards in this collection!";
            }

            foreach (var i in Cards)
            {
                k++;
                if (k == 1)
                {
                    view += ($"[{k}] - {i.Define()}");
                }
                else
                {
                    view += ($"\n[{k}] - {i.Define()}");
                }

            }

            return view;
        }

    }
}
