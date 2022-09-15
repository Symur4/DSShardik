using Assets._Scripts.Core.Task;
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
            this.IsInitialised = true;
        }

        //Execute() needs to be called in update of the TaskManager.
        public override void Execute()
        {
            Debug.Log("Task started");
            this.Started = true;
            BuildManager.Instance.StartBuilding(_buildingType, _tile);
            
        }

        public override bool IsFinished()
        {
            return false;
        }

    }
}
