using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueingSystem
{
    class Operator
    {
        public State State { get; set; } = State.Free;
        public DateTime WhenItWillBeFree { get; set; }
        public double WorkCoefficient { get; set; }
        private Random Random = new Random(Guid.NewGuid().GetHashCode());

        public void AddClient()
        {
            State = State.Busy;
            WhenItWillBeFree = DateTime.Now + new TimeSpan(0, 0, (int)Math.Ceiling(-Math.Log(Random.NextDouble()) / WorkCoefficient));
        }

        public void CheckIfIsFree()
        {
            if (State == State.Busy && DateTime.Now >= WhenItWillBeFree)
            {
                State = State.Free;
            }
        }
    }

    enum State
    {
        Free,
        Busy
    }
}
