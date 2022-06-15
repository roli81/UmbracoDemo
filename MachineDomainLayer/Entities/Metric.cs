using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineDomainLayer.Entities
{
    public class Metric
    {
        public Guid Id { get; set; }
        public Guid MachineId { get; set; }
        public string Type { get; set; }
        public Machine Machine { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }

    }
}
