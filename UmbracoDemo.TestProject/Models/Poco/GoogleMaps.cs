﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoDemo.TestProject.Models.Poco
{
    public class GoogleMaps
    {
        public IEnumerable<GoogleMapsMarker> Markers { get; set; }
    }
}