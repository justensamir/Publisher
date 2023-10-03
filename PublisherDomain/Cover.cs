﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherDomain
{
    public class Cover
    {
        public int Id { get; set; }
        public bool DigitalOnly { get; set; }
        public string DesignIdea { get; set; }
        public List<Artist> Artists { get; set; } = new List<Artist>();
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
