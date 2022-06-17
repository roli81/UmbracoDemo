using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoDemo.TestProject.Models.Echarts;
using UmbracoDemo.TestProject.Models.PageModels;
using UmbracoDemo.TestProject.Models.ECharts;

namespace UmbracoDemo.TestProject.Models.Widgets
{
    public class ChartWidget: BaseWidget, IWidget
    {

        public Chart ChartModel { get; set; }




        public ChartWidget(IPublishedElement content, IPublishedValueFallback publishedValueFallback,  Machine machine = null) : base(content, publishedValueFallback)
        {
            this.Machine = machine;
     
        }


        public Machine Machine { get; set; }
        public string Type => this.HasValue("type") ?
            this.Value<string>("type") : 
            string.Empty;
        public IEnumerable<string> Signals => 
            this.HasValue("signals") ? 
            this.Value<IEnumerable<string>>("signals") : null;


        public async Task<IHtmlContent> RenderModule(IHtmlHelper helper)
        {
            var series = new List<Serie>();

            foreach (var signal in Signals) 
            {
                if (this.Machine != null)
                {
                    series.Add(new Serie()
                    {
                        Name = signal,
                        Type = this.Type,
                        Data = this.Machine.Metrics.Where(m => signal == m.Type)
                        .OrderBy(m => m.TimeStamp).Select(m =>  m.Value.ToString())
                    });
                }
            }

            var xAxis = new Axis() {
                Type = "category",
                Data = Machine.Metrics.Where(m => Signals.Contains(m.Type))
                .OrderBy(m => m.TimeStamp).Select(m => m.TimeStamp.ToString()).ToList()               
            };

            this.ChartModel = new Chart()
            {
                XAxis = xAxis,
                YAxis = new Axis {
                    Type = "value"
                },
                Series = series,
                Legend = new Legend() {
                    Data = Signals
                },
                Grid = new Grid() {
                    Bottom = "3%",
                    Right = "4%",
                    Left = "3%",
                    containLabel = true
               }
            };

            return await helper.PartialAsync("~/Views/Widgets/EChart.cshtml", this, helper.ViewData);
        }

    }
}
