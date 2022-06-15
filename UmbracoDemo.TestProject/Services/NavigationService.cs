using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoDemo.TestProject.Models.Poco;

namespace UmbracoDemo.TestProject.Services
{


    public interface INavigationService
    {
        IEnumerable<NavItem> GetNavigation();
    }
    public class NavigationService : BaseService, INavigationService
    {
        public NavigationService(ILogger<INavigationService> logger, IUmbracoContextAccessor contextAccessor) : base(logger, contextAccessor)
        {
        }

        public IEnumerable<NavItem> GetNavigation()
        {
            var result = new List<NavItem>();
            var homePage = DetermineHomeNode();


            if (homePage.Children != null && homePage.Children.Any())
            {
                foreach (var childNode in homePage.Children)
                {
                    result.Add(MapNode(childNode));
                }
            }

            return result;
        }

        private IPublishedContent DetermineHomeNode()
        {

            IPublishedContent node = Context.PublishedRequest.PublishedContent;

            while (node.Parent != null)
            {
                node = node.Parent;
            }

            return node;
        }

        private NavItem MapNode(IPublishedContent parentNode)
        {
            var result = new NavItem()
            {
                Text = parentNode.Name,
                Link = new Umbraco.Cms.Core.Models.Link { 
                    Name = parentNode.Name,
                    Url = parentNode.Url(),
                }
            };

            return result;
        }
    }
}
