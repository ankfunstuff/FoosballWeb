using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ankiro.Framework.Queuing;

namespace EventSourcingTest
{
    public class EventProcessor<T> where T : class
    {
        private readonly Action<T> _action;
        private readonly IBlockingQueue<T> _queue = new BlockingQueue<T>();

        public EventProcessor(Action<T> action)
        {
            _action = action;
            Task.Factory.StartNew(Process);
        }

        public void Add(IEnumerable<T> @events)
        {
            foreach (var enlisted in @events)
                _queue.Enqueue(enlisted);
        }

        public void Add(T @event)
        {
            _queue.Enqueue(@event);
        }

        private void Process()
        {
            foreach (var @event in _queue)
            {
                _action(@event);
            }
        }
    }
}