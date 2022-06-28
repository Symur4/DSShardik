using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core.Map
{
    public class HexMap
    {
        List<Tile> _map = new List<Tile>();
        System.Random random = new System.Random();

        public HexMap()
        {
        }

        public List<Hex> Hexes => _map.Select(s => s.Hex).ToList();
        public List<Tile> Tiles => _map;

        public void SetTiles(List<Tile> tiles)
        {
            _map = tiles;
        }

        public void GenerateHexes(int radius)
        {
            for (int q = -radius; q <= radius; q++)
            {
                int r1 = Math.Max(-radius, -q - radius);
                int r2 = Math.Min(radius, -q + radius);
                for (int r = r1; r <= r2; r++)
                {
                    var t = new Tile()
                    {
                        Hex = new Hex(q, r),
                        IsExplored = false,
                        TileType = TypeConstants.TileType.Grass,
                        ExploreProgress = 0f,
                    };
                    _map.Add(t);
                }
            }
        }

        public void AddNoise(int seed)
        {
            foreach (var tile in _map)
            {
                tile.Noise = NoiseGenerator.GetNoiseAt(tile.Hex.q, tile.Hex.r,
                    0.1f,
                    50f,
                    2,
                    0.9f,
                    0.5f,
                    seed);
            }
            Debug.Log("min:" + _map.Min(m => m.Noise));
            Debug.Log("max:" + _map.Max(m => m.Noise));
        }

        public void SetTileTypes(List<BiomLimit> biomLimits)
        {
            foreach (var tile in _map)
            {
                var tt = biomLimits
                    .Where(w => w.Start <= tile.Noise && w.End >= tile.Noise)
                    .FirstOrDefault();

                if (tt != null)
                {
                    tile.TileType = tt.TileType;
                }
                else
                {
                    tile.TileType = TypeConstants.TileType.Grass;
                }

            }
        }

        public Tile FindTile(int q, int r, int s)
        {
            return _map.Where(w => w.Hex.q == q && w.Hex.r == r).FirstOrDefault();
        }

        public List<Hex> GetNeighbours(Hex center, int startRange, int endRange)
        {
            List<Hex> neighbours = new List<Hex>();

            for (int q = -endRange; q <= endRange; q++)
            {
                int r1 = Math.Max(-endRange, -q - endRange);
                int r2 = Math.Min(endRange, -q + endRange);
                for (int r = r1; r <= r2; r++)
                {
                    var newHex = Add(center, new Hex(q, r));
                    var distance = Distance(center, newHex);
                    if ((distance >= startRange && distance <= endRange)
                        || (distance <= -startRange && distance >= -endRange))
                    {
                        neighbours.Add(newHex);
                    }
                }

            }

            return neighbours;
        }

        Hex Add(Hex hexA, Hex hexB)
        {
            return new Hex(hexA.q + hexB.q, hexA.r + hexB.r);
        }

        public Tile GetRandomHex()
        {
            return _map[random.Next(_map.Count())];
        }

        Hex Subtract(Hex a, Hex b)
        {
            return new Hex(a.q - b.q, a.r - b.r);
        }

        int Distance(Hex a, Hex b)
        {
            var vec = Subtract(a, b);
            return (Math.Abs(vec.q)
                  + Math.Abs(vec.q + vec.r)
                    + Math.Abs(vec.r)) / 2;
        }
    }
}
