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
        private GameParams _gameParams;
        private void Start()
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
                    BuildManager.Instance.StartBuilding(b.BuildingType
                        , MapManager.Instance.FindTile(b.Position.q, b.Position.r));
                }
            }

            

        }

        void OnApplicationQuit()
        {
            SaveGame();
        }

        public void Init()
        {
            _gameParams = new GameParams();
            _gameParams.Seed = UnityEngine.Random.Range(10000, 100000);
            _gameParams.Dimension = 10;
            MapManager.Instance.GenerateMap(_gameParams.Seed, _gameParams.Dimension);
            MapManager.Instance.ShowHexes();

            var baseTile = MapManager.Instance.FindTile(0, 0);


            BuildManager.Instance.StartBuilding(_Scripts.TypeConstants.BuildingType.MainBase
                , baseTile);

        }


        private void SaveGame()
        {
            var gd = new GameData();
            gd.Tiles = MapManager.Instance.Tiles;
            gd.Buildings = BuildManager.Instance.Buildings.Select(s => s.BuildingData).ToList();

            FileManager.SaveData("gameData.json", gd);
        }
    }
}
