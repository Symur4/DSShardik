using Assets.Scripts.TypeConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.Map
{
    public class BiomLimit
    {
        public TileType TileType { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
    }
}
