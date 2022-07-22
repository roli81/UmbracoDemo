using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoDemo.TestProject.Api.Models;
using UmbracoDemo.TestProject.Models.PageModels;
using UmbracoDemo.TestProject.Services;

namespace UmbracoDemo.TestProject.Api.Controllers
{

  
    public class MachineController : UmbracoApiController
    {

        private readonly IMachineService _machineService;



        public MachineController(IMachineService machineService)
        {
            _machineService = machineService;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Policy = Policies.User)]
        [HttpGet("api/machines")]
        [ProducesResponseType(typeof(IEnumerable<MachineResponse>), StatusCodes.Status200OK)]
        public IActionResult GetAll() 
        {
            var machinePages = _machineService.GetMachines().ToList();

            var res = machinePages.Select(mp => new MachineResponse()
            {
                Customer = mp.Customer,
                DbId = mp.DbKey, 
                Description = mp.Description,   
                DisplayName = mp.DisplayName,
                Key = mp.Content.Key,
                Lat = mp.Lat,
                Long = mp.Long,
                Metrics = mp.Metrics,
                SerialNo = mp.SerialNo
            });


            return Ok(res);
        }


        [Authorize(AuthenticationSchemes = "Bearer", Policy = Policies.User)]
        [HttpGet("api/machines/{machineId}")]
        [ProducesResponseType(typeof(MachineResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid machineId)
        { 
            var mp = _machineService.GetMachineByKey(machineId);

            var res = new MachineResponse()
            {
                Customer = mp.Customer,
                DbId = mp.DbKey,
                Description = mp.Description,
                DisplayName = mp.DisplayName,
                Key = mp.Content.Key,
                Lat = mp.Lat,
                Long = mp.Long,
                Metrics = mp.Metrics,
                SerialNo = mp.SerialNo,
                ImageUrl = mp.Image != null ? mp.Image.Src : string.Empty

            };

            return Ok(res);
        }



    }
}
