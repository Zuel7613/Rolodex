using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Person
    {
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddresses = new HashSet<EmailAddress>();
            PhoneNumbers = new HashSet<PhoneNumber>();
            PhysicalAddresses = new HashSet<PhysicalAddress>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public virtual ICollection<EmailAddress> EmailAddresses { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public virtual ICollection<PhysicalAddress> PhysicalAddresses { get; set; }
    }
}
