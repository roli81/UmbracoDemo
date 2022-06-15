using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoDemo.TestProject.Models.PageModels;
using UmbracoDemo.TestProject.Services;

namespace UmbracoDemo.TestProject.Controllers
{
    public class MachineController : RenderController
    {
        private readonly IMachineService _machineService;


        public MachineController(
            ILogger<RenderController> logger,
            ICompositeViewEngine compositeViewEngine,
            IUmbracoContextAccessor umbracoContextAccessor, IMachineService machineService = null)
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _machineService = machineService;
        }


        

        public override  IActionResult Index()
        {
            var machineModel =  _machineService.EnrichMachineData(new Machine(CurrentPage)) ;
            return CurrentTemplate(machineModel);
        }
    }
}
