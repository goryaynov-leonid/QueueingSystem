using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueingSystem
{
    class Queue : IAgent
    {
        public double customerLambda { get; set; }
        public double min { get; set; }
        public double max { get; set; }
        public DateTime Arrive { get; set; }
        Random Random = new Random(Guid.NewGuid().GetHashCode());
        public Logger Logger { get; set; }
        public int size { get; set; }

        public Queue(double lambda, Logger logger)
        {
            Logger = logger;
            customerLambda = lambda;
            double r1 = -Math.Log(Random.NextDouble()) / lambda;
            double r2 = -Math.Log(Random.NextDouble()) / lambda;
            min = Math.Min(r1, r2);
            max = Math.Max(r1, r2);
            UpdArrive();
        }

        public void Tick()
        {
            if (DateTime.Now >= Arrive)
            {
                size++;
                Logger.AddLog($"{DateTime.Now} customer arrived");
                UpdArrive();
            }
        }

        private void UpdArrive()
        {
            Arrive = DateTime.Now + new TimeSpan(0, 0, (int)Math.Round(Random.NextDouble() * (max - min)));
        }

        public void GetCustomer()
        {
            size--;
        }
    }
}
