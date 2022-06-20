using Assets.Scripts.TypeConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.Map
{
    public class Tile
    {
        public Hex Hex;
        public double Noise;
        public TileType TileType;
        public ResourceType ResourceType;
        public bool IsExplored;
    }
}
