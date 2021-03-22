using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Models.Client
{
    public class Client
    {
        public int ClientId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }

        public Client() { }
        public Client(int clientId, string firstName,
            string lastName, string phoneNumber, string position)
        {
            this.ClientId = clientId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.Position = position;
        }
    }
}
