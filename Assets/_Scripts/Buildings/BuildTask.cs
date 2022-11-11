using Assets._Scripts.Core.Task;
using Assets._Scripts.Drones;
using Assets._Scripts.Managers;
using Assets._Scripts.Map;
using Assets._Scripts.TypeConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Scripts.Buildings
{
    public class BuildTask : Task
    {
        private readonly BuildingType _buildingType;
        private readonly MapTile _tile;
        private DroneMoveTask _droneMoveTask;
        private string _buildingId;
        private Drone _currentDrone = null;

        public BuildTask(BuildingType buildingType, MapTile tile)
        {
            this._buildingType = buildingType;
            this._tile = tile;
            
        }

        public override bool Valid
        {
            get
            {
                throw new NotImplementedException();
            }
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

        public override void Execute()
        {
            Debug.Log("Build Task started:" + _buildingId);
            this.Started = true;            
            _buildingId = BuildManager.Instance.StartBuilding(new BuildingData()
            { 
                BuildingType = _buildingType,
                Position = _tile.TileData.Hex
            });
            
        }

        public override bool IsFinished()
        {
            var building = BuildManager.Instance.Buildings.Where(w => w.BuildingData.Id == _buildingId).FirstOrDefault();

            if (building != null
                && building.IsBuilding == false)
            {
                _currentDrone.SetIdle();
                Debug.Log("Build Task finished:" + _buildingId);
                return true;
            } else
            {
                return false;
            }
        }

    }
}
