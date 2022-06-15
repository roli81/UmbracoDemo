using MachineDomainLayer;
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
        Machine EnrichMachineData(Machine machine);
    }

    public class MachineService : IMachineService
    {
        private readonly MachineDomainContext _domainContext;

        public MachineService(MachineDomainContext domainContext)
        {
            _domainContext = domainContext;
        }

        public Machine EnrichMachineData(Machine machine)
        {
            var dbMachine = _domainContext.Machines
                                .AsNoTracking()
                                .Include(m => m.Metrics)
                                .FirstOrDefault(m => m.SerialNo == machine.SerialNo);
            machine.Metrics = dbMachine.Metrics.Select(m => new Models.Poco.Metric()
            {
                Id = m.Id,
                MachineId = m.MachineId,
                TimeStamp = m.TimeStamp,
                Type = m.Type,
                Unit = m.Unit,
                Value = m.Value
            });
            machine.DbId = dbMachine.Id;


            return machine;
        }
    }
}
