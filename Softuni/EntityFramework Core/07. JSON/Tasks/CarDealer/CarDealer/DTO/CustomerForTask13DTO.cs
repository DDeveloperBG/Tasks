using System;
using System.Text.Json.Serialization;

namespace CarDealer.DTO
{
    class CustomerForTask13DTO
    {
        public string Name { get; set; }
      
        public DateTime BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }
    }
}
