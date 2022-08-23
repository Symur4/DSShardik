using Assets._Scripts.TypeConstants;
using Assets.Scripts.Core.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Buildings
{
    [Serializable]
    public class BuildingData
    {
        public BuildingType BuildingType;
        public Hex Position;
    }
}
