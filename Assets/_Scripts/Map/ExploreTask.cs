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
            Started = true;
            MapManager.Instance.ExploreTile(_tile.TileData.Hex);
            Debug.Log("Explore Task started:" + TaskID);
        }        

        public override void Initialise()
        {
            if (_currentDrone == null)
            {
                _currentDrone = DroneManager.Instance.GetIdleDrone(true);                
            }

            if (_currentDrone != null)
            {                
                if (this._droneMoveTask == null)
                {
                    _droneMoveTask = new DroneMoveTask(_tile, _currentDrone);
                    Debug.Log("Drone moving:" + _tile.TileData.Hex.q + ":" + _tile.TileData.Hex.q);
                    TaskManager.Instance.AddTask(_droneMoveTask);
                    return;
                }

                if (this._droneMoveTask.IsFinished())
                {
                    Debug.Log("ExploreTask initialized");

                   this.IsInitialised = true;
                    _droneMoveTask = null;  
                }
            }
        }

        private void OnDroneMoveFinished()
        {
            
        }

        public override bool IsFinished()
        {
            if(this._tile.TileData.IsExplored)
            {
                _currentDrone.SetIdle();
                _currentDrone = null;
                Debug.Log("Explore Task finished" + TaskID);
                _isFinished = true;
            }
            
            return _isFinished;
        }
    }
}
