using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueingSystem
{
    class Operator : IAgent
    {
        public State State { get; set; } = State.Free;
        public DateTime WhenItWillBeFree { get; set; }
        public double WorkCoefficient { get; set; }
        private Random Random = new Random(Guid.NewGuid().GetHashCode());
        public Queue Queue { get; set; }
        public Logger Logger { get; set; }

        public void AddClient()
        {
            State = State.Busy;
            WhenItWillBeFree = DateTime.Now + new TimeSpan(0, 0, (int)Math.Ceiling(-Math.Log(Random.NextDouble()) / WorkCoefficient));
            Queue.GetCustomer();
            Logger.AddLog($"{DateTime.Now} operator start serving");
        }

        public void Tick()
        {
            switch (State)
            {
                case State.Free:
                    if (Queue.size > 0)
                    {
                        AddClient();
                    }
                    break;
                case State.Busy:
                    if (DateTime.Now >= WhenItWillBeFree)
                    {
                        Logger.AddLog($"{DateTime.Now} operator finished serving");
                        if (Queue.size > 0)
                        {
                            AddClient();
                        }
                        else
                        {
                            State = State.Free;
                        }
                    }
                    break;
            }
        }
    }

    enum State
    {
        Free,
        Busy
    }
}
