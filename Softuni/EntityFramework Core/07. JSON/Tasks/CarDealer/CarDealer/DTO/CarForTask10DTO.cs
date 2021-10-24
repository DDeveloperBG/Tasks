using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO
{
    public class CarForTask10DTO
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int TravelledDistance { get; set; }
        public HashSet<int> PartsId { get; set; }
    }
}
