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
using UmbracoDemo.TestProject.Models;
using UmbracoDemo.TestProject.Models.PageModels;

namespace UmbracoDemo.TestProject.Controllers
{
    public class HelloSolveController : RenderController
    {
        public HelloSolveController(
            ILogger<RenderController> logger, 
            ICompositeViewEngine compositeViewEngine, 
            IUmbracoContextAccessor umbracoContextAccessor) : 
            base(logger, compositeViewEngine, umbracoContextAccessor)
        {
        }

        public override IActionResult Index()
        {
            return CurrentTemplate(new HelloSolve(CurrentPage));
        }

    }
}
