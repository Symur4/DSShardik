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

        private void Start()
        {

            var gameData = FileManager.LoadData<GameData>("gameData.json");

            if (gameData == null)
            {
                MapManager.Instance.GenerateMap();
                MapManager.Instance.ShowHexes();
            }
            else
            {
                MapManager.Instance.GenerateMap(gameData.Tiles);
                MapManager.Instance.ShowHexes();                
            }

            var baseTile = MapManager.Instance.FindTile(0, 0);

            BuildManager.Instance.StartBuilding(_Scripts.TypeConstants.BuildingType.MainBase
                , baseTile);

        }

        void OnApplicationQuit()
        {
            SaveGame();
        }

        private void SaveGame()
        {
            var gd = new GameData();
            gd.Tiles = MapManager.Instance.Tiles;

            FileManager.SaveData("gameData.json", gd);
        }
    }
}
