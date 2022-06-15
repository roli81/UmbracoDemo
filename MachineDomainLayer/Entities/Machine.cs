using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineDomainLayer.Entities
{
    public class Machine
    {
        public Guid Id { get; set; }
        public string SerialNo { get; set; }
        ICollection<Metric> Metrics { get; set;}

    }
}
