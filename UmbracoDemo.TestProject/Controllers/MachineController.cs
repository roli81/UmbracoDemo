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
using UmbracoDemo.TestProject.Models.PageModels;
using UmbracoDemo.TestProject.Services;

namespace UmbracoDemo.TestProject.Controllers
{
    public class MachineController : RenderController
    {
        private readonly IMachineService _machineService;
        private readonly IImageService _imageService;
        private readonly IWidgetService _widgetService;


        public MachineController(
            ILogger<RenderController> logger,
            ICompositeViewEngine compositeViewEngine,
            IUmbracoContextAccessor umbracoContextAccessor, IMachineService machineService, IImageService imageService, IWidgetService widgetService)
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _machineService = machineService;
            _imageService = imageService;
            _widgetService = widgetService;
        }




        public override  IActionResult Index()
        {
            var machineModel =  _machineService.EnrichMachineData(new Machine(CurrentPage) {
                Image = CurrentPage.HasValue("image") ?
                _imageService.GetImage(CurrentPage.Value<IPublishedContent>("image"), 800, 600)
                : null,
                WidgetsLeft = CurrentPage.HasValue("widgetsLeft") ?
                _widgetService.GetWidgets(CurrentPage, "widgetsLeft")
                : null,
                WidgetsRight = CurrentPage.HasValue("widgetsRight") ?
                _widgetService.GetWidgets(CurrentPage, "widgetsRight")
                : null
            }) ;
            return CurrentTemplate(machineModel);
        }
    }
}
