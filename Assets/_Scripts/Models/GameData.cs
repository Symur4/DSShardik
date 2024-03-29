﻿using Assets._Scripts.Buildings;
using Assets.Scripts.Core.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Models
{
    [Serializable]
    internal class GameData
    {
        public GameParams GameParams;
        public List<Tile> Tiles;
        public List<BuildingData> Buildings;
        public List<ResourceData> Resources;
        public List<DroneData> Drones;
    
    }
}
