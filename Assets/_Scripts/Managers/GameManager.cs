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
            MapManager.Instance.GenerateMap();
            MapManager.Instance.ShowHexes();
        }
    }
}
