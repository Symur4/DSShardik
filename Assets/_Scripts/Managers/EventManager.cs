using Assets._Scripts.Core.Events;
using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Managers
{
    public class EventManager : Singleton<EventManager>
    {
        private Dictionary<Type, object> _queues = new Dictionary<Type, object>();
        
        public void StartListening<T>(EventType e, Action<T> listener) where T : EventArgs
        {
            var queue = GetQueue<T>();
            queue.StartListening(e, listener);
        }

        public void StopListening<T>(EventType e, Action<T> listener) where T : EventArgs
        {
            var queue = GetQueue<T>();
            queue.StopListening(e, listener);
        }

        public void Publish<T>(EventType e, T args) where T: EventArgs
        {
            var queue = GetQueue<T>();
            queue.Publish(e, args);
        }

        private EventQueue<T> GetQueue<T>() where T : EventArgs
        {
            if(_queues.TryGetValue(typeof(T), out var queue))
            {
                return queue as EventQueue<T>; 
            }
            else
            {
                var newQueue = new EventQueue<T>();
                _queues.Add(typeof(T), newQueue);
                return newQueue;
            }
        }
    }
}
