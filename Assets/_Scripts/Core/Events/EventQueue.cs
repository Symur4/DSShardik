using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Core.Events
{
    public class EventQueue<T>  where T: EventArgs
    {
        private readonly Dictionary<EventType, Action<T>> _events;

        public EventQueue()
        {
            _events = new Dictionary<EventType,Action<T>>();
        }

        public void StartListening(EventType e, Action<T> listener)
        {
            if(_events.TryGetValue(e, out var thisEvent))
            {
                thisEvent += listener;
                _events[e] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                _events.Add(e, thisEvent);
            }
        }

        public void StopListening(EventType e, Action<T> listener)
        {
            if (_events.TryGetValue(e, out var thisEvent))
            {
                thisEvent -= listener;
                _events[e] = thisEvent;
            }
        }

        public  void Publish(EventType e, T args)
        {
            if (_events.TryGetValue(e, out var thisEvent))
            {
                thisEvent.Invoke(args);
            }
        }


    }
}
