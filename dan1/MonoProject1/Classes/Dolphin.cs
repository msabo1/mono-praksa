using MonoProject1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoProject1
{
    class Dolphin : Animal, IDolphin
    {
        public Dolphin()
        {
            _consoleWriter.Prefix = "Dolphin";
        }

        private bool _atSurface;

        public void Surface()
        {
            if (_atSurface)
            {
                _consoleWriter.WriteLine("Already on surface");
            }
            else
            {
                _consoleWriter.WriteLine("Going up...");
                _consoleWriter.WriteLine("Surfaced");
                _atSurface = true;
            }
        }

        public void Dive()
        {
            if (_atSurface)
            {
                _consoleWriter.WriteLine("Dove");
                _atSurface = false;
            }
            else
            {
                _consoleWriter.WriteLine("Already under water");
            }
        }

        public override void Breathe()
        {
            if (_atSurface)
            {
                TakeBreath();
            }
            else
            {
                Surface();
                TakeBreath();
            }
        }

        private void TakeBreath()
        {
            BreatheIn();
            BreatheOut();
        
        }
    }
}
