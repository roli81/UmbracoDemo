using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoDemo.TestProject.Models.ECharts;

namespace UmbracoDemo.TestProject.Models.Echarts
{
    public class Serie
    {
        public string Stack { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Data { get; set; }
        public string Type { get; set; }
        
    }
}
