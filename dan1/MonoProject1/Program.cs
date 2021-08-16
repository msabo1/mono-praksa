using MonoProject1.Interfaces;
using System;

namespace MonoProject1
{
    class Program
    {
        static void Main(string[] args)
        {
            Human<Dog> mario = new Human<Dog>("Mario", "Sabo");
            mario.BuyPet("Asi");
            mario.Pet.Bark();
            mario.Pet.Breathe();
            mario.Walk();
            mario.Pet.Escape();
            mario.Say("Asiiiiiiiiiiiiii, vracaj se!!!!!");


            Dolphin dolfi = new Dolphin();
            dolfi.Breathe();
            dolfi.Dive();
            dolfi.Surface();
            dolfi.Breathe();
            dolfi.BreatheOut();
        }
    }
}
