using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using UmbracoDemo.TestProject.Models.Widgets;

namespace UmbracoDemo.TestProject.Services
{
    public interface IWidgetService
    {
        IEnumerable<BaseWidget> GetWidgets(IPublishedContent contentNode, string fieldname, string culture = null);
    }

    public class WidgetService : IWidgetService
    {
        public IEnumerable<BaseWidget> GetWidgets(IPublishedContent contentNode, string fieldname, string culture = null)
        {
            var result = new List<BaseWidget>();
            var elements = contentNode.HasValue("widgets") ? contentNode.Value<IEnumerable<IPublishedElement>>("widgets").ToList() : null;

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
                    }
                }
            }
            return result;  
        }
    }
}
