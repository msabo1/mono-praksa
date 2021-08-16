using MonoProject1.Interfaces;
using System;

namespace MonoProject1
{
    class Cat : Animal, ICat, IWalkable
    {
        public void Walk()
        {
            ExtendClaws();
            _consoleWriter.WriteLine("Moving forward");
        }

        public void ExtendClaws()
        {
            _consoleWriter.WriteLine("Extending claws...");

        }

    }
}
