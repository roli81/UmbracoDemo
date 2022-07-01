using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoDemo.TestProject.Models.Echarts;
using UmbracoDemo.TestProject.Models.Widgets;

namespace UmbracoDemo.TestProject.Models.PageModels
{

    /// <summary>
    /// all models should be inherted from Umbraco.Cms.Core.Models.ContentModel
    /// </summary>
    public class HelloSolve : ContentModel
    {
        // property to get the typed field values
        public string Title => Content.HasValue("title") ? Content.Value<string>("title") : string.Empty;
        public Image Image { get; set; }
        public IEnumerable<IWidget> WidgetsLeft { get; set; }
        public IEnumerable<IWidget> WidgetsRight { get; set; }

        public HelloSolve(IPublishedContent content) : base(content)
        {
        }
    }
}
