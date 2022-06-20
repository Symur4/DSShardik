using Assets._Scripts.Map;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Map;
using Assets.Scripts.TypeConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class MapManager : Singleton<MapManager>
    {
        HexMap _hexMap;
        private float _xOffset = 2;
        private float _yOffset = 1;
        private float _zOffset = 1.73f;
        private int _exploredTileCount = 1;
        List<TileBase> _tileMap = new List<TileBase>();


        public void GenerateMap()
        {
            _hexMap = new HexMap();
            _hexMap.GenerateHexes(20);
            _hexMap.FindTile(0, 0, 0).IsExplored = true;
            _hexMap.AddNoise(100);
            _hexMap.SetTileTypes(new List<BiomLimit> {
                new BiomLimit() { Start=0, End=20, TileType = TileType.Water },
                new BiomLimit() { Start=20, End=40, TileType = TileType.Desert },
                new BiomLimit() { Start=40, End=70, TileType = TileType.Grass },
                new BiomLimit() { Start=70, End=200, TileType = TileType.Mountain },
            });
        }

        public void ShowHexes()
        {
            foreach (var tile in _hexMap.Tiles)
            {
                DrawTile(tile);
            }
        }

        public void ExploreTile(Tile tile)
        {
            if (tile.IsExplored == false)
            {
                var tileResource = ResourceCore.Instance.Tiles.Where(w => w.TileType == tile.TileType).FirstOrDefault();
                var tileBase = _tileMap.Where(w => w.TileData.Hex.q == tile.Hex.q
                                    && w.TileData.Hex.r == tile.Hex.r)
                    .FirstOrDefault();

                tileBase.StartExplore(null, _exploredTileCount++);
            }
        }

        void DrawTile(Tile tile)
        {
            var tileResource = ResourceCore.Instance.Tiles.Where(w => w.TileType == tile.TileType).FirstOrDefault();

            var col = tile.Hex.q + (tile.Hex.r - (tile.Hex.r & 1)) / 2;
            var row = tile.Hex.r;
            var height = 0f;
            int correction = col % 2 == 0 ? 1 : 0;
            var pos = new Vector3((row) * _xOffset - correction,
                            height,
                            col * _zOffset);

            var spawned = Instantiate(tileResource.Prefab, pos, Quaternion.identity, transform);
            var tileBase = spawned.GetComponent<TileBase>();
            tileBase.OriginalMaterial = tileResource.TileMaterail;
            _tileMap.Add(tileBase);
            if (tile.IsExplored)
            {
                tileBase.SetMaterial(tileResource.TileMaterail);
            } else
            {
                tileBase.SetMaterial(tileResource.BlankMaterial);
            }

            tileBase.TileData = tile;
        }
         
    }
}
