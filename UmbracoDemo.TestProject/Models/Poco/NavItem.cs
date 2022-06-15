using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;

namespace UmbracoDemo.TestProject.Models.Poco
{
    public class NavItem
    {
        public string Text { get; set; }
        public Link Link { get; set; }
    }
}
