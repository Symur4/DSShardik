﻿using Assets.Scripts.TypeConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.Map
{
    [Serializable]
    public class Tile
    {
        public Hex Hex;
        public double Noise;
        public TileType TileType;
        public ResourceType ResourceType;
        public bool IsExplored;
        public float ExploreProgress;
        public bool HasEnergy;
        public bool IsBuildable;
    }
}
