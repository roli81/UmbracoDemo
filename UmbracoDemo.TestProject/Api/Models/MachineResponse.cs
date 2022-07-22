using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoDemo.TestProject.Models.Echarts;

namespace UmbracoDemo.TestProject.Api.Models;

public class MachineResponse
{
    public Guid Key { get; set; }
    public Guid DbId { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public decimal Lat { get; set; }
    public decimal Long { get; set; }
    public string Customer { get; set; }
    public string SerialNo { get; set; }
    public string ImageUrl { get; set; }
    public IEnumerable<Metric> Metrics { get; set; }
}

