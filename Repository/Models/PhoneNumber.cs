﻿using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class PhoneNumber
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public int PersonId { get; set; }

        public virtual Person Person { get; set; } = null!;
    }
}
