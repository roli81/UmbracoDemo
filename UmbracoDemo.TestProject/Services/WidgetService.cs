using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
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
            throw new NotImplementedException();
        }
    }
}
