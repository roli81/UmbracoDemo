using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoDemo.TestProject.Models.Widgets
{
    public class BaseWidget : PublishedElementModel
    {
        public BaseWidget(
            IPublishedElement content, 
            IPublishedValueFallback publishedValueFallback) 
            : base(content, publishedValueFallback)
        {
        }
    }
}
