using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Managers
{
    public class TmpEventManager : Singleton<TmpEventManager>
    {
        private Dictionary<string, Action<Dictionary<string, object>>> _eventDictionary = 
            new Dictionary<string, Action<Dictionary<string, object>>>();

        public void StartListening(string eventName, Action<Dictionary<string, object>> listener)
        {
            Action<Dictionary<string, object>> thisEvent;

            if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent += listener;
                Instance._eventDictionary[eventName] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                Instance._eventDictionary.Add(eventName, thisEvent);
            }
        }

        public void StopListening(string eventName, Action<Dictionary<string, object>> listener)
        {            
            Action<Dictionary<string, object>> thisEvent;
            if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent -= listener;
                Instance._eventDictionary[eventName] = thisEvent;
            }
        }

        public void TriggerEvent(string eventName, Dictionary<string, object> message)
        {
            Action<Dictionary<string, object>> thisEvent = null;
            if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(message);
            }
        }
    }
}
