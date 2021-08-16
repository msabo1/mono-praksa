using MonoProject1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoProject1.Classes
{
    class ConsoleWriter : IConsoleWriter 
    {
        public string Prefix { get; set; }

        public ConsoleWriter(string prefix)
        {
            Prefix = prefix;
        }

        public void WriteLine(string line)
        {
            Console.Write($"{Prefix}: {line}\n");
        }
    }
}
