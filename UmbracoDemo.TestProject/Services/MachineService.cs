using MachineDomainLayer;
using MachineDomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoDemo.TestProject.Models.PageModels;


namespace UmbracoDemo.TestProject.Services
{

    public interface IMachineService
    {
        Models.PageModels.Machine EnrichMachineData(Models.PageModels.Machine machine);
        IEnumerable<Metric> GetMetrics();
        IEnumerable<Metric> GetMetricsForMachine(Guid machineId);
        public IEnumerable<Models.PageModels.Machine> GetMachines();
        public Models.PageModels.Machine GetMachineByKey(Guid key);

    }

    public class MachineService : BaseService, IMachineService
    {
        private readonly MachineDomainContext _domainContext;
        private readonly IImageService _imageService;


        public MachineService(
            MachineDomainContext domainContext,
            ILogger<MachineService> logger,
            IUmbracoContextAccessor contextAccessor, 
            IImageService imageService)
            : base(logger, contextAccessor)
        {
            _domainContext = domainContext;
            _imageService = imageService;
        }


        public IEnumerable<Models.PageModels.Machine> GetMachines()
        {
            if (this.ContextAccessor.TryGetUmbracoContext(out var context))
            {
                var contentType = context.Content.GetContentType("machineOverview");
                var overview = context.Content.GetByContentType(contentType).FirstOrDefault();

                if (overview != null) 
                {

                    var machines = overview
                        .Children
                        .Select(c => EnrichMachineData(new Models.PageModels.Machine(c)));

                    return machines;
                }
                
            }
            return null;
        }

        public Models.PageModels.Machine GetMachineByKey(Guid key)
        {
            if (this.ContextAccessor.TryGetUmbracoContext(out var context)) 
            {
                var page = context.Content.GetById(key);
                
                if (page != null)
                {
                    var machine = new Models.PageModels.Machine(page);
                    machine.Image = machine.Content.HasValue("image") ? _imageService.GetImage(machine.Content.Value<IPublishedContent>("image"), 800, 450) : null;
                    machine = EnrichMachineData(machine);
                    return machine;
                }
            
            }
            
            return null;
        }

        public Models.PageModels.Machine EnrichMachineData(Models.PageModels.Machine machine)
        {
            var dbMachine = _domainContext.Machines
                                .AsNoTracking()
                                .Include(m => m.Metrics)
                                .FirstOrDefault(m => m.SerialNo == machine.SerialNo);
            machine.Metrics = dbMachine.Metrics.Select(m => new Models.Echarts.Metric()
            {
                Id = m.Id,
                MachineId = m.MachineId,
                TimeStamp = m.TimeStamp,
                Type = m.Type,
                Unit = m.Unit,
                Value = m.Value
            }).OrderBy(m => m.TimeStamp);
            machine.DbKey = dbMachine.Id;


            return machine;
        }

        public IEnumerable<Metric> GetMetrics()
        {
            return _domainContext.Metrics;
        }

        public IEnumerable<Metric> GetMetricsForMachine(Guid machineId)
        {
            return _domainContext.Metrics.Where(m => m.MachineId == machineId);
        }
    }
}
