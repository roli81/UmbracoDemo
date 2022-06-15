using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Extensions;
using UmbracoDemo.TestProject.Models;
using UmbracoDemo.TestProject.Models.PageModels;
using UmbracoDemo.TestProject.Services;

namespace UmbracoDemo.TestProject.Controllers
{
    public class HelloSolveController : RenderController
    {
        private readonly IImageService _imageService;
        private readonly IWidgetService _widgetService;



        public HelloSolveController(
            ILogger<RenderController> logger,
            ICompositeViewEngine compositeViewEngine,
            IUmbracoContextAccessor umbracoContextAccessor,
            IImageService imageService, 
            IWidgetService widgetService) :
            base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _imageService = imageService;
            _widgetService = widgetService;
        }

        public override IActionResult Index()
        {
            return CurrentTemplate(new HelloSolve(CurrentPage) { 
                Image = CurrentPage.HasValue("image") ? 
                _imageService.GetImage(CurrentPage.Value<IPublishedContent>("image"), height:500, width: 1980)
                : null,
                Widgets = CurrentPage.HasValue("widgets") ?
                _widgetService.GetWidgets(CurrentPage, "widgets") 
                : null
            });
        }

    }
}
