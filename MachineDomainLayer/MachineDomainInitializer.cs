using MachineDomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineDomainLayer
{
    public static class MachineDomainInitializer
    {


        public static void Initialize(MachineDomainContext context)
        {
            context.Database.EnsureCreated();

            var gen = new Random();

            var machines = new List<Machine>()
            {
                new Machine()
                {
                    Id = Guid.NewGuid(),
                    SerialNo = "SLV-DEV0001"
                },
                                new Machine()
                {
                    Id = Guid.NewGuid(),
                    SerialNo = "SLV-DEV0002"
                },
                new Machine()
                {
                    Id = Guid.NewGuid(),
                    SerialNo = "SLV-DEV0003"
                },
                new Machine()
                {
                    Id = Guid.NewGuid(),
                    SerialNo = "SLV-DEV0004"
                },
                new Machine()
                {
                    Id = Guid.NewGuid(),
                    SerialNo = "SLV-DEV0005"
                },
                new Machine()
                {
                    Id = Guid.NewGuid(),
                    SerialNo = "SLV-DEV0006"
                },
                new Machine()
                {
                    Id = Guid.NewGuid(),
                    SerialNo = "SLV-DEV0007"
                },
                new Machine()
                {
                    Id = Guid.NewGuid(),
                    SerialNo = "SLV-DEV0008"
                },
                                                                                                                                                new Machine()
                {
                    Id = Guid.NewGuid(),
                    SerialNo = "SLV-DEV0009"
                },
                new Machine()
                {
                    Id = Guid.NewGuid(),
                    SerialNo = "SLV-DEV0010"
                }

            };

            var metrics = new List<Metric>();
            var protMetrics = new List<Metric>() {

                new Metric()
                {
                    Type = "level",
                    Unit = "%"
                },
                new Metric()
                {
                    Type = "core temperature",
                    Unit = "K",
                },
                new Metric()
                {
                    Type = "power consumption",
                    Unit = "mA"
                },
                new Metric()
                {
                    Type = "production units",
                    Unit = "Pcs"

                }

            };

            foreach (var machine in machines)
            {
                for (var i = 0; i < 250; i++)
                {

                    var protMetric = protMetrics[i % 4];

                    var rndDate = GetRandomDate();
                    switch (protMetric.Type)
                    {
                        case "level":
                            metrics.Add(new Metric()
                            {
                                Id = Guid.NewGuid(),
                                MachineId = machine.Id,
                                TimeStamp = rndDate,
                                Value = gen.NextDouble() * 100,
                                Type = protMetric.Type,
                                Unit = protMetric.Unit
                            });
                            break;
                        case "core temperature":
                            metrics.Add(new Metric()
                            {
                                Id = Guid.NewGuid(),
                                MachineId = machine.Id,
                                TimeStamp = rndDate,
                                Value = gen.NextDouble() * 30,
                                Type = protMetric.Type,
                                Unit = protMetric.Unit
                            });
                            break;
                        case "power consumption":
                            metrics.Add(new Metric()
                            {
                                Id = Guid.NewGuid(),
                                MachineId = machine.Id,
                                TimeStamp = rndDate,
                                Value = gen.NextDouble() * 1000,
                                Type = protMetric.Type,
                                Unit = protMetric.Unit
                            });
                            break;
                        case "production units":
                            metrics.Add(new Metric()
                            {
                                Id = Guid.NewGuid(),
                                MachineId = machine.Id,
                                TimeStamp = rndDate,
                                Value = gen.Next(25457),
                                Type = protMetric.Type,
                                Unit = protMetric.Unit
                            });
                            break;

                    }

                }
            }



            if (context.Machines.Count() < 1 && context.Metrics.Count() < 1)
            {
                foreach (var machine in machines)
                {
                    context.Machines.Add(machine);

                }
                context.SaveChanges();


                foreach (var metric in metrics)
                {
                    context.Metrics.Add(metric);

                }
                context.SaveChanges();

            }

        }


        private static DateTime GetRandomDate()
        {
            var gen = new Random();
            DateTime start = new DateTime(2017, 1, 1);
            int range = (DateTime.Today - start).Days;
            start = start.AddHours(gen.Next(60));
            start = start.AddMinutes(gen.Next(60));
            start = start.AddSeconds(gen.Next(60));
            start = start.AddMilliseconds(gen.Next(1000));
            return start.AddDays(gen.Next(range));

        }
    }
}
