using MonoProject1.Classes;
using MonoProject1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoProject1
{
    class Human<PetT> : Mammal, IHuman<PetT>, IWalkable where PetT : IAnimal, new()
    {
        public string FirstName {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                _consoleWriter.Prefix = value;
            }
        }
        public string LastName { get; set; }

        public PetT Pet { get { return _pet; } }

        private string _firstName;
        private PetT _pet;

        public Human() : base(new ConsoleWriter("John"))
        {
            FirstName = "John";
            LastName = "Doe";
        }

        public Human(string firstName, string lastName) : base(new ConsoleWriter(firstName))
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public void PrintFullName()
        {
            _consoleWriter.WriteLine(GetFullName());
        }

        public void BuyPet(string name)
        {
            _pet = new PetT();
            _pet.Name = name;
        }

        public void Say(string message)
        {
            _consoleWriter.WriteLine(message);
        }

        public void Walk()
        {
            _consoleWriter.WriteLine("Move left leg forward");
            _consoleWriter.WriteLine("Move right leg forward");

        }

    }
}
