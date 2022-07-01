using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoDemo.TestProject.Models.PageModels;
using UmbracoDemo.TestProject.Models.Widgets;

namespace UmbracoDemo.TestProject.Services
{
    public interface IWidgetService
    {
        IEnumerable<BaseWidget> GetWidgets(IPublishedContent contentNode, string fieldname, string culture = null);

    }

    public class WidgetService : IWidgetService
    {
        private readonly IMachineService _machineService;


        public WidgetService(IMachineService machineService)
        {
            _machineService = machineService;
      
        }


        public IEnumerable<BaseWidget> GetWidgets(IPublishedContent contentNode, string fieldname, string culture = null)
        {
            var result = new List<BaseWidget>();
            var elements = contentNode.HasValue(fieldname) ? contentNode.Value<IEnumerable<IPublishedElement>>(fieldname).ToList() : null;

            if (elements != null)
            {
                for (var i = 0; i < elements.Count(); i ++)
                {
                    switch (elements[i].ContentType.Alias)
                    {
                        case "textWidget":
                            result.Add(new TextWidget(elements[i], null) { 
                                SortOrder = i
                            });
                            break;

                        case "eChart":
                            var machine = elements[i].HasValue("machine") ? _machineService.EnrichMachineData(new Machine(elements[i].Value<IPublishedContent>("machine"))) : new Machine(contentNode);
                            _machineService.EnrichMachineData(machine);
                            result.Add(new ChartWidget(elements[i], null) 
                            {
                                Machine = machine,
                                SortOrder = i
                            });
                            break;
                        case "tableWidget":
                            machine = elements[i].HasValue("machine") ? _machineService.EnrichMachineData(new Machine(elements[i].Value<IPublishedContent>("machine"))) : new Machine(contentNode);
                            _machineService.EnrichMachineData(machine);
                            result.Add(new TableWidget(elements[i], null)
                            {
                                Machine = machine,
                                SortOrder = i

                            });
                            break;
                    }
                }
            }
            return result;  
        }
    }
}
