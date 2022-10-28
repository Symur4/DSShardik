using Assets._Scripts.Buildings;
using Assets._Scripts.Core;
using Assets._Scripts.Managers;
using Assets._Scripts.Models;
using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private bool Save = true;

        private GameParams _gameParams;
        private void Start()
        {
            Load();
        }

        void OnApplicationQuit()
        {
            SaveGame();
        }

        private void Load()
        {
            var gameData = FileManager.LoadData<GameData>("gameData.json");

            if (gameData == null)
            {
                Init();
            }
            else
            {
                _gameParams = gameData.GameParams;
                MapManager.Instance.GenerateMap(gameData.Tiles);
                MapManager.Instance.ShowHexes();
                foreach (var b in gameData.Buildings)
                {
                    BuildManager.Instance.StartBuilding(b, true);                         
                }
                ResourceManager.Instance.InitResources(gameData.Resources);  
                DroneManager.Instance.InitDrones(gameData.Drones);
            }

        }

        public void Init()
        {
            _gameParams = new GameParams();
            _gameParams.Seed = UnityEngine.Random.Range(10000, 100000);
            _gameParams.Dimension = 10;
            MapManager.Instance.GenerateMap(_gameParams.Seed, _gameParams.Dimension);
            MapManager.Instance.ShowHexes();

            var baseTile = MapManager.Instance.FindTile(0, 0);


            BuildManager.Instance.StartBuilding(new BuildingData() { 
                Id = Guid.NewGuid().ToString(),
                BuildingType = _Scripts.TypeConstants.BuildingType.MainBase,
                Position = baseTile.TileData.Hex
            }, true);

            ResourceManager.Instance.ResourceGenerated(TypeConstants.ResourceType.Concrete, 100); 
            ResourceManager.Instance.ResourceGenerated(TypeConstants.ResourceType.IronBar, 100);

            DroneManager.Instance.AddDrone(MapManager.Instance.FindTile(0, 0));
            DroneManager.Instance.AddDrone(MapManager.Instance.FindTile(0, 0));


        }


        private void SaveGame()
        {
            if (this.Save)
            {
                var gd = new GameData();
                gd.Tiles = MapManager.Instance.Tiles;
                gd.Buildings = BuildManager.Instance.Buildings.Select(s => s.BuildingData).ToList();
                gd.Resources = ResourceManager.Instance.GetResourceData();
                gd.Drones = DroneManager.Instance.GetDronesData();

                FileManager.SaveData("gameData.json", gd);
            }
        }
    }
}
