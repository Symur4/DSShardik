using Assets._Scripts.Managers;
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
        public List<Tile> Tiles => _tileMap.Select(s => s.TileData).ToList();

        public MapTile SelectedTile => _selectedTile;

        private HexMap _hexMap = new HexMap();
        private Transform _tileMapContainer;
        private float _size = 1.15333f;
        private int _exploredTileCount = 1;
        private List<MapTile> _tileMap = new List<MapTile>();
        private MapTile _selectedTile = null;

        private void Awake()
        {            
            _tileMapContainer = GameObject.Find("Tiles").transform;
        }

        public void GenerateMap(List<Tile> tiles)
        {
            _hexMap.SetTiles(tiles);

        }

        public void GenerateMap(int seed, int dimension)
        {
            ClearTiles();
            _hexMap.GenerateHexes(dimension);
            _hexMap.FindTile(0, 0, 0).IsExplored = true;
            _hexMap.AddNoise(seed);
            _hexMap.SetTileTypes(new List<BiomLimit> {
                new BiomLimit() { Start=0, End=20, TileType = TileType.Water },
                new BiomLimit() { Start=20, End=40, TileType = TileType.Desert },
                new BiomLimit() { Start=40, End=70, TileType = TileType.Grass },
                new BiomLimit() { Start=70, End=200, TileType = TileType.Mountain },
            });
        }        

        public void ShowHexes()
        {
            _exploredTileCount = _hexMap.Tiles.Where(w => w.IsExplored).Count();
            foreach (var tile in _hexMap.Tiles)
            {
                DrawTile(tile);
            }

            
        }

        public void SelectTile(Tile tile)
        {
            foreach (var t in _tileMap.Where(w => w.IsSelected))
            {
                t.ClearSelect();
            }

            var tileBase = _tileMap.Where(w => w.TileData.Hex.q == tile.Hex.q
                                       && w.TileData.Hex.r == tile.Hex.r)
                       .FirstOrDefault();
            if (_selectedTile == null
                || _selectedTile.TileData.Hex != tileBase.TileData.Hex)
            {
                tileBase.Select();
                _selectedTile = tileBase;

                UIManager.Instance.ShowBuildPanel();
            }
            else { 
                tileBase.ClearSelect();
                _selectedTile = null;

                UIManager.Instance.HideBuildPanel();
            }
            


            Debug.Log(string.Format("q:{0} r:{1}", tileBase.TileData.Hex.q, tileBase.TileData.Hex.r));            
        }        

        public MapTile FindTile(int q, int r) 
        {
            return _tileMap.Where(w => w.TileData.Hex.q == q
                                && w.TileData.Hex.r == r)
                       .FirstOrDefault();
        }

        public void ExploreTile(Tile tile)
        {
            if (tile.IsExplored == false)
            {
                var neighbours = _hexMap.GetNeighbours(tile.Hex, 1, 1);

                var neighbourTiles = FindTilesByHex(neighbours);

                if (tile.ExploreProgress > 0f
                    || neighbourTiles.Where(w => w.TileData.IsExplored).Count() > 0
                    )
                {

                    var tileResource = ResourceCore.Instance.Tiles.Where(w => w.TileType == tile.TileType).FirstOrDefault();
                    var tileBase = _tileMap.Where(w => w.TileData.Hex.q == tile.Hex.q
                                        && w.TileData.Hex.r == tile.Hex.r)
                        .FirstOrDefault();

                    tileBase.StartExplore(null, _exploredTileCount++);
                }
            }
        }

        public bool HasEnergyInNeighbour(Tile tile)
        {
            var neighbours = _hexMap.GetNeighbours(tile.Hex, 1, 1);
            var neighbourTiles = FindTilesByHex(neighbours);

            return neighbourTiles.Where(w => w.TileData.HasEnergy).Count() > 0
                    || neighbours.Where(w => w.q == 0 && w.r == 0).Count() > 0 ;
        }

        List<MapTile> FindTilesByHex(List<Hex> hexes)
        {
            var result = new List<MapTile>();

            foreach (var h in hexes)
            {
                var tileBase = _tileMap.Where(w => w.TileData.Hex.q == h.q
                                    && w.TileData.Hex.r == h.r)
                    .FirstOrDefault();

                if(tileBase != null)
                {
                    result.Add(tileBase);
                }
            }

            return result;
        }        

        void DrawTile(Tile tile)
        {
            var tileResource = ResourceCore.Instance.Tiles.Where(w => w.TileType == tile.TileType).FirstOrDefault();

            var x = _size * (Mathf.Sqrt(3) * tile.Hex.q + Mathf.Sqrt(3) / 2f * tile.Hex.r);
            var y = _size * (3f/2f * tile.Hex.r);

            var pos = new Vector3(x,
                            0f,
                            y);

            var spawned = Instantiate(tileResource.Prefab, pos, Quaternion.identity, _tileMapContainer);
            //spawned.transform.parent = _tileMapContainer;

            var tileBase = spawned.GetComponent<MapTile>();
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

            if(tile.IsExplored == false && tile.ExploreProgress != 0f)
            {
                ExploreTile(tile);
            }

            if(tile.HasEnergy)
            {
                BuildManager.Instance.StartBuilding(_Scripts.TypeConstants.BuildingType.Pylon
                    , tileBase);
            }
        }
        public void ClearTiles()
        {
            _tileMap = new List<MapTile>();
            foreach (Transform child in _tileMapContainer)
            {
                GameObject.Destroy(child.gameObject);
            }

        }



    }
}
