using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoDemo.TestProject.Api.Models;
using UmbracoDemo.TestProject.Api.Models.WidgetResponses;
using UmbracoDemo.TestProject.Models.Widgets;
using UmbracoDemo.TestProject.Services;

namespace UmbracoDemo.TestProject.Api.Controllers
{
    public class WidgetController : UmbracoApiController
    {
        private readonly IWidgetService _widgetService;

        public WidgetController(IWidgetService widgetService)
        {
            _widgetService = widgetService;
        }

        [HttpGet("api/machines/{machineKey}/{tab}")]
        [ProducesResponseType(typeof(IEnumerable<WidgetResponse>), StatusCodes.Status200OK)]
        public IActionResult GetWidgets(Guid machineKey, int tab)
        {

            var result = new List<WidgetResponse>();
            var widgets = _widgetService.GetWidgets(machineKey, tab);

            foreach (var widget in widgets)
            {
                switch (widget)
                {

                    case TextWidget txtWidget:
                        result.Add(new TextWidgetResponse()
                        {
                            Title = txtWidget.Title,
                            IsCollapsible = txtWidget.IsCollapsible,
                            SortOrder = txtWidget.SortOrder,
                            Text = txtWidget.Text,
                            Type = Enum.EWidgetType.Text
                        });
                        break;
                    case TableWidget tblWidget:
                        result.Add(new TableWidgetResponse()
                        {
                            Title = tblWidget.Title,
                            IsCollapsible = tblWidget.IsCollapsible,
                            Machine = new MachineResponse() 
                            { 
                                Customer = tblWidget.Machine?.Customer,
                                DbId = tblWidget.Machine?.DbKey ?? Guid.Empty,
                                Description = tblWidget.Machine?.Description,
                                DisplayName = tblWidget.Machine?.DisplayName,
                                Key = tblWidget.Machine?.Content?.Key ?? Guid.Empty,
                                Lat = tblWidget.Machine?.Lat ?? 0,
                                Long = tblWidget.Machine?.Long ?? 0,
                                SerialNo = tblWidget.Machine?.SerialNo,
                                Metrics = tblWidget.Machine?.Metrics,
                                ImageUrl = tblWidget.Machine?.Image?.Src
                            }

                        });
                        break;
                    case ChartWidget chrtWidget:
                        result.Add(new ChartWidgetResponse()
                        {
                            ChartModel = chrtWidget.ChartModel,
                            IsCollapsible = chrtWidget.IsCollapsible,
                            SortOrder = chrtWidget.SortOrder,
                            Text = chrtWidget.Text,
                            Title = chrtWidget.Title,

                        });
                        break;
                    default:
                        break;


                }
            }


            return Ok(widgets);
        }

    }
}
