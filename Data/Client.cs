using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Client
    {
       public string Name { get; }
       public string Surname { get; }
       public Guid Id { get; }

        public Client(string name, string surname) {
            Name = name;
            Surname = surname;
            Id = Guid.NewGuid();
        }
    }
}
