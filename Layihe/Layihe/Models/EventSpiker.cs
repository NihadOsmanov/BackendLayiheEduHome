﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.Models
{
    public class EventSpiker
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int SpikerId { get; set; }
        public Spiker Spiker { get; set; }

    }
}
