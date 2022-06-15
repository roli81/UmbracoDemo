using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoDemo.TestProject.Models.PageModels;
using UmbracoDemo.TestProject.Models.Poco;
using UmbracoDemo.TestProject.Services;

namespace UmbracoDemo.TestProject.Models.Widgets
{
    public class ChartWidget: BaseWidget, IWidget
    {
      
        public ChartWidget(IPublishedElement content, IPublishedValueFallback publishedValueFallback, Machine machine = null) : base(content, publishedValueFallback)
        {
            this.Machine = machine;
        }


        public Machine Machine { get; set; }
        public string Type => this.HasValue("type") ? this.Value<string>("type") : string.Empty;
        public IEnumerable<string> Signals => this.HasValue("signals") ? this.Value<IEnumerable<string>>("signals") : null;


        public async Task<IHtmlContent> RenderModule(IHtmlHelper helper)
        {

            var series = new List<Serie>();


            foreach (var signal in Signals) 
            {
                if (this.Machine != null)
                {
                    series.Add(new Serie()
                    {
                        Type = this.Type,
                        Data = this.Machine.Metrics.Where(m => signal == m.Type).OrderBy(m => m.TimeStamp).Select(m => m.Value.ToString())
                    });

                }


            }


            var xAxis = new Axis() {
                Type = "category",
                Data = Machine.Metrics.Where(m => Signals.Contains(m.Type)).OrderBy(m => m.TimeStamp).Select(m => m.TimeStamp.ToString()).ToList()
               
            };



            var chartModel = new Chart()
            {
               XAxis = xAxis,
               YAxis = new Axis { 
                    Type="value"
               },
               Series = series
            };

            return await helper.PartialAsync("~/Views/Partials/Chart.cshtml", chartModel, helper.ViewData);
        }

    }
}
