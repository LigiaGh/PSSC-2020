using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldInterface
{
    // extinde o interfata marker fata nici o metoda
    // Grain identificabil pe baza unei key integer - ar fi ok ca in practica sa fie acelasi ca la questionId
    public interface IHello:IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting); // primeste un mesaj si returneaza alt mesaj
    }
}
