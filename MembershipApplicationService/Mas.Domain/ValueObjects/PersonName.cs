﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas.Domain.ValueObjects
{
    public class PersonName
    {
        public PersonName() {}
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public PersonName(string first, string last)
        {
            FirstName = first;
            LastName = last;
        }
    }
}
