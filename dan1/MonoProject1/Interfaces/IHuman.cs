using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoProject1.Interfaces
{
    interface IHuman<PetT>
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        PetT Pet { get; }
        string GetFullName();
        void PrintFullName();
        void BuyPet(string name);
        void Say(string message);



    }
}
