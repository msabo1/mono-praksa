using MonoProject1.Interfaces;
using System;

namespace MonoProject1
{
    class Dog : Animal, IDog, IWalkable
    {
       public void Walk()
        {
            WaveTail();
            Bark();
            _consoleWriter.WriteLine("Moving forward");
        }

        public void WaveTail()
        {
            _consoleWriter.WriteLine("Wave tail");
        }

        public void Escape()
        {
            _consoleWriter.WriteLine("Hmm, although everybody here loves me, and I have unlimited food and water, doors are open, so I should probably escape");
            Walk();
        }

        public void Bark()
        {
            _consoleWriter.WriteLine("Wuf, wuf");
        }
    }
}
