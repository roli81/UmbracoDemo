using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoDemo.TestProject.Api.Enum;

namespace UmbracoDemo.TestProject.Api.Models.WidgetResponses;

public class WidgetResponse
{
    public EWidgetType Type { get; set; }
    public int SortOrder { get; set; }
    public string Title { get; set; }
    public string Text  { get; set; }
    public bool IsCollapsible { get; set; }
}


