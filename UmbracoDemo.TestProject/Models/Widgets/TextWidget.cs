using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoDemo.TestProject.Models.Widgets
{
    public class TextWidget : BaseWidget, IWidget
    {
        public TextWidget(
            IPublishedElement content, 
            IPublishedValueFallback publishedValueFallback) 
            : base(content, publishedValueFallback)
        {

        }

        public async Task<IHtmlContent> RenderModule(IHtmlHelper helper)
        {
            return await  helper.PartialAsync("~/Views/Widgets/TextWidget.cshtml", this, helper.ViewData);
        }
    }
}
