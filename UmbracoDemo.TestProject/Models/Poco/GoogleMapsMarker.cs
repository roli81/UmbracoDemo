using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoDemo.TestProject.Models.Poco
{
    public class GoogleMapsMarker
    {
        public string InfoWindowTitle { get; set; }
        public string InfoWindowContent { get; set; }
        public float Lat { get; set; }
        public float Long { get; set; }
        public string IconPath { get; set; }

    }
}
