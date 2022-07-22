using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoDemo.TestProject.Models.PageModels;

namespace UmbracoDemo.TestProject.Api.Models.WidgetResponses;

public class TableWidgetResponse : WidgetResponse
{
    public MachineResponse Machine { get; set; }
    public IEnumerable<string> Signals { get; set; }
}

