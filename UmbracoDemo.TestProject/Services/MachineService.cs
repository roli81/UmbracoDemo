using MachineDomainLayer;
using MachineDomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoDemo.TestProject.Models.PageModels;


namespace UmbracoDemo.TestProject.Services
{

    public interface IMachineService
    {
        Models.PageModels.Machine EnrichMachineData(Models.PageModels.Machine machine);
        IEnumerable<Metric> GetMetrics();
        IEnumerable<Metric> GetMetricsForMachine(Guid machineId);
        
    }

    public class MachineService : IMachineService
    {
        private readonly MachineDomainContext _domainContext;

        public MachineService(MachineDomainContext domainContext)
        {
            _domainContext = domainContext;
           
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
            machine.DbId = dbMachine.Id;


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
