using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Utility
{
    //Singleton
    public class Logger
    {
        static private Logger? Instance;
        static private List<string> strings = new List<string>();
        private Logger()
        {
        }

        public static Logger getInstance()
        {
            if (Instance == null)
            {
                Instance = new Logger();
            }
            return Instance;
        }

        public void Log(string message)
        {
            strings.Add(message);
            Console.WriteLine(message);
        }

        public void Record(string message)
        {
            strings.Add(message);
        }

        public void Break()
        {
            strings.Add("----------");
            Console.WriteLine();
        }

        public void Reveal()
        {
            foreach (var items in strings)
            {
                Console.WriteLine(items);
            }
        }
    }
}
