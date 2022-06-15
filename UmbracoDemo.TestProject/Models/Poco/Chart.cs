using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace UmbracoDemo.TestProject.Models.Poco
{
    public class Chart
    {
        public IEnumerable<Serie> Series { get; set; }
        public Axis XAxis { get; set; }
        public Axis YAxis { get; set; }


        public string ToJson() {
            return HttpUtility.HtmlAttributeEncode(JsonSerializer.Serialize(this, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            })) ;
        }


    }
}
