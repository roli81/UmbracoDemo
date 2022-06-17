using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoDemo.TestProject.Models.PageModels;

namespace UmbracoDemo.TestProject.Models.Widgets
{
    public class TableWidget : BaseWidget, IWidget
    {

        public Machine Machine { get; set; }

        public IEnumerable<string> Signals => this.HasValue("signals") ? this.Value<IEnumerable<string>>("signals") : null;


        public TableWidget(IPublishedElement content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }

        public async Task<IHtmlContent> RenderModule(IHtmlHelper helper)
        
        {
            return await helper.PartialAsync("~/Views/Widgets/TableWidget.cshtml", this, helper.ViewData);
        }



        public string GetDataAsJson() { 
            var res = JsonSerializer.Serialize(this.Machine.Metrics.Where(m => this.Signals.Contains(m.Type)).Select(m => new { 
                Type = m.Type,
                TimeStamp = m.TimeStamp,
                Value = m.Value,
                Unit = m.Unit
            
            }).ToArray(), new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            });

            return res;
        }

    }
}
