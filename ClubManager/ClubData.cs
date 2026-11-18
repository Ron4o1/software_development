using System;
using System.Collections.Generic;
using System.Text;

namespace ClubManager
{
    internal class ClubData
    {
        public int NextId { get; set; } = 1;
        public List<Club> Clubs { get; set; } = new List<Club>();
    }
}
