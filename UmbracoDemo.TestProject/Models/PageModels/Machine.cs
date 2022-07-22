using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoDemo.TestProject.Models.Echarts;

namespace UmbracoDemo.TestProject.Models.PageModels
{
    public class Machine : HelloSolve
    {

        public string DisplayName => this.Content.HasValue("displayName") ?
        this.Content.Value<string>("displayName") : string.Empty;

        public string Description => this.Content.HasValue("description") ?
         this.Content.Value<string>("description") : string.Empty;

        public decimal Lat => this.Content.HasValue("lat") ?
        this.Content.Value<decimal>("lat") : 0;

        public decimal Long => this.Content.HasValue("long") ?
        this.Content.Value<decimal>("long") : 0;

        public string Customer => this.Content.HasValue("customer") ?
        this.Content.Value<string>("customer") : string.Empty;

        public string SerialNo => this.Content.HasValue("serialNo") ?
        this.Content.Value<string>("serialNo") : string.Empty;


        /// DATA FROM EXTERN DATA SOURCE
        public IEnumerable<Metric> Metrics { get; set; }
        public Guid DbKey { get; set; }

        public Machine(IPublishedContent content) : base(content)
        {
            
        }
    }
}
