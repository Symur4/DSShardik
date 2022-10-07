using Assets._Scripts.Core.Task;
using Assets._Scripts.Drones;
using Assets._Scripts.Managers;
using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Scripts.Map
{
    public class ExploreTask : Task
    {
        private readonly MapTile _tile;
        private Drone _currentDrone;
        private DroneMoveTask _droneMoveTask;

        public ExploreTask(MapTile tile)
        {
            this._tile = tile;
        }

        public override bool Valid => throw new NotImplementedException();

        public override void Execute()
        {            
            MapManager.Instance.ExploreTile(_tile.TileData.Hex);

        }        

        public override void Initialise()
        {
            if (_currentDrone == null)
            {
                _currentDrone = DroneManager.Instance.GetIdleDrone(true);

            }
            else
            {
                if (this._droneMoveTask == null)
                {
                    _droneMoveTask = new DroneMoveTask(_tile, _currentDrone);
                    TaskManager.Instance.AddTask(_droneMoveTask);
                }

                if (this._droneMoveTask.IsFinished())
                {
                    this.IsInitialised = true;
                }
            }
        }

        public override bool IsFinished()
        {
            if(this._tile.TileData.IsExplored)
            {
                _currentDrone.SetIdle();
                Debug.Log("Explore Task finished");

            }

            return this._tile.TileData.IsExplored;
        }
    }
}
