using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;

namespace UmbracoDemo.TestProject.Services
{
    public abstract class BaseService
    {

        protected readonly ILogger Logger;
        protected readonly IUmbracoContextAccessor ContextAccessor;
        protected readonly IUmbracoContext Context;
        protected readonly IPublishedContent CurrentPage;



        protected BaseService(ILogger logger, IUmbracoContextAccessor contextAccessor)
        {
            Logger = logger;
            ContextAccessor = contextAccessor;

            if (ContextAccessor.TryGetUmbracoContext(out Context))
            {
                CurrentPage = Context.PublishedRequest.PublishedContent;
            }
        }

    }
}
