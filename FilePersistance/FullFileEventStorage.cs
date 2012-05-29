using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Ankiro.Framework.Tools.Bus;
using EventSourcingTest;
using EventSourcingTest.Interfaces;

namespace FilePersistance
{
    public class FullFileEventStorage : IEventStorage
    {
        private readonly FileInfo _file;
        private readonly IDictionary<Type, IAggregateRootStorage> _stores = new Dictionary<Type, IAggregateRootStorage>();

        public FullFileEventStorage(Assembly assembly, IBus eventBus)
        {
            _file = new FileInfo(Path.Combine("C:\\temp", "event.store"));
            if (!_file.Exists) return;
            using (var reader = new StreamReader(_file.FullName))
            {
                    while (reader.Peek() >= 0)
                    {
                        var line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line))
                            continue;
                        var @event = JsonHelper.Deserialize<Event>(line);
                        //Type type = assembly.GetType(@event.EventType);
                        //Type rootType = assembly.GetType(@event.AggregateRootType);
                        //dynamic castedEvent = JsonHelper.Deserialize(line, type);

                        //IAggregateRootStorage store;
                        //if (!_stores.TryGetValue(rootType, out store))
                        //{
                        //    store = new InMemoryAggregateRootStorage();
                        //    _stores.Add(rootType, store);
                        //}
                        //store.Append(castedEvent.RootId, castedEvent);
                        //eventBus.Raise(castedEvent);
                }

            }
        }

        public IEnumerable<dynamic> GetEventsForRoot<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot
        {
            var store = GetAggregateRootStore<TAggregateRoot>();
            return store.GetEventsForRoot(id);
        }

        public void Append<TAggregateRoot>(Guid id, IEnumerable<Event> events) where TAggregateRoot : AggregateRoot
        {
            using (var writer = new StreamWriter(_file.FullName, true))
            {
                var store = GetAggregateRootStore<TAggregateRoot>();

                foreach (var @event in events)
                {
                    //@event.AggregateRootType = typeof(TAggregateRoot).FullName;
                    //@event.EventType = @event.GetType().FullName;
                    @event.RootId = id;
                    store.Append(id, @event);
                    writer.WriteLine(JsonHelper.Serialize(@event));
                }
            }
        }

        private IAggregateRootStorage GetAggregateRootStore<TAggregateRoot>() where TAggregateRoot : AggregateRoot
        {
            IAggregateRootStorage store;
            if (!_stores.TryGetValue(typeof(TAggregateRoot), out store))
            {
                store = new InMemoryAggregateRootStorage();
                _stores.Add(typeof(TAggregateRoot), store);
            }
            return store;
        }

        public void Dispose()
        {
        }
    }
}