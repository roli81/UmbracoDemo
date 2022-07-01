using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoDemo.TestProject.Models.Echarts
{
    public class GoogleMapsMarker
    {
        public string InfoWindowTitle { get; set; }
        public string InfoWindowContent { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public string IconPath { get; set; }

    }
}
