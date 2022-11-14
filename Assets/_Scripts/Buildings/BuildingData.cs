using Assets._Scripts.Models;
using Assets._Scripts.TypeConstants;
using Assets.Scripts.Core.Map;
using Assets.Scripts.TypeConstants;
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
        public List<ResourceItem> GenerateResource;
        public List<ResourceData> LocalResources;
        public float BuildLength;
        public float BuildProgress;
        public string Id;

    }
}
