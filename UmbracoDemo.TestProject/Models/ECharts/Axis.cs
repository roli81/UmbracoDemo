using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoDemo.TestProject.Models.ECharts
{
    public class Axis
    {
        public string Type { get; set; }
        public IEnumerable<string> Data { get; set; }
    }
}
