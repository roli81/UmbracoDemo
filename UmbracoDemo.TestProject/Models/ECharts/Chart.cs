using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using UmbracoDemo.TestProject.Models.Echarts;

namespace UmbracoDemo.TestProject.Models.ECharts
{
    public class Chart
    {
        public IEnumerable<Serie> Series { get; set; }

        public Axis XAxis { get; set; }
        public Axis YAxis { get; set; }
        public Legend Legend { get; set; }
        public Grid Grid { get; set; }



        public string ToJson()
        {
            var res = HttpUtility.HtmlAttributeEncode(JsonSerializer.Serialize(this, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            }));

            return res;
        }


    }
}
