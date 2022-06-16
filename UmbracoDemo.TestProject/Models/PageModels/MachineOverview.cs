using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoDemo.TestProject.Models.PageModels
{
    public class MachineOverview : HelloSolve
    {

        public IEnumerable<Machine> Machines { get; set; }


        public MachineOverview(IPublishedContent content) : base(content)
        {
        }
    }
}
