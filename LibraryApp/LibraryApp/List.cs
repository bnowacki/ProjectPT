using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp
{
    public class Wykaz
    {
        private string firstName;
        private string lastName;
        private string id;

        public Wykaz(string firstName, string lastName, string id)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.id = id;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Id { get => id; set => id = value; }
        public string All { get => id + " " + firstName + " " + lastName; }

        public override bool Equals(object obj)
        {
            Wykaz other = (Wykaz)obj;
            return this.Id == other.Id;
        }
    }
}
