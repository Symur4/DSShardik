using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Core.Events
{
    public class BuildingArg : EventArgs
    {
        public BuildingArg()
        {
        }

        public string Id { get; set; }
    }
}
