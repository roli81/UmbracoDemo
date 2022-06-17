using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace UmbracoDemo.TestProject.Models.Widgets
{

    public class BaseWidget : PublishedElementModel, IWidget
    {
        public int SortOrder { get; set; }
        public string Title => this.HasValue("title") ? this.Value<string>("title") : string.Empty;
        public string Text => this.HasValue("text") ? this.Value<string>("text") : string.Empty;
        public bool IsCollapsible => this.HasValue("isCollapsible") ? this.Value<bool>("isCollapsible") : false;

        public BaseWidget(
            IPublishedElement content, 
            IPublishedValueFallback publishedValueFallback) 
            : base(content, publishedValueFallback)
        {
        }
    }
}
