using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingHub
{
    public class Package
    {
        public uint ID { get; set; }
        public string? City { get; set; }
        public string? StateInitials { get; set; }
        public string? Address { get; set; }
        public string? Zip { get; set; }
        public DateTime ArrivalDate { get; set; }

        public bool Equals(Package other) {
            return this.ID == other.ID;
        }
    }
}
