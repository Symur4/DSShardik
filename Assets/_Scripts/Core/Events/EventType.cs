using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Core.Events
{
    public enum EventType
    {
        [EventTypeAttribute(typeof(BuildingArg))]
        BuildingBuilt,
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class EventTypeAttribute : Attribute
    {
        public EventTypeAttribute(Type argType)
        {
            ArgsType = argType;
        }

        public Type ArgsType { get; }
    }
}
