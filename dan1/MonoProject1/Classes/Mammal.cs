using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoProject1.Interfaces;

namespace MonoProject1
{
    abstract class Mammal : IMammal
    {
        private bool _lungsFull = false;
        protected IConsoleWriter _consoleWriter;

        public Mammal(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        }
        public void BreatheIn()
        {
            if (_lungsFull)
            {
                _consoleWriter.WriteLine("Lungs are already filled with air");
            }
            else
            {
                _consoleWriter.WriteLine("Breathe in...");
                _lungsFull = true;
            }

        }

        public void BreatheOut()
        {
            if (_lungsFull)
            {
                _consoleWriter.WriteLine("Breathe out...");
                _lungsFull = false;
            }
            else
            {
                _consoleWriter.WriteLine("Lungs are already empty");

            }

        }
        public virtual void Breathe()
        {
            BreatheIn();
            BreatheOut();
        }

    }
}
