using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class PhysicalAddress
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public int PersonId { get; set; }

        public virtual Person Person { get; set; } = null!;
    }
}
